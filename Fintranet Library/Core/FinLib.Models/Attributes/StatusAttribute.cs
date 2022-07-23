using System;
using System.ComponentModel;

namespace FinLib.Models.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public sealed class StatusAttribute : DescriptionAttribute
    {
        public string BackColor { get; set; }

        public StatusAttribute(string description, string backColor)
            : base(description)
        {
            BackColor = backColor;
        }
    }
}
