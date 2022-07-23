using FinLib.Models.Base.View;
using FinLib.Models.Base;

namespace FinLib.Models.Views.DBO
{
    public class MyBorrowingHistoryView : UpdatableView
    {
        [ViewColumn("Book Title")]
        public string BookTitle { get; set; }

        [ViewColumn("Category")]
        public string CategoryTitle { get; set; }
        
        [ViewColumn("Borrowing Date", Filter = "dateTime")]
        public DateTime BorrowingDate { get; set; }
        
        [ViewColumn("Returning Date", Filter = "dateTime")]
        public DateTime? ReturningDate { get; set; }

        public int BookId { get; set; }
    }
}
