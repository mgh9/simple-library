namespace FinLib.Models.Dtos.DBO
{
    public class BookBorrowingDto : Base.Dto.UpdatableDto
    {
        public int BookId { get; set; }
        public int CustomerUserRoleId { get; set; }
        public int LibrarianUserRoleId { get; set; }

        public DateTime BorrowingDate { get; set; }
        public DateTime? ReturningDate { get; set; }
    }
}
