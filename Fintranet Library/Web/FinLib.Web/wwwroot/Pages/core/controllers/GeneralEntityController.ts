class GeneralEntityController<TGeneralDto extends IGeneralDto
    , TGeneralView extends IGeneralView
    , TSearchFilter extends IGeneralEntitySearchFilter>
    extends UpdatableEntityController<TGeneralDto, TGeneralView, TSearchFilter> {
    constructor(public $root: IRootScope,
        public $scope: ITableBaseScope<GeneralEntityController<TGeneralDto, TGeneralView, TSearchFilter>, TGeneralDto, TGeneralView, TSearchFilter>,
        public service: any,
        public defaultSort?: string,
        public loadDataAfterInit: boolean = true) {

        super($root, $scope, service, (defaultSort || 'title asc'), loadDataAfterInit);
    }

    clear() {
        var self = this,
            $scope = self.$scope;

        super.clear();

        $scope.entity.title = '';
        $scope.entity.description = '';
        $scope.entity.isActive = true;
    }

    clearSearchFilters() {
        var self = this,
            $scope = self.$scope;

        $scope.request.searchFilterModel.title.value = undefined;
        $scope.request.searchFilterModel.isActive.value = undefined;

        super.clearSearchFilters();
    }

    initSearchFilters(): TSearchFilter {
        var self = this,
            $scope = self.$scope;

        var theSearchFilter = super.initSearchFilters();

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
    }

    validateOnSave() {
        var self = this,
            $scope = self.$scope,
            $root = self.$root;

        if (!$scope.entity.title) {
            $root.notify('Please enter Title', NotifyTypes.Warning);
            return false;
        }

        return super.validateOnSave();
    }
}