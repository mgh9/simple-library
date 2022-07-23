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
var UpdatableEntityController = (function (_super) {
    __extends(UpdatableEntityController, _super);
    function UpdatableEntityController($root, $scope, service, defaultSort, loadDataAfterInit) {
        if (loadDataAfterInit === void 0) { loadDataAfterInit = true; }
        var _this = _super.call(this, $root, $scope, service, defaultSort, loadDataAfterInit) || this;
        _this.$root = $root;
        _this.$scope = $scope;
        _this.service = service;
        _this.defaultSort = defaultSort;
        _this.loadDataAfterInit = loadDataAfterInit;
        var self = $scope.self = _this;
        self.editDialog = UIkit.modal(dialogsOptions.editDialogId, self.getModalOptionsForEditDialog());
        self.handleOnEditDialogClosed();
        return _this;
    }
    UpdatableEntityController.prototype.getModalOptionsForEditDialog = function () {
        return dialogsOptions.notClosable;
    };
    UpdatableEntityController.prototype.clear = function () {
        var self = this, $scope = self.$scope;
        _super.prototype.clear.call(this);
    };
    UpdatableEntityController.prototype.clearSearchFilters = function () {
        var self = this, $scope = self.$scope;
        _super.prototype.clearSearchFilters.call(this);
    };
    UpdatableEntityController.prototype.initSearchFilters = function () {
        var self = this, $scope = self.$scope;
        var theSearchFilter = _super.prototype.initSearchFilters.call(this);
        return theSearchFilter;
    };
    UpdatableEntityController.prototype.save = function () {
        var self = this, $scope = self.$scope, $root = self.$root, request = $scope.request, tableData = $scope.tableData, searchFilters = request.searchFilterModel;
        if ($root.isLoading)
            return;
        if (!self.validateOnSave())
            return;
        $scope.isFormActionExecuting = true;
        $root.showPreloader();
        self.service.save($scope.entity)
            .then(function (response) {
            var result = response.data;
            if (result.success) {
                if (result.message)
                    $root.notify(result.message, NotifyTypes.Success);
                else
                    $root.notifySuccessfullAction();
                self.loadData(request, undefined, true);
                self.editDialog.hide();
            }
            else {
                if (result.message) {
                    $root.notify(result.message, NotifyTypes.Warning);
                }
                else
                    $root.handleError(arguments);
            }
        }, $root.handleError)
            .finally(function () {
            $scope.isFormActionExecuting = false;
            $root.hidePreloader();
        });
    };
    UpdatableEntityController.prototype.validateOnSave = function () {
        var self = this, $scope = self.$scope, $root = self.$root;
        return true;
    };
    UpdatableEntityController.prototype.cancel = function ($event) {
        var self = this, $scope = self.$scope;
        $scope.tableData.rows.forEach(function (row) { row.isSelected = false; });
        self.editDialog.hide();
        $event.preventDefault();
        $event.stopPropagation();
    };
    UpdatableEntityController.prototype.delete = function ($event, row) {
        var self = this, $scope = self.$scope, $root = self.$root, tableData = $scope.tableData, request = $scope.request, searchFilters = $scope.request.searchFilterModel;
        row.isSelected = true;
        UIkit.modal.confirm(statics.deleteQuestion, function () {
            if ($root.isLoading)
                return;
            row.isLoading = true;
            self.service.delete(row.id)
                .then(function (response) {
                var result = response.data;
                if (result.success) {
                    self.loadData(request);
                    self.clear();
                    $root.notify(statics.deleteMessage);
                }
                else
                    $root.handleError(arguments);
            }, $root.handleError)
                .finally(function () {
                row.isLoading = false;
                row.isSelected = false;
            });
        }, function () {
            $scope.tableData.rows.forEach(function (row) { row.isSelected = false; });
        }, dialogsOptions.delete);
        $event && $event.preventDefault();
        $event && $event.stopPropagation();
    };
    UpdatableEntityController.prototype.add = function ($event) {
        var self = this, $scope = self.$scope;
        self.clear();
        self.editDialog.show();
        $scope.$broadcast('addDialogShown');
        $scope.$broadcast('addOrEditDialogShown');
        $event.preventDefault();
        $event.stopPropagation();
    };
    UpdatableEntityController.prototype.prepareDataOnEditDialogShown = function (data) {
        var self = this, $scope = self.$scope, $root = self.$root;
    };
    UpdatableEntityController.prototype.showEditDialog = function ($event, row) {
        var self = this, $scope = self.$scope, $root = self.$root, tableData = $scope.tableData;
        row.isLoading = true;
        $root.isActionExecuting = true;
        self.service.getById(row.id)
            .then(function (response) {
            var result = response.data;
            if (result.success) {
                self.prepareDataOnEditDialogShown(result.data);
                $scope.entity = result.data;
                $scope.originalEntity = angular.copy($scope.entity);
                row.isSelected = true;
                self.editDialog.show();
                $scope.$broadcast('editDialogShown');
                $scope.$broadcast('addOrEditDialogShown');
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
    UpdatableEntityController.prototype.handleOnEditDialogClosed = function () {
        var self = this, $scope = self.$scope, $root = self.$root;
        $('#edit-dialog').on('hide.uk.modal', function () {
            $scope.tableData.rows.forEach(function (row) { row.isSelected = false; });
        });
    };
    return UpdatableEntityController;
}(BaseEntityController));
//# sourceMappingURL=UpdatableEntityController.js.map