namespace FinLib.Web.Shared.Models.ViewModels
{
    public class GeneralErrorViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public string HelpUrlTitle { get; set; }
        public string HelpUrl { get; set; }

        public string ContextTraceId { get; set; }
        public string TraceId { get; set; }
    }
}
