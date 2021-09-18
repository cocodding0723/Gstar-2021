using UnityEngine;

namespace ObjectTemplate.CustomAttributes
{
    public class ProgressBarAttribute : PropertyAttribute
    {
        public bool hideWhenZero;
        public string label;
    }
}