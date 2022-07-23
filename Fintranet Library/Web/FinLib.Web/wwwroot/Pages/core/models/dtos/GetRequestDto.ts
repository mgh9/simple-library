




interface IGetRequestDto <TSearchFilter> extends IBaseDto 
{   
    pageIndex: number;
    pageSize: number;
    pageOrder: string;
    searchFilterModel: TSearchFilter;
}