namespace FinLib.Pages.Shared {
    export class HeaderService extends BaseService {
        public static $inject = ['$http'];

        constructor(public $http: angular.IHttpService) {
            super();
        }

        public setDefaultUserRole(newUserRoleId: number): IJsonWithoutDataPromis {
            var self = this,
                $http = self.$http;

            return $http.post(`${self.apiPath}/Account/SetDefaultUserRole`, newUserRoleId);
        }

        public logout(): IJsonPromis<string> {
            var self = this,
                $http = self.$http;

            return $http.get(`/Account/Logout`, undefined);
        }
    }

    angular
        .module('altairApp')
        .service('HeaderService', HeaderService);
}