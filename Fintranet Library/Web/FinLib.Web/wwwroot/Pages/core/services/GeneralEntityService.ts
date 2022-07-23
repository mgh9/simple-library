class GeneralEntityService<TGeneralDto extends IGeneralDto
    , TGeneralView extends IGeneralView
    , TGeneralDtoSearchFilter extends IGeneralEntitySearchFilter>
    extends UpdatableEntityService<TGeneralDto, TGeneralView, TGeneralDtoSearchFilter>{
    constructor(public $http: angular.IHttpService,
        protected webAPIController: string) {
        super($http, webAPIController);
        var self = this;

        self.webAPIController = `${self.apiPath}/${self.webAPIController}`;
    }

    public getGeneralEntitiesTitleValueList(includeEmptySelector?: boolean, includeSelectAllSelector?: boolean, onlyActives?: boolean, text?: string): IJsonPromis<ITitleValue<number>[]> {
        var self = this,
            $http = self.$http;

        return $http.get<IJsonResult<ITitleValue<number>[]>>(`${self.webAPIController}/GetGeneralEntitiesTitleValueList`, {
            params: { includeEmptySelector: includeEmptySelector, includeSelectAllSelector: includeSelectAllSelector, onlyActives: onlyActives, text: text }
        });
    }
}
