using FinLib.Models.Base;
using FinLib.Models.Dtos.CNT;
using FinLib.Models.Dtos.DBO;
using FinLib.Models.Enums;
using FinLib.Services.Base;
using FinLib.Web.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using JsonResult = FinLib.Models.Base.JsonResult;

namespace FinLib.Web.Api.DBO
{
    public class MyBorrowingHistoryController : Base.BaseEntityController<FinLib.DomainClasses.DBO.BookBorrowing, FinLib.Models.Dtos.DBO.BookBorrowingDto, FinLib.Models.Views.DBO.MyBorrowingHistoryView, FinLib.Services.DBO.MyBorrowingHistoryService
        , FinLib.Models.SearchFilters.DBO.MyBorrowingHistorySearchFilter>
    {
        public MyBorrowingHistoryController(ICommonServicesProvider<Models.Configs.GlobalSettings> commonServicesProvider, FinLib.Services.DBO.MyBorrowingHistoryService service)
            : base(commonServicesProvider,service)
        {

        }
    }
}

