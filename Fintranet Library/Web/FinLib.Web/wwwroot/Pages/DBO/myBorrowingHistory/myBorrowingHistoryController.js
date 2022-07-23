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
            var MyBorrowingHistoryController = (function (_super) {
                __extends(MyBorrowingHistoryController, _super);
                function MyBorrowingHistoryController($root, $scope, $http, bookService, userService, myBorrowingHistoryService) {
                    var _this = _super.call(this, $root, $scope, myBorrowingHistoryService, 'id desc') || this;
                    _this.$root = $root;
                    _this.$scope = $scope;
                    _this.$http = $http;
                    _this.bookService = bookService;
                    _this.userService = userService;
                    _this.myBorrowingHistoryService = myBorrowingHistoryService;
                    _this.init();
                    return _this;
                }
                MyBorrowingHistoryController.prototype.init = function () {
                    _super.prototype.init.call(this);
                    var self = this, $scope = self.$scope, $root = self.$root;
                    self.initCustomColumns();
                    self.initSearchFilters();
                    $scope.books = [];
                    $scope.booksConfig = angular.extend({}, $root.selectizeConfig, {
                        placeholder: 'Book...'
                    });
                    $scope.isFilterActive = true;
                };
                MyBorrowingHistoryController.prototype.initCustomColumns = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    $scope.gridViewConfig = {
                        elementId: 'my_borrowing_history_grid',
                        showCheckbox: false,
                        deleteLinkVisible: false,
                        editLinkVisible: false,
                        singleViewLinkVisible: false,
                        lastUpdateTimeVisible: false,
                    };
                };
                MyBorrowingHistoryController.prototype.initSearchFilters = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    var theSearchFilter = _super.prototype.initSearchFilters.call(this);
                    $scope.booksForSearchFilter = [];
                    $scope.booksForSearchFilterConfig = angular.extend({}, $root.selectizeConfig, {
                        placeholder: 'Book...'
                    });
                    self.bookService.getTitleValueList()
                        .then(function (response) {
                        var result = response.data;
                        if (result.success) {
                            self.$scope.booksForSearchFilter = result.data;
                            self.$scope.booksForSearchFilter.unshift({
                                value: -1,
                                title: '«All Books»'
                            });
                        }
                        else
                            self.$root.handleError(arguments);
                    }, self.$root.handleError);
                    theSearchFilter.bookId = {
                        value: undefined,
                        value2: undefined,
                        isIgnore: undefined,
                        conditionType: ConditionType.Equals,
                        columnName: undefined,
                        valueType: ValueType.Number
                    };
                    return theSearchFilter;
                };
                MyBorrowingHistoryController.prototype.clear = function () {
                    var self = this, $scope = self.$scope;
                    _super.prototype.clear.call(this);
                };
                MyBorrowingHistoryController.prototype.clearSearchFilters = function () {
                    var self = this, $scope = self.$scope;
                    _super.prototype.clearSearchFilters.call(this);
                    $scope.request.searchFilterModel.bookId.value = undefined;
                };
                MyBorrowingHistoryController.$inject = ['$rootScope', '$scope', '$http', 'BookService', 'UserService', 'MyBorrowingHistoryService'];
                return MyBorrowingHistoryController;
            }(BaseEntityController));
            DBO.MyBorrowingHistoryController = MyBorrowingHistoryController;
            angular
                .module('altairApp')
                .controller('MyBorrowingHistoryController', MyBorrowingHistoryController);
        })(DBO = Pages.DBO || (Pages.DBO = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=myBorrowingHistoryController.js.map