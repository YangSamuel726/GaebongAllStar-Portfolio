using System.Collections.Generic;
using UnityEngine;
# if UNITY_EDITOR
using UnityEditor;
# endif
using System.Linq;

public static class TagUtil
{
    public const int INVALID_TAG_ID = 0;
    public const int NO_PARENT_TAG_ID = -1;
    private static Dictionary<int, TagSO> tagToIdMap;
    private static Dictionary<int, List<int>> tagChildMap;
    private const string TAG_LIBRARY_PATH = "Tag/TagLibrarySO";


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        TagLibrarySO tagLibrary = LoadTagLibrary();
        if (tagLibrary == null)
        {
            Debug.LogError("TagUtil: Failed to load TagLibrarySO.");
            return;
        }

        tagToIdMap = new Dictionary<int, TagSO>();
        foreach (var tag in tagLibrary.Tags)
        {
            tagToIdMap[tag.GetId()] = tag;
        }

        tagChildMap = new Dictionary<int, List<int>>();
        foreach (var tag in tagLibrary.Tags)
        {
            if (tag.Parent == null) continue;
            int parentId = tag.Parent.GetId();
            if (!tagChildMap.ContainsKey(parentId))
            {
                tagChildMap[parentId] = new List<int>();
            }
            tagChildMap[parentId].Add(tag.GetId());
        }
    }

    public static Dictionary<int, TagSO> GetTagToIdMap()
    {
        return tagToIdMap;
    }

    public static int GetParentId(int tagId)
    {
        if (tagToIdMap.TryGetValue(tagId, out TagSO tag))
        {
            if (tag.Parent != null)
            {
                return tag.Parent.GetId();
            }
        }
        return NO_PARENT_TAG_ID; // 부모 태그가 없음을 나타냄
    }

    public static TagLibrarySO LoadTagLibrary()
    {
        TagLibrarySO tagLibrary = Resources.Load<TagLibrarySO>(TAG_LIBRARY_PATH);
        if (tagLibrary == null)
        {
#if UNITY_EDITOR
            tagLibrary = ScriptableObject.CreateInstance<TagLibrarySO>();
            AssetDatabase.CreateAsset(tagLibrary, "Assets/Resources/Tag/TagLibrarySO.asset");
            AssetDatabase.SaveAssets();
            Debug.Log("Created new TagLibrarySO asset at Resources/Tag/TagLibrarySO.asset");
#else
            Debug.LogError("TagLibrarySO asset not found in Resources/Tag/");
            return null;
#endif
        }
        return tagLibrary;
    }
}
