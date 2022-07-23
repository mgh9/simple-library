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
            var BookBorrowingController = (function (_super) {
                __extends(BookBorrowingController, _super);
                function BookBorrowingController($root, $scope, $http, bookService, userService, bookBorrowingService) {
                    var _this = _super.call(this, $root, $scope, bookBorrowingService, 'id desc') || this;
                    _this.$root = $root;
                    _this.$scope = $scope;
                    _this.$http = $http;
                    _this.bookService = bookService;
                    _this.userService = userService;
                    _this.bookBorrowingService = bookBorrowingService;
                    _this.init();
                    return _this;
                }
                BookBorrowingController.prototype.init = function () {
                    _super.prototype.init.call(this);
                    var self = this, $scope = self.$scope, $root = self.$root;
                    self.initCustomColumns();
                    self.initSearchFilters();
                    $scope.books = [];
                    $scope.booksConfig = angular.extend({}, $root.selectizeConfig, {
                        placeholder: 'Book...'
                    });
                    $scope.customers = [];
                    $scope.customersConfig = angular.extend({}, $root.selectizeConfig, {
                        placeholder: 'Customer...'
                    });
                    $scope.isFilterActive = true;
                    $scope.$on('addOrEditDialogShown', function () {
                        self.bookService.getTitleValueList()
                            .then(function (response) {
                            var result = response.data;
                            if (result.success) {
                                $scope.books = result.data;
                            }
                            else
                                self.$root.handleError(arguments);
                        }, self.$root.handleError);
                        self.userService.getCustomersUserRoleIdsTitleValueList()
                            .then(function (response) {
                            var result = response.data;
                            if (result.success) {
                                $scope.customers = result.data;
                            }
                            else
                                self.$root.handleError(arguments);
                        }, self.$root.handleError);
                    });
                };
                BookBorrowingController.prototype.initCustomColumns = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    $scope.gridViewConfig = {
                        elementId: 'books_borrowing_grid',
                        showCheckbox: false,
                        deleteLinkVisible: false,
                        editLinkVisible: true,
                        singleViewLinkVisible: false,
                        lastUpdateTimeVisible: true,
                        customActions: [
                            {
                                click: function (event, row) {
                                    self.returnBook(event, row);
                                },
                                icon: "assignment_turned_in",
                                title: "Return Book",
                                color: "md-color-green-500",
                                tooltip: "Mark the selected Borrowing row as returned"
                            },
                        ]
                    };
                };
                BookBorrowingController.prototype.initSearchFilters = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    var theSearchFilter = _super.prototype.initSearchFilters.call(this);
                    $scope.booksForSearchFilter = [];
                    $scope.booksForSearchFilterConfig = angular.extend({}, $root.selectizeConfig, {
                        placeholder: 'Book...'
                    });
                    $scope.customersForSearchFilter = [];
                    $scope.customersForSearchFilterConfig = angular.extend({}, $root.selectizeConfig, {
                        placeholder: 'Customer...'
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
                    self.userService.getCustomersUserRoleIdsTitleValueList()
                        .then(function (response) {
                        var result = response.data;
                        if (result.success) {
                            self.$scope.customersForSearchFilter = result.data;
                            self.$scope.customersForSearchFilter.unshift({
                                value: -1,
                                title: '«All Customers»'
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
                    theSearchFilter.customerUserRoleId = {
                        value: undefined,
                        value2: undefined,
                        isIgnore: undefined,
                        conditionType: ConditionType.Equals,
                        columnName: undefined,
                        valueType: ValueType.Number
                    };
                    return theSearchFilter;
                };
                BookBorrowingController.prototype.clear = function () {
                    var self = this, $scope = self.$scope;
                    _super.prototype.clear.call(this);
                };
                BookBorrowingController.prototype.returnBook = function ($event, row) {
                    var self = this, $scope = self.$scope, $root = self.$root, tableData = $scope.tableData, request = $scope.request;
                    UIkit.modal.confirm("Are u sure to return the seleceted book?", function () {
                        var model = { bookBorrowingId: row.id };
                        self.bookBorrowingService.returnBook(model)
                            .then(function (response) {
                            var result = response.data;
                            if (result.success) {
                                self.loadData(request);
                                $root.notify("The selected book returned successfully", NotifyTypes.Success);
                            }
                            else {
                                if (result.message) {
                                    $root.notify(result.message, NotifyTypes.Warning);
                                }
                                else
                                    $root.handleError(arguments);
                            }
                        }, $root.handleError)
                            .finally(function () {
                            $root.hidePreloader();
                        });
                    }, dialogsOptions.confirm);
                };
                BookBorrowingController.prototype.clearSearchFilters = function () {
                    var self = this, $scope = self.$scope;
                    _super.prototype.clearSearchFilters.call(this);
                    $scope.request.searchFilterModel.bookId.value = undefined;
                    $scope.request.searchFilterModel.customerUserRoleId.value = undefined;
                };
                BookBorrowingController.prototype.validateOnSave = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    if (!$scope.entity.bookId) {
                        $root.notify('Please select a book', NotifyTypes.Warning);
                        return false;
                    }
                    if (!$scope.entity.customerUserRoleId) {
                        $root.notify('Please select a customer', NotifyTypes.Warning);
                        return false;
                    }
                    return _super.prototype.validateOnSave.call(this);
                };
                BookBorrowingController.$inject = ['$rootScope', '$scope', '$http', 'BookService', 'UserService', 'BookBorrowingService'];
                return BookBorrowingController;
            }(UpdatableEntityController));
            DBO.BookBorrowingController = BookBorrowingController;
            angular
                .module('altairApp')
                .controller('BookBorrowingController', BookBorrowingController);
        })(DBO = Pages.DBO || (Pages.DBO = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=bookBorrowingController.js.map