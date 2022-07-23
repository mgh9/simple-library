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
            var CategoryController = (function (_super) {
                __extends(CategoryController, _super);
                function CategoryController($root, $scope, $http, categoryService) {
                    var _this = _super.call(this, $root, $scope, categoryService) || this;
                    _this.$root = $root;
                    _this.$scope = $scope;
                    _this.$http = $http;
                    _this.categoryService = categoryService;
                    _this.init();
                    return _this;
                }
                CategoryController.prototype.init = function () {
                    _super.prototype.init.call(this);
                    var self = this, $scope = self.$scope, $root = self.$root;
                    $scope.$on('editDialogShown', function () {
                    });
                };
                CategoryController.prototype.clear = function () {
                    var self = this, $scope = self.$scope;
                    _super.prototype.clear.call(this);
                };
                CategoryController.$inject = ['$rootScope', '$scope', '$http', 'CategoryService'];
                return CategoryController;
            }(GeneralEntityController));
            DBO.CategoryController = CategoryController;
            angular
                .module('altairApp')
                .controller('CategoryController', CategoryController);
        })(DBO = Pages.DBO || (Pages.DBO = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=categoryController.js.map