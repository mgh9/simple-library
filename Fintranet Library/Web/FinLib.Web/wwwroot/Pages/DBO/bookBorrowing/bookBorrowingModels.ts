namespace FinLib.Pages.DBO {
    export interface IBookBorrowingScope extends ITableBaseScope<BookBorrowingController, IBookBorrowingDto, IBookBorrowingView, IBookBorrowingSearchFilter> {
        books: ITitleValue<number>[];
        booksConfig: any;
        booksForSearchFilter: ITitleValue<number>[];
        booksForSearchFilterConfig: any;

        customers: ITitleValue<number>[];
        customersConfig: any;
        customersForSearchFilter: ITitleValue<number>[];
        customersForSearchFilterConfig: any;
    }
}