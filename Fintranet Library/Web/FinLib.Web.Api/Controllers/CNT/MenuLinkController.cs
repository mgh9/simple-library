using FinLib.Models.Base;
using FinLib.Models.Dtos.CNT;
using FinLib.Models.Enums;
using FinLib.Web.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace FinLib.Web.Api.CNT
{
    public partial class MenuLinkController
    {
        [MyAuthorize(ApplicationRole.Customer)]
        [HttpGet]
        public async Task<JsonResult<List<MenuLinkDto>>> GetMenusAsync()
        {
            var result = new JsonResult<List<MenuLinkDto>>
            {
                Data = await Service.GetAsync(),
                Success = true
            };

            return result;
        }
    }
}

