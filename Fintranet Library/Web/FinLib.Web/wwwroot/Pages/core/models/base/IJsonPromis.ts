type IJsonPromis<TData> = angular.IHttpPromise<IJsonResult<TData>>;

type IJsonWithoutDataPromis = angular.IHttpPromise<IJsonResultWithoutData>;

type IJsonWithConfigPromis<TData, TConfig> = angular.IHttpPromise<IJsonResultWithConfig<TData, TConfig>>;
