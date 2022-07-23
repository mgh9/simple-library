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
var UpdatableEntityService = (function (_super) {
    __extends(UpdatableEntityService, _super);
    function UpdatableEntityService($http, webAPIController) {
        var _this = _super.call(this, $http, webAPIController) || this;
        _this.$http = $http;
        _this.webAPIController = webAPIController;
        var self = _this;
        self.webAPIController = "".concat(self.apiPath, "/").concat(self.webAPIController);
        return _this;
    }
    UpdatableEntityService.prototype.delete = function (id) {
        var self = this, $http = self.$http;
        return $http.delete("".concat(self.webAPIController, "/Delete/").concat(id));
    };
    UpdatableEntityService.prototype.save = function (entity) {
        var self = this;
        if (!entity.id || entity.id <= 0)
            return self.insert(entity);
        else
            return self.update(entity);
    };
    UpdatableEntityService.prototype.insert = function (entity) {
        var self = this, $http = self.$http;
        return $http.post("".concat(self.webAPIController, "/Insert"), entity);
    };
    UpdatableEntityService.prototype.update = function (entity) {
        var self = this, $http = self.$http;
        return $http.put("".concat(self.webAPIController, "/Update"), entity);
    };
    return UpdatableEntityService;
}(BaseEntityService));
//# sourceMappingURL=UpdatableEntityService.js.map