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
        var SEC;
        (function (SEC) {
            var RoleService = (function (_super) {
                __extends(RoleService, _super);
                function RoleService($http) {
                    return _super.call(this, $http, 'Role') || this;
                }
                RoleService.prototype.getRolesTitleValueList = function (text, includeEmptySelector, includeSelectAllSelector) {
                    var self = this, $http = self.$http;
                    return $http.get("".concat(self.webAPIController, "/GetTitleValueList"), {
                        params: { text: text, includeEmptySelector: includeEmptySelector, includeSelectAllSelector: includeSelectAllSelector }
                    });
                };
                RoleService.$inject = ['$http'];
                return RoleService;
            }(BaseEntityService));
            SEC.RoleService = RoleService;
            angular
                .module('altairApp')
                .service('RoleService', RoleService);
        })(SEC = Pages.SEC || (Pages.SEC = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=roleService.js.map