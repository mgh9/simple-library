using FinLib.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinLib.Web.Api.Base
{
    public interface ITitleValueProvider
    {
        Task<JsonResult<List<TitleValue<int>>>> GetTitleValueListAsync(bool includeEmptySelector = false, bool includeSelectAllSelector = false, string text = "");
    }
}
