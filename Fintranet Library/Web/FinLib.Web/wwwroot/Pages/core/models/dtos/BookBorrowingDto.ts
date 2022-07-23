




interface IBookBorrowingDto extends IUpdatableDto 
{   
    bookId: number;
    customerUserRoleId: number;
    librarianUserRoleId: number;
    borrowingDate: Date;
    returningDate: Date;
}