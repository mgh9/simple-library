namespace FinLib.Pages.SEC {
    export class RoleService extends BaseEntityService<IRoleDto, IRoleView, IBaseEntitySearchFilter> {
        public static $inject = ['$http'];

        constructor($http: angular.IHttpService) {
            super($http, 'Role');
        }

        public getRolesTitleValueList(text?: string, includeEmptySelector?: boolean, includeSelectAllSelector?: boolean) {
            var self = this,
                $http = self.$http;

            return $http.get<IJsonResult<ITitleValue<number>[]>>(`${self.webAPIController}/GetTitleValueList`, {
                params: { text: text, includeEmptySelector: includeEmptySelector, includeSelectAllSelector: includeSelectAllSelector }
            });
        }
    }

    angular
        .module('altairApp')
        .service('RoleService', RoleService);
}