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
var BaseEntityService = (function (_super) {
    __extends(BaseEntityService, _super);
    function BaseEntityService($http, webAPIController) {
        var _this = _super.call(this) || this;
        _this.$http = $http;
        _this.webAPIController = webAPIController;
        var self = _this;
        self.webAPIController = "".concat(self.apiPath, "/").concat(self.webAPIController);
        return _this;
    }
    BaseEntityService.prototype.get = function (request) {
        var self = this, $http = self.$http;
        return $http.post("".concat(self.webAPIController, "/Get"), request);
    };
    BaseEntityService.prototype.getTitleValueList = function (includeEmptySelector, includeSelectAllSelector, text) {
        var self = this, $http = self.$http;
        return $http.get("".concat(self.webAPIController, "/GetTitleValueList"), {
            params: { includeEmptySelector: includeEmptySelector, includeSelectAllSelector: includeSelectAllSelector, text: text }
        });
    };
    BaseEntityService.prototype.getById = function (id) {
        var self = this, $http = self.$http;
        return $http.get("".concat(self.webAPIController, "/GetById/").concat(id));
    };
    BaseEntityService.prototype.getAsViewById = function (id) {
        var self = this, $http = self.$http;
        return $http.get("".concat(self.webAPIController, "/GetAsViewById/").concat(id));
    };
    return BaseEntityService;
}(BaseService));
//# sourceMappingURL=BaseEntityService.js.map