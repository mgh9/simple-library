namespace FinLib.Models.Base
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ViewColumnAttribute : Attribute
    {
        public string Title { get; set; }
        public int OrderNumber { get; set; }
        public string Filter { get; set; }

        public ViewColumnAttribute(string title)
        {
            Title = title;
        }
    }
}
