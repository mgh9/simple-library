using System;

namespace FinLib.Models.Attributes
{
    /// <summary>
    /// مپینگ برای تایپ رایتر، بیخیالش شو
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class| AttributeTargets.Enum, Inherited = false, AllowMultiple = true)]
    public sealed class IgnoreTypewriterMappingAttribute : Attribute
    {

    }
}
