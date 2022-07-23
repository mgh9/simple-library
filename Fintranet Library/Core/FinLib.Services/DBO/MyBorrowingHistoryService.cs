using FinLib.DomainClasses.DBO;
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
    public class MyBorrowingHistoryService : BaseEntityService<BookBorrowing, BookBorrowingDto, MyBorrowingHistoryView, MyBorrowingHistorySearchFilter>
    {
        public MyBorrowingHistoryService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
            : base(commonServicesProvider)
        {

        }

        public override async Task<GetResultDto<MyBorrowingHistoryView>> GetAsync(GetRequestDto<MyBorrowingHistorySearchFilter> model)
        {
            ValidateOnGet(model);
            PrepareModelOnGet(model);

            var currentUserRoleId = CommonServicesProvider.LoggedInUserRoleId.Value;
            var query = from theBookBorrowing in DbContext.Set<BookBorrowing>()

                        join theBook in DbContext.Set<Book>() on theBookBorrowing.BookId equals theBook.Id
                        join theCategory in DbContext.Set<Category>() on theBook.CategoryId equals theCategory.Id

                        where theBookBorrowing.CustomerUserRoleId == currentUserRoleId
                        select new MyBorrowingHistoryView
                        {
                            Id = theBookBorrowing.Id,
                            CategoryTitle = theCategory.Title,
                            BookTitle = theBook.Title,
                            BorrowingDate = theBookBorrowing.BorrowingDate,
                            ReturningDate = theBookBorrowing.ReturningDate,
                            BookId = theBookBorrowing.BookId,

                            UpdateDate = theBookBorrowing.UpdateDate,
                        };

            query = FilterService.ParseFilter(query, model.SearchFilterModel);
            var count = await query.CountAsync();

            return new GetResultDto<MyBorrowingHistoryView>(query.OrderBy(model.PageOrder)
                       .Skip(model.PageIndex * model.PageSize)
                       .Take(model.PageSize)
                       .ToList(), count);
        }
    }
}
