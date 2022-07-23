using FinLib.Common.Exceptions.Business;
using FinLib.DomainClasses.DBO;
using FinLib.DomainClasses.SEC;
using FinLib.Mappings;
using FinLib.Models.Dtos;
using FinLib.Models.Dtos.DBO;
using FinLib.Models.SearchFilters.DBO;
using FinLib.Models.Views.DBO;
using FinLib.Services.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace FinLib.Services.DBO
{
    public partial class BookBorrowingService
    {
        public override async Task<GetResultDto<BookBorrowingView>> GetAsync(GetRequestDto<BookBorrowingSearchFilter> model)
        {
            ValidateOnGet(model);
            PrepareModelOnGet(model);

            var query = from theBookBorrowing in DbContext.Set<BookBorrowing>()

                        join theBook in DbContext.Set<Book>() on theBookBorrowing.BookId equals theBook.Id
                        join theCategory in DbContext.Set<Category>() on theBook.CategoryId equals theCategory.Id

                        join theLibrarianUserRole in DbContext.Set<UserRole>() on theBookBorrowing.LibrarianUserRoleId equals theLibrarianUserRole.Id
                        join theLibrarianUser in DbContext.Set<User>() on theLibrarianUserRole.UserId equals theLibrarianUser.Id

                        join theCustomerUserRole in DbContext.Set<UserRole>() on theBookBorrowing.CustomerUserRoleId equals theCustomerUserRole.Id
                        join theCustomerUser in DbContext.Set<User>() on theCustomerUserRole.UserId equals theCustomerUser.Id

                        select new BookBorrowingView
                        {
                            Id = theBookBorrowing.Id,
                            CategoryTitle = theCategory.Title,
                            BookTitle = theBook.Title,
                            CustomerUserFullName = theCustomerUser.FirstName + " " + theCustomerUser.LastName,
                            LibrarianUserFullName = theLibrarianUser.FirstName + " " + theLibrarianUser.LastName,
                            BorrowingDate = theBookBorrowing.BorrowingDate,
                            ReturningDate = theBookBorrowing.ReturningDate,

                            BookId= theBookBorrowing.BookId,
                            LibrarianUserRoleId = theBookBorrowing.LibrarianUserRoleId,
                            CustomerUserRoleId = theBookBorrowing.CustomerUserRoleId,

                            UpdateDate = theBookBorrowing.UpdateDate,
                        };

            query = FilterService.ParseFilter(query, model.SearchFilterModel);
            var count = await query.CountAsync();            

            return new GetResultDto<BookBorrowingView>(query.OrderBy(model.PageOrder)
                       .Skip(model.PageIndex * model.PageSize)
                       .Take(model.PageSize)
                       .ToList(), count);
        }

        public async Task ReturnBookAsync(ReturnBookDto model)
        {
            var pendingBorrowingBook = await FindAndThrowExceptionIfNotFoundAsync(model.BookBorrowingId);
            validateOnReturnBook(pendingBorrowingBook);

            pendingBorrowingBook.ReturningDate = DateTime.Now;
            await SaveAsync(pendingBorrowingBook);
        }

        private static void validateOnReturnBook(BookBorrowingDto model)
        {
            if (model.ReturningDate.HasValue)
                throw new BusinessValidationException($"Cannot completed. The book already returned at {model.ReturningDate}");
        }

        protected override void ValidateOnInsert(BookBorrowingDto model)
        {
            base.ValidateOnInsert(model);
        }

        protected override void PrepareModelOnInsert(BookBorrowingDto model)
        {
            base.PrepareModelOnInsert(model);

            model.BorrowingDate = DateTime.Now;
            model.LibrarianUserRoleId = CommonServicesProvider.LoggedInUserRoleId.Value;
        }
    }
}
