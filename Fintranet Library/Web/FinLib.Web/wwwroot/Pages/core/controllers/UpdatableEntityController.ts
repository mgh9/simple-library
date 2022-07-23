class UpdatableEntityController<TUpdatableDto extends IUpdatableDto
    , TUpdatableView extends IUpdatableView
    , TUpdatableDtoSearchFilter extends IUpdatableEntitySearchFilter>
    extends BaseEntityController<TUpdatableDto, TUpdatableView, TUpdatableDtoSearchFilter> {
    public editDialog: UIkit.ModalElement;

    constructor(public $root: IRootScope,
        public $scope: ITableBaseScope<UpdatableEntityController<TUpdatableDto, TUpdatableView, TUpdatableDtoSearchFilter>, TUpdatableDto, TUpdatableView, TUpdatableDtoSearchFilter>,
        public service: any,
        public defaultSort?: string,
        public loadDataAfterInit: boolean = true) {

        super($root, $scope, service, defaultSort, loadDataAfterInit);

        var self = $scope.self = this;

        self.editDialog = UIkit.modal(dialogsOptions.editDialogId, self.getModalOptionsForEditDialog());
        self.handleOnEditDialogClosed();
    }

    getModalOptionsForEditDialog() {
        return dialogsOptions.notClosable;
    }

    clear() {
        var self = this,
            $scope = self.$scope;

        super.clear();
    }

    clearSearchFilters() {
        var self = this,
            $scope = self.$scope;

        super.clearSearchFilters();
    }

    initSearchFilters(): TUpdatableDtoSearchFilter {
        var self = this,
            $scope = self.$scope;

        var theSearchFilter = super.initSearchFilters();

        return theSearchFilter;
    }

    save() {
        var self = this,
            $scope = self.$scope,
            $root = self.$root,
            request = $scope.request,
            tableData = $scope.tableData,
            searchFilters = request.searchFilterModel;

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
    }

    validateOnSave() {
        var self = this,
            $scope = self.$scope,
            $root = self.$root;

        return true;
    }

    cancel($event: angular.IAngularEvent) {
        var self = this,
            $scope = self.$scope;

        $scope.tableData.rows.forEach(function (row) { row.isSelected = false; })
        self.editDialog.hide();

        $event.preventDefault();
        $event.stopPropagation();
    }

    delete($event: angular.IAngularEvent, row: TUpdatableDto) {
        var self = this,
            $scope = self.$scope,
            $root = self.$root,
            tableData = $scope.tableData,
            request = $scope.request,
            searchFilters = $scope.request.searchFilterModel;

        // highlight current row
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
            $scope.tableData.rows.forEach(function (row) { row.isSelected = false; })
        }, dialogsOptions.delete);

        $event && $event.preventDefault();
        $event && $event.stopPropagation();
    }

    add($event: angular.IAngularEvent) {
        var self = this,
            $scope = self.$scope;

        self.clear();
        self.editDialog.show();

        $scope.$broadcast('addDialogShown');
        $scope.$broadcast('addOrEditDialogShown');

        $event.preventDefault();
        $event.stopPropagation();
    }

    prepareDataOnEditDialogShown(data) {
        var self = this,
            $scope = self.$scope,
            $root = self.$root;

        // maybe in his child classes
    }

    showEditDialog($event: angular.IAngularEvent, row: TUpdatableDto) {
        var self = this,
            $scope = self.$scope,
            $root = self.$root,
            tableData = $scope.tableData;

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
    }

    handleOnEditDialogClosed() {
        var self = this,
            $scope = self.$scope,
            $root = self.$root;

        // clear (unSelect) all rows selected state
        $('#edit-dialog').on('hide.uk.modal', function () {
            $scope.tableData.rows.forEach(function (row) { row.isSelected = false; })
        });
    }
}