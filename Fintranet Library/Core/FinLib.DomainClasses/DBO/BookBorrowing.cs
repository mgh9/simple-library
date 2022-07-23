using FinLib.DomainClasses.SEC;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinLib.DomainClasses.DBO
{
    public class BookBorrowing: Base.UpdatableEntity
    {
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [ForeignKey("CustomerUserRole")]
        public int CustomerUserRoleId { get; set; }
        public UserRole CustomerUserRole { get; set; }

        [ForeignKey("LibrarianUserRole")]
        public int LibrarianUserRoleId { get; set; }
        public UserRole LibrarianUserRole { get; set; }

        public DateTime BorrowingDate { get; set; }
        public DateTime? ReturningDate { get; set; }
    }
}
