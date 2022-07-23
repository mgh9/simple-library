class UpdatableEntityService<TUpdatableDto extends IUpdatableDto
    , TUpdatableView extends IUpdatableView
    , TUpdatableDtoSearchFilter extends IUpdatableEntitySearchFilter>
    extends BaseEntityService<TUpdatableDto, TUpdatableView, TUpdatableDtoSearchFilter>{
    constructor(public $http: angular.IHttpService,
        protected webAPIController: string) {
        super($http, webAPIController);
        var self = this;

        self.webAPIController = `${self.apiPath}/${self.webAPIController}`;
    }

    public delete(id: number): IJsonWithoutDataPromis {
        var self = this,
            $http = self.$http;

        return $http.delete(`${self.webAPIController}/Delete/${id}`);
    }

    public save(entity: TUpdatableDto): IJsonWithoutDataPromis {
        var self = this;

        if (!entity.id || entity.id <= 0)
            return self.insert(entity);
        else
            return self.update(entity);
    }

    public insert(entity: TUpdatableDto): IJsonWithoutDataPromis {
        var self = this,
            $http = self.$http;

        return $http.post(`${self.webAPIController}/Insert`, entity);
    }

    public update(entity: TUpdatableDto): IJsonWithoutDataPromis {
        var self = this,
            $http = self.$http;

        return $http.put(`${self.webAPIController}/Update`, entity);
    }
}
