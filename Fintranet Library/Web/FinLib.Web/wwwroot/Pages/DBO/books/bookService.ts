namespace FinLib.Pages.DBO {
    export class BookService extends GeneralEntityService<IBookDto, IBookView, IBookSearchFilter> {
        public static $inject = ['$http'];

        constructor($http: angular.IHttpService) {
            super($http, 'Book');
        }
    }

    angular
        .module('altairApp')
        .service('BookService', BookService);
}