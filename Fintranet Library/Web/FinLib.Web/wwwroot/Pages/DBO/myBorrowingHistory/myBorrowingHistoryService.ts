namespace FinLib.Pages.DBO {
    export class MyBorrowingHistoryService extends BaseEntityService<IBookBorrowingDto, IMyBorrowingHistoryView, IMyBorrowingHistorySearchFilter> {
        public static $inject = ['$http'];

        constructor($http: angular.IHttpService) {
            super($http, 'MyBorrowingHistory');
        }
    }

    angular
        .module('altairApp')
        .service('MyBorrowingHistoryService', MyBorrowingHistoryService);
}