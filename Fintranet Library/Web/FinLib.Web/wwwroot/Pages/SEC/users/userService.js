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
            var UserService = (function (_super) {
                __extends(UserService, _super);
                function UserService($http) {
                    return _super.call(this, $http, 'User') || this;
                }
                UserService.prototype.getUsersTitleValueList = function (text, includeEmptySelector, includeSelectAllSelector) {
                    var self = this, $http = self.$http;
                    return $http.get("".concat(self.webAPIController, "/GetTitleValueList"), {
                        params: (includeEmptySelector ? { includeEmptySelector: includeEmptySelector } : null)
                    });
                };
                UserService.prototype.getCustomersUserRoleIdsTitleValueList = function () {
                    var self = this, $http = self.$http;
                    return $http.get("".concat(self.webAPIController, "/GetCustomersUserRoleIdsTitleValueList"), {});
                };
                UserService.prototype.removeUserRole = function (userRoleId) {
                    var self = this, $http = self.$http;
                    return $http.delete("".concat(self.webAPIController, "/DeleteUserRole"), { params: { userRoleId: userRoleId } });
                };
                UserService.prototype.getPasswordPolicy = function () {
                    var self = this, $http = self.$http;
                    return $http.get("".concat(self.webAPIController, "/GetPasswordPolicy"));
                };
                UserService.prototype.resetPassword = function (model) {
                    var self = this, $http = self.$http;
                    return $http.post("".concat(self.webAPIController, "/ResetPassword"), model);
                };
                UserService.prototype.saveUserRoles = function (model) {
                    var self = this, $http = self.$http;
                    return $http.put("".concat(self.webAPIController, "/SaveUserRoles"), model);
                };
                UserService.$inject = ['$http'];
                return UserService;
            }(UpdatableEntityService));
            SEC.UserService = UserService;
            angular
                .module('altairApp')
                .service('UserService', UserService);
        })(SEC = Pages.SEC || (Pages.SEC = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=userService.js.map