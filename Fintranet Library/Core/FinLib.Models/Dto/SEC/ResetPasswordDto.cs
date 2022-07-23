namespace FinLib.Models.Dtos.SEC
{
    public class ResetPasswordDto : Base.Dto.BaseDto
    {
        public int SubjectUserId { get; set; }

        public string NewPassword { get; set; }
    }
}
