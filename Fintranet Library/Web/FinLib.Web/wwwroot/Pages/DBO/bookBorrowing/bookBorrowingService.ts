namespace FinLib.Pages.DBO {
    export class BookBorrowingService extends UpdatableEntityService<IBookBorrowingDto, IBookBorrowingView, IBookBorrowingSearchFilter> {
        public static $inject = ['$http'];

        constructor($http: angular.IHttpService) {
            super($http, 'BookBorrowing');
        }

        public returnBook(model: IReturnBookDto): IJsonWithoutDataPromis {
            var self = this,
                $http = self.$http;

            return $http.post(`${self.webAPIController}/ReturnBook`, model);
        }
    }

    angular
        .module('altairApp')
        .service('BookBorrowingService', BookBorrowingService);
}