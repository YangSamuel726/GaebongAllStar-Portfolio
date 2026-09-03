using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

/// <summary>
/// 네트워크 Actor가 보유한 Tag(상태)를 관리한다.
/// Tag 변경은 State Authority에서만 수행하며,
/// 자식 Tag가 활성화되면 모든 부모 Tag도 Reference Count 방식으로 활성화한다.
/// </summary>
public class NetworkTagSystem : NetworkBehaviour
{
#if UNITY_EDITOR
    [OnChangedRender(nameof(OnTagChanged))]
#endif
    // 활성 Tag의 ID와 적용 횟수를 저장한다.
    // bool 대신 count를 사용하는 이유는 동일 Tag의 중첩 적용과
    // 여러 자식 Tag가 동일 부모 Tag를 활성화하는 경우를 처리하기 위함이다.
    [Networked, Capacity(GameConfig.AllTagCount)] private NetworkDictionary<int, int> TagDictionary { get; } // id, 활성화 count

    // Tag 계층 탐색을 위한 TagId -> ParentTagId 캐시.
    // 서버에서 Tag 변경 시 부모 Tag를 재귀적으로 적용/제거하는 데 사용한다.
    private Dictionary<int, int> parentTagMap = new Dictionary<int, int>(); // 서버용

    private Dictionary<int, Action<int>> onTagAddedEvent = new Dictionary<int, Action<int>>();
    private Dictionary<int, Action<int>> onTagRemovedEvent = new Dictionary<int, Action<int>>();

#if UNITY_EDITOR
    [SerializeField] private List<string> debugTagNames = new List<string>(); // 디버그용 태그 이름 리스트

    private void OnTagChanged()
    {
        debugTagNames.Clear();
        foreach (var kvp in TagDictionary)
        {
            int tagId = kvp.Key;
            int count = kvp.Value;
            string debugString = $"Tag ID: {tagId}, Count: {count}";
            debugTagNames.Add(debugString);
        }
    }
#endif

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            // 모든 태그를 가져와서 태그 트리 초기화
            foreach (var tagEntry in TagUtil.GetTagToIdMap())
            {
                parentTagMap[tagEntry.Key] = TagUtil.GetParentId(tagEntry.Key);
            }
        }
    }

    public bool HasTag(int tag)
    {
        return TagDictionary.TryGet(tag, out int count) && count > 0;
    }
    public bool HasTag(IReadOnlyList<int> tags)
    {
        foreach (var tag in tags)
        {
            if (!HasTag(tag))
            {
                return false;
            }
        }
        return true;
    }

    public bool HasAnyTag(IReadOnlyList<int> tags)
    {
        foreach (var tag in tags)
        {
            if (HasTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    // 자식 Tag가 활성화되면 부모 Tag도 활성 상태로 간주한다.
    // 예: State.Stun 활성화 → State도 활성화.
    // 부모 역시 count로 관리하여 여러 자식이 동시에 활성화된 경우에도
    // 하나의 자식 제거만으로 부모가 비활성화되지 않도록 한다.
    public void AddTag(int tag)
    {
        if (Object.HasStateAuthority == false) return;
        if (tag == TagUtil.INVALID_TAG_ID) return;
        if (tag == TagUtil.NO_PARENT_TAG_ID) return;

        if (TagDictionary.TryGet(tag, out int currentCount))
        {
            TagDictionary.Set(tag, currentCount + 1);
        }
        else
        {
            TagDictionary.Set(tag, 1);
            InvokeTagAddedEvent(tag);
        }
        AddTag(parentTagMap[tag]);
    }
    public void RemoveTag(int tag)
    {
        if (Object.HasStateAuthority == false) return;
        if (tag == TagUtil.INVALID_TAG_ID) return;
        if (tag == TagUtil.NO_PARENT_TAG_ID) return;

        if (TagDictionary.TryGet(tag, out int currentCount) && currentCount > 0)
        {
            int newCount = currentCount - 1;
            if (newCount <= 0)
            {
                TagDictionary.Remove(tag);
                InvokeTagRemoveEvent(tag);
            }
            else
            {
                TagDictionary.Set(tag, newCount);
            }
            RemoveTag(parentTagMap[tag]);
        }
    }

    public void AddTag(IReadOnlyList<int> tags)
    {
        foreach (var tag in tags)
        {
            AddTag(tag);
        }
    }
    public void RemoveTag(IReadOnlyList<int> tags)
    {
        foreach (var tag in tags)
        {
            RemoveTag(tag);
        }
    }

    public void RegisterTagAddedEvent(int tagId, Action<int> onTagAdded)
    {
        if (this.onTagAddedEvent.ContainsKey(tagId) == false)
        {
            this.onTagAddedEvent[tagId] = onTagAdded;
        }
        else
        {
            this.onTagAddedEvent[tagId] += onTagAdded;
        }
    }
    public void UnregisterTagAddedEvent(int tagId, Action<int> onTagAdded)
    {
        if (this.onTagAddedEvent.ContainsKey(tagId))
        {
            this.onTagAddedEvent[tagId] -= onTagAdded;
        }
    }

    public void RegisterTagRemovedEvent(int tagId, Action<int> onTagRemoved)
    {
        if (this.onTagRemovedEvent.ContainsKey(tagId) == false)
        {
            this.onTagRemovedEvent[tagId] = onTagRemoved;
        }
        else
        {
            this.onTagRemovedEvent[tagId] += onTagRemoved;
        }
    }

    public void UnregisterTagRemovedEvent(int tagId, Action<int> onTagRemoved)
    {
        if (this.onTagRemovedEvent.ContainsKey(tagId))
        {
            this.onTagRemovedEvent[tagId] -= onTagRemoved;
        }
    }

    // 하나의 Listener에서 예외가 발생해도
    // 나머지 Listener의 실행이 중단되지 않도록 개별 호출한다.
    private void InvokeTagAddedEvent(int tagId)
    {
        if (!onTagAddedEvent.TryGetValue(tagId, out var callbacks))
            return;

        if (callbacks == null)
            return;

        foreach (Delegate callback in callbacks.GetInvocationList())
        {
            try
            {
                ((Action<int>)callback).Invoke(tagId);
            }
            catch (Exception ex)
            {
                Debug.LogError(
                    $"[NetworkTagSystem] 태그 {tagId} 추가 이벤트 실패\n" +
                    $"콜백: {callback.Method.DeclaringType?.Name}.{callback.Method.Name}"
                );

                Debug.LogException(ex);
            }
        }
    }
    private void InvokeTagRemoveEvent(int tagId)
    {
        if (!onTagRemovedEvent.TryGetValue(tagId, out var callbacks))
            return;

        if (callbacks == null)
            return;

        foreach (Delegate callback in callbacks.GetInvocationList())
        {
            try
            {
                ((Action<int>)callback).Invoke(tagId);
            }
            catch (Exception ex)
            {
                Debug.LogError(
                    $"[NetworkTagSystem] 태그 {tagId} 제거 이벤트 실패\n" +
                    $"콜백: {callback.Method.DeclaringType?.Name}.{callback.Method.Name}"
                );

                Debug.LogException(ex);
            }
        }
    }
}
