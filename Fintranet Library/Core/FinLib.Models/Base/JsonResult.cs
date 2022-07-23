namespace FinLib.Models.Base
{
    public class JsonResult
    {
        public string IdpTraceId { get; set; }
        public string ContextTraceId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public Exception Error { get; set; }
    }
 
    public class JsonResult<TData> : JsonResult
    {
        public TData Data { get; set; }
    }

    public class JsonResult<TData, TConfig>
    where TConfig : Base.Dto.BaseConfigDto, new()
    {
        public string IdpTraceId { get; set; }
        public string ContextTraceId { get; set; }
        public bool Success { get; set; }
        public TData Data { get; set; }

        /// <summary>
        /// Data's Config related
        /// ex: Data=UserInfo, DataConfig=UserInfoConfig { UsrImagesUrl }
        /// </summary>
        public TConfig DataConfig { get; set; }
        public string Message { get; set; }

        public Exception Error { get; set; }
    }

}
