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
var BaseEntityController = (function (_super) {
    __extends(BaseEntityController, _super);
    function BaseEntityController($root, $scope, service, defaultSort, loadDataAfterInit) {
        if (loadDataAfterInit === void 0) { loadDataAfterInit = true; }
        var _this = _super.call(this, $root, $scope) || this;
        _this.$root = $root;
        _this.$scope = $scope;
        _this.service = service;
        _this.defaultSort = defaultSort;
        _this.loadDataAfterInit = loadDataAfterInit;
        var self = $scope.self = _this;
        $scope.isFilterActive = false;
        var preparedDefaultSort = self.getDefaultSortPrepared(defaultSort);
        $scope.request =
            {
                searchFilterModel: undefined,
                pageIndex: 0,
                pageSize: 10,
                pageOrder: preparedDefaultSort
            };
        $scope.request.searchFilterModel = undefined;
        $scope.tableData = {
            columns: [],
            rows: [],
            count: 0,
        };
        self.singleViewDialog = UIkit.modal(dialogsOptions.singleViewDialogId, dialogsOptions.notClosable);
        self.handleSingleViewDialogClosed();
        self.clear();
        if (loadDataAfterInit) {
            self.loadData($scope.request, true);
        }
        return _this;
    }
    BaseEntityController.prototype.init = function () {
        var self = this, $scope = self.$scope;
        $scope.request.searchFilterModel = self.initSearchFilters();
    };
    BaseEntityController.prototype.initSearchFilters = function () {
        var self = this, $scope = self.$scope;
        $scope.request.searchFilterModel = {};
        return $scope.request.searchFilterModel;
    };
    BaseEntityController.prototype.clear = function () {
        var self = this, $scope = self.$scope;
        $scope.entity = ({});
    };
    BaseEntityController.prototype.clearSearchFilters = function () {
        var self = this, $scope = self.$scope;
    };
    BaseEntityController.prototype.toggleFilter = function () {
        var self = this, $scope = self.$scope;
        $scope.isFilterActive = !$scope.isFilterActive;
        self.clearSearchFilters();
        if (!$scope.isFilterActive)
            self.filterTable();
    };
    BaseEntityController.prototype.filterTable = function () {
        var self = this, $scope = self.$scope;
        self.refreshGrid();
    };
    BaseEntityController.prototype.refreshGrid = function () {
        var self = this, $root = self.$root, $scope = self.$scope;
        self.loadData($scope.request);
    };
    BaseEntityController.prototype.getDefaultSortPrepared = function (defaultSort) {
        var self = this, $scope = self.$scope, $root = self.$root;
        if (!defaultSort)
            return '';
        var preparedDefaultSort = defaultSort.trim();
        var defaultSortNormalized = preparedDefaultSort.toUpperCase();
        if (defaultSortNormalized.indexOf(' ASC') < 0 && defaultSortNormalized.indexOf(' DESC') < 0) {
            preparedDefaultSort = defaultSort + ' asc';
        }
        preparedDefaultSort = preparedDefaultSort.charAt(0).toLocaleLowerCase() + preparedDefaultSort.substring(1, preparedDefaultSort.length);
        return preparedDefaultSort;
    };
    BaseEntityController.prototype.loadData = function (request, isInit, forceLoadData) {
        var self = this, $scope = self.$scope, $root = self.$root, tableData = $scope.tableData;
        if ($root.isLoading && !forceLoadData)
            return;
        $root.showPreloader();
        $root.isActionExecuting = true;
        self.service.get(request)
            .then(function (response) {
            var result = response.data;
            if (result.success) {
                $scope.tableData = result.data;
                $root.$broadcast('tableDataLoaded', { data: $scope.tableData.rows });
            }
            else
                $root.handleError(arguments);
        }, $root.handleError)
            .finally(function () {
            $root.isActionExecuting = false;
            $root.hidePreloader();
        });
    };
    BaseEntityController.prototype.prepareDataOnSingleViewDialogShown = function (data) {
    };
    BaseEntityController.prototype.showSingleViewDialog = function ($event, row) {
        var self = this, $scope = self.$scope, $root = self.$root, tableData = $scope.tableData;
        row.isLoading = true;
        $root.isActionExecuting = true;
        self.service.getAsViewById(row.id)
            .then(function (response) {
            var result = response.data;
            if (result.success) {
                self.prepareDataOnSingleViewDialogShown(result.data);
                $scope.singleViewModel = result.data;
                row.isSelected = true;
                self.singleViewDialog.show();
                $scope.$broadcast('singleViewDialogShown');
            }
            else
                $root.handleError(arguments);
        }, $root.handleError)
            .finally(function () {
            row.isLoading = false;
            $root.isActionExecuting = false;
        });
        $event && $event.preventDefault();
        $event && $event.stopPropagation();
    };
    BaseEntityController.prototype.closeSingleViewDialog = function ($event) {
        var self = this, $scope = self.$scope;
        $scope.tableData.rows.forEach(function (row) { row.isSelected = false; });
        self.singleViewDialog.hide();
        $event.preventDefault();
        $event.stopPropagation();
    };
    BaseEntityController.prototype.handleSingleViewDialogClosed = function () {
        var self = this, $scope = self.$scope, $root = self.$root;
        $('#single-view-dialog').on('hide.uk.modal', function () {
            $scope.tableData.rows.forEach(function (row) { row.isSelected = false; });
        });
    };
    return BaseEntityController;
}(BaseController));
//# sourceMappingURL=BaseEntityController.js.map