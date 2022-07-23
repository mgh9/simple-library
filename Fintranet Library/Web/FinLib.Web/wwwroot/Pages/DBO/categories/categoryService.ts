namespace FinLib.Pages.DBO {
    export class CategoryService extends GeneralEntityService<ICategoryDto, ICategoryView, IGeneralEntitySearchFilter> {
        public static $inject = ['$http'];

        constructor($http: angular.IHttpService) {
            super($http, 'Category');
        }
    }

    angular
        .module('altairApp')
        .service('CategoryService', CategoryService);
}