using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TagSO", menuName = "GAS/Tag/TagSO")]
public class TagSO : ScriptableObject
{
    public string TagShortName;
    public TagSO Parent;

    [SerializeField, HideInInspector] private int Id = TagUtil.INVALID_TAG_ID;
    [SerializeField, HideInInspector] private bool isLocked = false;
    public int GetId()
    {
        return Id;
    }
    public string GetTagFullName(HashSet<TagSO> visited = null)
    {
        if (visited == null)
        {
            visited = new HashSet<TagSO>();
        }

        if (visited.Contains(this))
        {
            Debug.LogError($"Cyclic dependency detected in tag hierarchy at tag: {TagShortName}");
            return TagShortName;
        }

        visited.Add(this);

        if (Parent == null)
        {
            return TagShortName;
        }
        string fullName = Parent.GetTagFullName(visited) + "." + TagShortName;
        return fullName;
    }

    public bool HasValidId()
    {
        return Id != 0;
    }
#if UNITY_EDITOR
    internal void AssignId(int id)
    {
        if (isLocked)
        {
            throw new InvalidOperationException($"Tag '{GetTagFullName()}'는 Id가 이미 할당되어 있습니다. ID: {Id}");
        }

        Id = id;
        isLocked = true;
    }

    public List<TagSO> GetAllParents()
    {
        List<TagSO> parents = new List<TagSO>();
        TagSO current = this.Parent;
        while (current != null)
        {
            parents.Add(current);
            current = current.Parent;
        }
        return parents;
    }
#endif
}
