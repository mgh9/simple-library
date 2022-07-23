using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace FinLib.Common.Helpers
{
    public static class AspNetModelStateHelper
    {
        public static string GetAllErrors(ModelStateDictionary modelState)
        {
            StringBuilder retval = new StringBuilder();

            foreach (var item in modelState.Values)
            {
                foreach (var item2 in item.Errors)
                {
                    retval.Append(item2.ErrorMessage);
                }
            }

            return retval.ToString();
        }
    }
}
