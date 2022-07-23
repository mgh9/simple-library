class BaseEntityController
    <TDto extends IBaseEntityDto
    , TView extends IBaseView
    , TSearchFilter extends IBaseEntitySearchFilter>
    extends BaseController {
    public singleViewDialog: UIkit.ModalElement;

    constructor(public $root: IRootScope,
        public $scope: ITableBaseScope<BaseEntityController<TDto, TView, TSearchFilter>, TDto, TView, TSearchFilter>,
        public service: BaseEntityService<TDto, TView, TSearchFilter>,
        public defaultSort?: string,
        public loadDataAfterInit: boolean = true) {

        super($root, $scope);

        var self = $scope.self = this;
        $scope.isFilterActive = false;

        var preparedDefaultSort = self.getDefaultSortPrepared(defaultSort);

        $scope.request =
        {
            searchFilterModel: undefined,
            pageIndex: 0,
            pageSize: 10,
            pageOrder: preparedDefaultSort
        };

        $scope.request.searchFilterModel = undefined;// self.initSearchFilters();

        $scope.tableData = {
            columns: [],
            rows: [],
            count: 0,
        };

        self.singleViewDialog = UIkit.modal(dialogsOptions.singleViewDialogId, dialogsOptions.notClosable);
        self.handleSingleViewDialogClosed();

        self.clear();

        if (loadDataAfterInit) {
            //self.initSearchFilters();
            self.loadData($scope.request, true);
        }
    }

    init() {
        var self = this,
            $scope = self.$scope;

        $scope.request.searchFilterModel = self.initSearchFilters();
        //self.initSearchFilters();
    }

    initSearchFilters(): TSearchFilter {
        var self = this,
            $scope = self.$scope;

        $scope.request.searchFilterModel = <TSearchFilter>{};

        return $scope.request.searchFilterModel;
    }

    clear() {
        var self = this,
            $scope = self.$scope;

        $scope.entity = <TDto>({
            
        });
    }

    clearSearchFilters() {
        var self = this,
            $scope = self.$scope;

        //self.filterTable();
    }

    toggleFilter() {
        var self = this,
            $scope = self.$scope;

        $scope.isFilterActive = !$scope.isFilterActive;
        self.clearSearchFilters();

        if (!$scope.isFilterActive)
            self.filterTable();
    }

    filterTable() {
        var self = this,
            $scope = self.$scope;

        self.refreshGrid();
    }

    refreshGrid() {
        var self = this,
            $root = self.$root,
            $scope = self.$scope;

        self.loadData($scope.request);
    }

    getDefaultSortPrepared(defaultSort: string) {
        var self = this,
            $scope = self.$scope,
            $root = self.$root;

        if (!defaultSort)
            return '';

        var preparedDefaultSort = defaultSort.trim();

        var defaultSortNormalized = preparedDefaultSort.toUpperCase();
        if (defaultSortNormalized.indexOf(' ASC') < 0 && defaultSortNormalized.indexOf(' DESC') < 0) {
            preparedDefaultSort = defaultSort + ' asc';
        }

        // simple camelCasing
        preparedDefaultSort = preparedDefaultSort.charAt(0).toLocaleLowerCase() + preparedDefaultSort.substring(1, preparedDefaultSort.length);

        return preparedDefaultSort;
    }

    loadData(request: IGetRequestDto<TSearchFilter>, isInit?: boolean, forceLoadData?: boolean) {
        var self = this,
            $scope = self.$scope,
            $root = self.$root,
            tableData = $scope.tableData;

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
    }

    prepareDataOnSingleViewDialogShown(data: TView) {
        // prepare for viewing in child classes
    }

    showSingleViewDialog($event: angular.IAngularEvent, row: TDto) {
        var self = this,
            $scope = self.$scope,
            $root = self.$root,
            tableData = $scope.tableData;

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
    }

    closeSingleViewDialog($event: angular.IAngularEvent) {
        var self = this,
            $scope = self.$scope;

        $scope.tableData.rows.forEach(function (row) { row.isSelected = false; })
        self.singleViewDialog.hide();

        $event.preventDefault();
        $event.stopPropagation();
    }

    handleSingleViewDialogClosed() {
        var self = this,
            $scope = self.$scope,
            $root = self.$root;

        // clear (unSelect) all rows's selected state
        $('#single-view-dialog').on('hide.uk.modal', function () {
            $scope.tableData.rows.forEach(function (row) { row.isSelected = false; })
        });
    }
}