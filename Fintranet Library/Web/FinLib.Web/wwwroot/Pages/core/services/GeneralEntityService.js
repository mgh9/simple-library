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
var GeneralEntityService = (function (_super) {
    __extends(GeneralEntityService, _super);
    function GeneralEntityService($http, webAPIController) {
        var _this = _super.call(this, $http, webAPIController) || this;
        _this.$http = $http;
        _this.webAPIController = webAPIController;
        var self = _this;
        self.webAPIController = "".concat(self.apiPath, "/").concat(self.webAPIController);
        return _this;
    }
    GeneralEntityService.prototype.getGeneralEntitiesTitleValueList = function (includeEmptySelector, includeSelectAllSelector, onlyActives, text) {
        var self = this, $http = self.$http;
        return $http.get("".concat(self.webAPIController, "/GetGeneralEntitiesTitleValueList"), {
            params: { includeEmptySelector: includeEmptySelector, includeSelectAllSelector: includeSelectAllSelector, onlyActives: onlyActives, text: text }
        });
    };
    return GeneralEntityService;
}(UpdatableEntityService));
//# sourceMappingURL=GeneralEntityService.js.map