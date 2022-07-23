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
            var BookController = (function (_super) {
                __extends(BookController, _super);
                function BookController($root, $scope, $http, categoryService, bookService) {
                    var _this = _super.call(this, $root, $scope, bookService, "id") || this;
                    _this.$root = $root;
                    _this.$scope = $scope;
                    _this.$http = $http;
                    _this.categoryService = categoryService;
                    _this.bookService = bookService;
                    _this.init();
                    return _this;
                }
                BookController.prototype.init = function () {
                    _super.prototype.init.call(this);
                    var self = this, $scope = self.$scope, $root = self.$root;
                    self.$scope.categories = [];
                    $scope.categoriesConfig = angular.extend({}, $root.selectizeConfig, {
                        placeholder: 'Category...'
                    });
                    $scope.$on('addOrEditDialogShown', function () {
                        self.categoryService.getTitleValueList()
                            .then(function (response) {
                            var result = response.data;
                            if (result.success) {
                                $scope.categories = result.data;
                            }
                            else
                                self.$root.handleError(arguments);
                        }, self.$root.handleError);
                    });
                };
                BookController.prototype.initSearchFilters = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    var theSearchFilter = _super.prototype.initSearchFilters.call(this);
                    self.$scope.categoriesForSearchFilter = [];
                    $scope.categoriesForSearchFilterConfig = angular.extend({}, $root.selectizeConfig, {
                        placeholder: 'Category...'
                    });
                    self.categoryService.getTitleValueList()
                        .then(function (response) {
                        var result = response.data;
                        if (result.success) {
                            self.$scope.categoriesForSearchFilter = result.data;
                            self.$scope.categoriesForSearchFilter.unshift({
                                value: -1,
                                title: '«All Categories»'
                            });
                        }
                        else
                            self.$root.handleError(arguments);
                    }, self.$root.handleError);
                    theSearchFilter.categoryId = {
                        value: undefined,
                        value2: undefined,
                        isIgnore: undefined,
                        conditionType: ConditionType.Equals,
                        columnName: undefined,
                        valueType: ValueType.Number
                    };
                    return theSearchFilter;
                };
                BookController.prototype.clear = function () {
                    var self = this, $scope = self.$scope;
                    _super.prototype.clear.call(this);
                };
                BookController.prototype.clearSearchFilters = function () {
                    var self = this, $scope = self.$scope;
                    _super.prototype.clearSearchFilters.call(this);
                    $scope.request.searchFilterModel.categoryId.value = undefined;
                };
                BookController.prototype.validateOnSave = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    if (!$scope.entity.categoryId) {
                        $root.notify('Please select the category', NotifyTypes.Warning);
                        return false;
                    }
                    return _super.prototype.validateOnSave.call(this);
                };
                BookController.$inject = ['$rootScope', '$scope', '$http', 'CategoryService', 'BookService'];
                return BookController;
            }(GeneralEntityController));
            DBO.BookController = BookController;
            angular
                .module('altairApp')
                .controller('BookController', BookController);
        })(DBO = Pages.DBO || (Pages.DBO = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=bookController.js.map