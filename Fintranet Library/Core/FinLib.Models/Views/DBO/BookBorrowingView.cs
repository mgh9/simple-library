using FinLib.Models.Base.View;
using FinLib.Models.Base;

namespace FinLib.Models.Views.DBO
{
    public class BookBorrowingView : UpdatableView
    {
        [ViewColumn("Book Title")]
        public string BookTitle { get; set; }

        [ViewColumn("Category")]
        public string CategoryTitle { get; set; }
        
        [ViewColumn("Customer")]
        public string CustomerUserFullName { get; set; }
        
        [ViewColumn("Librarian")]
        public string LibrarianUserFullName { get; set; }

        [ViewColumn("Borrowing Date", Filter = "dateTime")]
        public DateTime BorrowingDate { get; set; }
        
        [ViewColumn("Returning Date", Filter = "dateTime")]
        public DateTime? ReturningDate { get; set; }

        public int BookId { get; set; }
        public int CustomerUserRoleId { get; set; }
        public int LibrarianUserRoleId { get; set; }
    }
}
