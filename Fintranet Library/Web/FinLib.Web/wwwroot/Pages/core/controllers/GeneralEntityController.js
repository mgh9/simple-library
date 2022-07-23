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
var GeneralEntityController = (function (_super) {
    __extends(GeneralEntityController, _super);
    function GeneralEntityController($root, $scope, service, defaultSort, loadDataAfterInit) {
        if (loadDataAfterInit === void 0) { loadDataAfterInit = true; }
        var _this = _super.call(this, $root, $scope, service, (defaultSort || 'title asc'), loadDataAfterInit) || this;
        _this.$root = $root;
        _this.$scope = $scope;
        _this.service = service;
        _this.defaultSort = defaultSort;
        _this.loadDataAfterInit = loadDataAfterInit;
        return _this;
    }
    GeneralEntityController.prototype.clear = function () {
        var self = this, $scope = self.$scope;
        _super.prototype.clear.call(this);
        $scope.entity.title = '';
        $scope.entity.description = '';
        $scope.entity.isActive = true;
    };
    GeneralEntityController.prototype.clearSearchFilters = function () {
        var self = this, $scope = self.$scope;
        $scope.request.searchFilterModel.title.value = undefined;
        $scope.request.searchFilterModel.isActive.value = undefined;
        _super.prototype.clearSearchFilters.call(this);
    };
    GeneralEntityController.prototype.initSearchFilters = function () {
        var self = this, $scope = self.$scope;
        var theSearchFilter = _super.prototype.initSearchFilters.call(this);
        theSearchFilter.title = {
            columnName: undefined,
            value: undefined,
            value2: undefined,
            isIgnore: undefined,
            valueType: ValueType.Text,
            conditionType: ConditionType.Contains
        };
        theSearchFilter.isActive = {
            columnName: undefined,
            value: undefined,
            value2: undefined,
            isIgnore: undefined,
            valueType: ValueType.Boolean,
            conditionType: ConditionType.Equals
        };
        return theSearchFilter;
    };
    GeneralEntityController.prototype.validateOnSave = function () {
        var self = this, $scope = self.$scope, $root = self.$root;
        if (!$scope.entity.title) {
            $root.notify('Please enter Title', NotifyTypes.Warning);
            return false;
        }
        return _super.prototype.validateOnSave.call(this);
    };
    return GeneralEntityController;
}(UpdatableEntityController));
//# sourceMappingURL=GeneralEntityController.js.map