namespace FinLib.Pages.DBO {
    export class MyBorrowingHistoryController
        extends BaseEntityController<IBookBorrowingDto,
        IMyBorrowingHistoryView, IMyBorrowingHistorySearchFilter> {
        public static $inject = ['$rootScope', '$scope', '$http', 'BookService', 'UserService', 'MyBorrowingHistoryService'];

        constructor(public $root: IRootScope,
            public $scope: IMyBorrowingHistoryScope,
            public $http: angular.IHttpService,
            public bookService: DBO.BookService,
            public userService: SEC.UserService,
            public myBorrowingHistoryService: MyBorrowingHistoryService) {

            super($root, $scope, myBorrowingHistoryService, 'id desc');

            this.init();
        }

        init() {
            super.init();
            
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            self.initCustomColumns();
            self.initSearchFilters();

            $scope.books = [];
            $scope.booksConfig = angular.extend({}, $root.selectizeConfig, {
                placeholder: 'Book...'
            });

            $scope.isFilterActive = true;
        }

        initCustomColumns() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            $scope.gridViewConfig = {
                elementId: 'my_borrowing_history_grid',
                showCheckbox: false,
                deleteLinkVisible: false,
                editLinkVisible: false,
                singleViewLinkVisible: false,
                lastUpdateTimeVisible: false,
            };
        }

        initSearchFilters(): IMyBorrowingHistorySearchFilter {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            var theSearchFilter = super.initSearchFilters();

            $scope.booksForSearchFilter = [];
            $scope.booksForSearchFilterConfig = angular.extend({}, $root.selectizeConfig, {
                placeholder: 'Book...'
            });

            self.bookService.getTitleValueList()
                .then(function (response) {
                    var result = response.data;

                    if (result.success) {
                        self.$scope.booksForSearchFilter = result.data;
                        self.$scope.booksForSearchFilter.unshift({
                            value: -1,
                            title: '«All Books»'
                        });
                    }
                    else
                        self.$root.handleError(arguments);
                }, self.$root.handleError);

            theSearchFilter.bookId = {
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

            $scope.request.searchFilterModel.bookId.value = undefined;
        }

    }

    angular
        .module('altairApp')
        .controller('MyBorrowingHistoryController', MyBorrowingHistoryController);
}