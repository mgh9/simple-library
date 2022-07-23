interface IJsonResult<T> {
    success: boolean;
    message: string;
    data: T;
    contextTraceId: string;
    idpTraceId : string;
    error: IException;
}

interface IJsonResultWithConfig<TData, TConfig> {
    success: boolean;
    message: string;
    data: TData;
    dataConfig: TConfig;
    contextTraceId: string;
    idpTraceId : string;
    error: IException;
}

interface IJsonResultWithoutData {
    success: boolean;
    message: string;
    contextTraceId: string;
    idpTraceId : string;
    error: IException;
}
