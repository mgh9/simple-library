namespace FinLib.Models.Base.Dto
{
    public class GeneralDto : UpdatableDto
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
