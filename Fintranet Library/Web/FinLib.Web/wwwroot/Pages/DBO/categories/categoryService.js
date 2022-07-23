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
            var CategoryService = (function (_super) {
                __extends(CategoryService, _super);
                function CategoryService($http) {
                    return _super.call(this, $http, 'Category') || this;
                }
                CategoryService.$inject = ['$http'];
                return CategoryService;
            }(GeneralEntityService));
            DBO.CategoryService = CategoryService;
            angular
                .module('altairApp')
                .service('CategoryService', CategoryService);
        })(DBO = Pages.DBO || (Pages.DBO = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=categoryService.js.map