




interface IBookBorrowingSearchFilter extends IUpdatableEntitySearchFilter
{

    bookId: ISearchFilterItem<number>;
    customerUserRoleId: ISearchFilterItem<number>;
    
}