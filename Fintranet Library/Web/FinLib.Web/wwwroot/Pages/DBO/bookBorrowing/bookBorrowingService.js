var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var FinLib;
(function (FinLib) {
    var Pages;
    (function (Pages) {
        var DBO;
        (function (DBO) {
            var BookBorrowingService = (function (_super) {
                __extends(BookBorrowingService, _super);
                function BookBorrowingService($http) {
                    return _super.call(this, $http, 'BookBorrowing') || this;
                }
                BookBorrowingService.prototype.returnBook = function (model) {
                    var self = this, $http = self.$http;
                    return $http.post("".concat(self.webAPIController, "/ReturnBook"), model);
                };
                BookBorrowingService.$inject = ['$http'];
                return BookBorrowingService;
            }(UpdatableEntityService));
            DBO.BookBorrowingService = BookBorrowingService;
            angular
                .module('altairApp')
                .service('BookBorrowingService', BookBorrowingService);
        })(DBO = Pages.DBO || (Pages.DBO = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=bookBorrowingService.js.map