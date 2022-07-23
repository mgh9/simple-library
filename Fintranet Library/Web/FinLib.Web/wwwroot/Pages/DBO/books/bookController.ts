namespace FinLib.Pages.DBO {
    export class BookController extends GeneralEntityController<IBookDto, IBookView, IBookSearchFilter> {
        public static $inject = ['$rootScope', '$scope', '$http', 'CategoryService', 'BookService'];

        constructor(public $root: IRootScope,
            public $scope: IBookScope,
            public $http: angular.IHttpService,
            public categoryService: DBO.CategoryService,
            public bookService: BookService) {

            super($root, $scope, bookService, "id");

            this.init();
        }

        init() {
            super.init();

            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            self.$scope.categories = [];
            $scope.categoriesConfig = angular.extend({}, $root.selectizeConfig, {
                placeholder: 'Category...'
            });

            $scope.$on('addOrEditDialogShown', function () {
                self.categoryService.getTitleValueList()
                    .then(function (response) {
                        var result = response.data;

                        if (result.success) {
                            $scope.categories = result.data;
                        }
                        else
                            self.$root.handleError(arguments);

                    }, self.$root.handleError);
                
            });
        }

        initSearchFilters(): IBookSearchFilter {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            var theSearchFilter = super.initSearchFilters();

            self.$scope.categoriesForSearchFilter = [];
            $scope.categoriesForSearchFilterConfig = angular.extend({}, $root.selectizeConfig, {
                placeholder: 'Category...'
            });

            self.categoryService.getTitleValueList()
                .then(function (response) {
                    var result = response.data;

                    if (result.success) {
                        self.$scope.categoriesForSearchFilter = result.data;
                        self.$scope.categoriesForSearchFilter.unshift({
                            value: -1,
                            title: '«All Categories»'
                        });
                    }
                    else
                        self.$root.handleError(arguments);
                }, self.$root.handleError);

            theSearchFilter.categoryId = {
                value: undefined,
                value2: undefined,
                isIgnore: undefined,
                conditionType: ConditionType.Equals,
                columnName: undefined,
                valueType: ValueType.Number
            };

            return theSearchFilter;
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

            $scope.request.searchFilterModel.categoryId.value = undefined;
        }

        validateOnSave() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            if (!$scope.entity.categoryId) {
                $root.notify('Please select the category', NotifyTypes.Warning);
                return false;
            }

            return super.validateOnSave();
        }

    }

    angular
        .module('altairApp')
        .controller('BookController', BookController);
}