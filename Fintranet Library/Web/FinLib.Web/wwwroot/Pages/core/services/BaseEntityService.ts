class BaseEntityService<TDto extends IBaseDto
    , TViewEntity extends IBaseView
    , TSearchFilter extends IBaseEntitySearchFilter>
    extends BaseService {
    constructor(public $http: angular.IHttpService,
        protected webAPIController: string) {
        super();
        var self = this;

        self.webAPIController = `${self.apiPath}/${self.webAPIController}`;
    }

    public get(request: IGetRequestDto<TSearchFilter>): IJsonPromis<ITableData<TDto>> {
        var self = this,
            $http = self.$http;

        return $http.post(`${self.webAPIController}/Get`, request );
    }

    public getTitleValueList(includeEmptySelector?: boolean, includeSelectAllSelector?: boolean, text?: string): IJsonPromis<ITitleValue<number>[]> {
        var self = this,
            $http = self.$http;

        return $http.get<IJsonResult<ITitleValue<number>[]>>(`${self.webAPIController}/GetTitleValueList`, {
            params: { includeEmptySelector: includeEmptySelector, includeSelectAllSelector: includeSelectAllSelector, text: text }
        });
    }

    public getById(id: number): IJsonPromis<TDto> {
        var self = this,
            $http = self.$http;

        return $http.get(`${self.webAPIController}/GetById/${id}`);
    }

    public getAsViewById(id: number): IJsonPromis<TViewEntity> {
        var self = this,
            $http = self.$http;

        return $http.get(`${self.webAPIController}/GetAsViewById/${id}`);
    }
}
