namespace FinLib.Pages.Shared {
    export class MainSidebarService extends BaseService {
        public static $inject = ['$http'];

        constructor(public $http: angular.IHttpService) {
            super();
        }

        public getMenuLinks(): IJsonPromis<IMenuLinkDto[]> {
            var self = this,
                $http = self.$http;

            return $http.get(`${self.apiPath}/MenuLink/GetMenus`);
        }
    }

    angular
        .module('altairApp')
        .service('MainSidebarService', MainSidebarService);
}