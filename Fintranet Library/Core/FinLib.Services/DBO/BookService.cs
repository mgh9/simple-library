using FinLib.DomainClasses.DBO;
using FinLib.Mappings;
using FinLib.Models.Dtos;
using FinLib.Models.SearchFilters.DBO;
using FinLib.Models.Views.DBO;
using FinLib.Services.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace FinLib.Services.DBO
{
    public partial class BookService
    {
        public override async Task<GetResultDto<BookView>> GetAsync(GetRequestDto<BookSearchFilter> model)
        {
            ValidateOnGet(model);
            PrepareModelOnGet(model);

            var query = from theBook in DbContext.Set<Book>()

                        join theCategory in DbContext.Set<Category>() on theBook.CategoryId equals theCategory.Id

                        select new BookView
                        {
                            Id = theBook.Id,
                            IsActive = theBook.IsActive,
                            Title = theBook.Title,
                            UpdateDate = theBook.UpdateDate,

                            CategoryTitle = theCategory.Title,
                        };

            query = FilterService.ParseFilter(query, model.SearchFilterModel);

            var count = await query.CountAsync();

            return new GetResultDto<BookView>(query.OrderBy(model.PageOrder)
                       .Skip(model.PageIndex * model.PageSize)
                       .Take(model.PageSize)
                       .ToList(), count);
        }

        public override async Task<BookView> GetAsViewByIdAsync(int id)
        {
            var query = from theBook in _repository
                        join theCategory in DbContext.Set<Category>() on theBook.CategoryId equals theCategory.Id
                        where theBook.Id == id
                        select new BookView 
                        { 
                            Id = theBook.Id,
                            Title = theBook.Title,
                            CategoryTitle = theCategory.Title,
                            IsActive = theBook.IsActive,
                            UpdateDate = theBook.UpdateDate
                        };
            
            return await query.SingleAsync();
        }
    }
}
