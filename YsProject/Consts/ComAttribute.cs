using System;

namespace YsProject.Consts
{
    /// <summary>
    /// Page URL
    /// </summary>
    public class BaseAttribute : Attribute
    {
        /// <summary>
        /// DB中の値
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// PageUrl
    /// </summary>
    public class ValueAttribute : BaseAttribute
    {
        public ValueAttribute(string value)
        {
            this.Value = value;
        }
    }
}