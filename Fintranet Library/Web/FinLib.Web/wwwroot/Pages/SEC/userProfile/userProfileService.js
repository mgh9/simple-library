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
            var UserProfileService = (function (_super) {
                __extends(UserProfileService, _super);
                function UserProfileService($http) {
                    var _this = _super.call(this) || this;
                    _this.$http = $http;
                    return _this;
                }
                UserProfileService.prototype.get = function () {
                    var self = this, $http = self.$http;
                    return $http.get("".concat(self.apiPath, "/UserProfile/Get"));
                };
                UserProfileService.prototype.saveData = function (data) {
                    var self = this, $http = self.$http;
                    return $http.put("".concat(self.apiPath, "/UserProfile/Update"), data);
                };
                UserProfileService.prototype.changePassword = function (model) {
                    var self = this, $http = self.$http;
                    return $http.put("".concat(self.apiPath, "/UserProfile/ChangePassword"), model);
                };
                UserProfileService.$inject = ['$http'];
                return UserProfileService;
            }(BaseService));
            SEC.UserProfileService = UserProfileService;
            angular
                .module('altairApp')
                .service('UserProfileService', UserProfileService);
        })(SEC = Pages.SEC || (Pages.SEC = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=userProfileService.js.map