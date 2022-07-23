using System;

namespace FinLib.Models.Attributes
{
    /// <summary>
    /// When applied on a class, the Typewriter engine doesn't render it as a TS class file
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class| AttributeTargets.Enum, Inherited = false, AllowMultiple = true)]
    public sealed class IgnoreTypewriterMappingAttribute : Attribute
    {

    }
}
