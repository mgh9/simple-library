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
        var Shared;
        (function (Shared) {
            var MainSidebarService = (function (_super) {
                __extends(MainSidebarService, _super);
                function MainSidebarService($http) {
                    var _this = _super.call(this) || this;
                    _this.$http = $http;
                    return _this;
                }
                MainSidebarService.prototype.getMenuLinks = function () {
                    var self = this, $http = self.$http;
                    return $http.get("".concat(self.apiPath, "/MenuLink/GetMenus"));
                };
                MainSidebarService.$inject = ['$http'];
                return MainSidebarService;
            }(BaseService));
            Shared.MainSidebarService = MainSidebarService;
            angular
                .module('altairApp')
                .service('MainSidebarService', MainSidebarService);
        })(Shared = Pages.Shared || (Pages.Shared = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=mainSidebarService.js.map