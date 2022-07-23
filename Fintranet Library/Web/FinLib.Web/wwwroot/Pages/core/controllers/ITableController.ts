interface ITableController<TSearchFilter extends IBaseEntitySearchFilter> extends IBaseController {
    loadData(request: IGetRequestDto<TSearchFilter>, isInit?: boolean);
}