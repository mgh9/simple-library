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
        var DashboardService = (function (_super) {
            __extends(DashboardService, _super);
            function DashboardService($http) {
                var _this = _super.call(this) || this;
                _this.$http = $http;
                return _this;
            }
            DashboardService.$inject = ['$http'];
            return DashboardService;
        }(BaseService));
        Pages.DashboardService = DashboardService;
        angular
            .module('altairApp')
            .service('DashboardService', DashboardService);
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=dashboardService.js.map