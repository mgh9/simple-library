using FinLib.Models.Base;
using FinLib.Models.Dtos.DBO;
using FinLib.Models.Enums;
using FinLib.Web.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JsonResult = FinLib.Models.Base.JsonResult;

namespace FinLib.Web.Api.DBO
{
    public partial class BookBorrowingController
    {
        [MyAuthorize(ApplicationRole.Librarian)]
        public override async Task<JsonResult<int>> InsertAsync([FromBody] BookBorrowingDto model)
        {
            return await base.InsertAsync(model);
        }

        [MyAuthorize(ApplicationRole.Librarian)]
        [HttpPost]
        public async Task<JsonResult> ReturnBookAsync(ReturnBookDto model)
        {
            await Service.ReturnBookAsync(model);

            var result = new JsonResult
            {
                Success = true
            };

            return result;
        }
    }
}

