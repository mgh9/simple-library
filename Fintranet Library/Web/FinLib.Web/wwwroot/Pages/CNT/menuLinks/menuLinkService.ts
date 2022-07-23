namespace FinLib.Pages.CNT {
    export class MenuLinkService extends GeneralEntityService<IMenuLinkDto, IMenuLinkView, IMenuLinkSearchFilter> {
        public static $inject = ['$http'];

        constructor($http: angular.IHttpService) {
            super($http, 'MenuLink');
        }
    }

    angular
        .module('altairApp')
        .service('MenuLinkService', MenuLinkService);
}