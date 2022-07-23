namespace FinLib.Pages {
    export class DashboardService extends BaseService {
        public static $inject = ['$http'];

        constructor(private $http: angular.IHttpService) {
            super();
        }
    }

    angular
        .module('altairApp')
        .service('DashboardService', DashboardService);
}