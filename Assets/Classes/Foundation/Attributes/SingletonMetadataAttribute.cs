using System;
using UnityEngine;

namespace Assets.Classes.Foundation.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SingletonMetadataAttribute : Attribute
    {
        public SingletonMetadataAttribute(string parentName = "", string parentTag = "", string name = "", string tag = "")
        {
            Tag = tag;
            Name = name;
            ParentTag = parentTag;
            ParentName = parentName;

        }

        public string ParentName { get; private set; }
        public string ParentTag { get; private set; }
        public string Name { get; private set; }
        public string Tag { get; private set; }

        public GameObject GetParentGameObject()
        {
            if (!string.IsNullOrEmpty(ParentName))
            {
                return GameObject.Find(ParentName);
            }
            return GameObject.FindGameObjectWithTag(ParentTag);
        }

        public void ApplyMetadataTo(GameObject target)
        {
            var parent = GetParentGameObject();
            if (parent != null)
            {
                target.transform.parent = parent.transform;
            }
            if (!string.IsNullOrEmpty(Name))
            {
                target.name = Name;
            }
            if (!string.IsNullOrEmpty(Tag))
            {
                target.tag = Tag;
            }
        }

    }
}
