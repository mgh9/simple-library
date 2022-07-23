namespace FinLib.Models.Dtos.SEC
{
    public class ChangePasswordDto : Base.Dto.BaseDto
    {
        public int Id { get; set; }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordRepeat { get; set; }
    }
}
