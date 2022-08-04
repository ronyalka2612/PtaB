using UnityEngine;
namespace Com.GNL.URP_MyLibProjectTest
{ 
    public class NamedArrayAttribute : PropertyAttribute
    {
        public readonly string nameAtb;
        public readonly string[] names;
        public readonly bool isUseSplit;
        public readonly bool isUseIncrement;
        public NamedArrayAttribute(string nameAtb, string[] names) 
        { 
            this.nameAtb = nameAtb; 
            this.names = names;
        }
        public NamedArrayAttribute(string nameAtb, string[] names, bool isUseSplit)
        {
            this.nameAtb = nameAtb;
            this.names = names; 
            this.isUseSplit = isUseSplit;
        }
        public NamedArrayAttribute(string nameAtb, string[] names, bool isUseSplit, bool isUseIncrement)
        {
            this.nameAtb = nameAtb;
            this.names = names; 
            this.isUseSplit = isUseSplit; 
            this.isUseIncrement = isUseIncrement; 
        }
    }
}