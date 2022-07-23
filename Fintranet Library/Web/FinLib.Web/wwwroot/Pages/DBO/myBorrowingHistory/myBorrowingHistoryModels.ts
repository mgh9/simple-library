namespace FinLib.Pages.DBO {
    export interface IMyBorrowingHistoryScope extends ITableBaseScope<MyBorrowingHistoryController
        , IBookBorrowingDto, IMyBorrowingHistoryView, IMyBorrowingHistorySearchFilter> {

        books: ITitleValue<number>[];
        booksConfig: any;
        booksForSearchFilter: ITitleValue<number>[];
        booksForSearchFilterConfig: any;
    }
}