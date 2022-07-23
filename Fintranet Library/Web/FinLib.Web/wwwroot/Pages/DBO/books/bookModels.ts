namespace FinLib.Pages.DBO {
    export interface IBookScope extends ITableBaseScope<BookController, IBookDto, IBookView, IBookSearchFilter> {
        categories: ITitleValue<number>[];
        categoriesConfig: any;
        categoryId: number;

        categoriesForSearchFilter: ITitleValue<number>[];
        categoriesForSearchFilterConfig: any;
    }
}