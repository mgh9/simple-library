

interface IBookBorrowingView extends IUpdatableView {
    bookTitle: string;
    categoryTitle: string;
    customerUserFullName: string;
    librarianUserFullName: string;
    borrowingDate: Date;
    returningDate: Date;
    bookId: number;
    customerUserRoleId: number;
    librarianUserRoleId: number;
    
}
