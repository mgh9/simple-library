namespace FinLib.Models.Base
{
    public class TitleValue<T>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public T Value { get; set; }
    }
}
