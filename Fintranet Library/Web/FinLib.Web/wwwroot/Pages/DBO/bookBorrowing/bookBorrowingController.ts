namespace FinLib.Pages.DBO {
    export class BookBorrowingController extends UpdatableEntityController<IBookBorrowingDto, IBookBorrowingView, IBookBorrowingSearchFilter> {
        public static $inject = ['$rootScope', '$scope', '$http', 'BookService', 'UserService', 'BookBorrowingService'];

        constructor(public $root: IRootScope,
            public $scope: IBookBorrowingScope,
            public $http: angular.IHttpService,
            public bookService: DBO.BookService,
            public userService: SEC.UserService,
            public bookBorrowingService: BookBorrowingService) {

            super($root, $scope, bookBorrowingService, 'id desc');

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

            $scope.customers = [];
            $scope.customersConfig = angular.extend({}, $root.selectizeConfig, {
                placeholder: 'Customer...'
            });

            $scope.isFilterActive = true;

            $scope.$on('addOrEditDialogShown', function () {

                self.bookService.getTitleValueList()
                    .then(function (response) {
                        var result = response.data;

                        if (result.success) {
                            $scope.books = result.data;
                        }
                        else
                            self.$root.handleError(arguments);

                    }, self.$root.handleError);

                self.userService.getCustomersUserRoleIdsTitleValueList()
                    .then(function (response) {
                        var result = response.data;

                        if (result.success) {
                            $scope.customers = result.data;
                        }
                        else
                            self.$root.handleError(arguments);

                    }, self.$root.handleError);

            });
        }

        initCustomColumns() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            $scope.gridViewConfig = {
                elementId: 'books_borrowing_grid',
                showCheckbox: false,
                deleteLinkVisible: true,
                editLinkVisible: true,
                singleViewLinkVisible: true,
                lastUpdateTimeVisible: true,
                customActions: [
                    {
                        click: (event, row) => {
                            self.returnBook(event, row)
                        },

                        icon: "assignment_turned_in",
                        title: "Return Book",
                        color: "md-color-green-500",
                        tooltip: "Mark the selected Borrowing row as returned"
                    },
                ]
            };
        }

        initSearchFilters(): IBookBorrowingSearchFilter {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            var theSearchFilter = super.initSearchFilters();

            $scope.booksForSearchFilter = [];
            $scope.booksForSearchFilterConfig = angular.extend({}, $root.selectizeConfig, {
                placeholder: 'Book...'
            });

            $scope.customersForSearchFilter = [];
            $scope.customersForSearchFilterConfig = angular.extend({}, $root.selectizeConfig, {
                placeholder: 'Customer...'
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

            self.userService.getCustomersUserRoleIdsTitleValueList()
                .then(function (response) {
                    var result = response.data;

                    if (result.success) {
                        self.$scope.customersForSearchFilter = result.data;
                        self.$scope.customersForSearchFilter.unshift({
                            value: -1,
                            title: '«All Customers»'
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

            theSearchFilter.customerUserRoleId = {
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

        returnBook($event: ng.IAngularEvent, row: IBookBorrowingDto) {
            var self = this,
                $scope = self.$scope,
                $root = self.$root,
                tableData = $scope.tableData,
                request = $scope.request;

            UIkit.modal.confirm("Are u sure to return the seleceted book?", function () {

                var model: IReturnBookDto = { bookBorrowingId  : row.id };

                self.bookBorrowingService.returnBook(model)
                    .then(function (response) {
                        var result = response.data;

                        if (result.success) {
                            self.loadData(request);
                            $root.notify(`The selected book returned successfully`, NotifyTypes.Success);
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
                        $root.hidePreloader();
                    });
            }, dialogsOptions.confirm);
        }

        clearSearchFilters() {
            var self = this,
                $scope = self.$scope;

            super.clearSearchFilters();

            $scope.request.searchFilterModel.bookId.value = undefined;
            $scope.request.searchFilterModel.customerUserRoleId.value = undefined;
        }

        validateOnSave() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            if (!$scope.entity.bookId) {
                $root.notify('Please select a book', NotifyTypes.Warning);
                return false;
            }

            if (!$scope.entity.customerUserRoleId) {
                $root.notify('Please select a customer', NotifyTypes.Warning);
                return false;
            }

            return super.validateOnSave();
        }

    }

    angular
        .module('altairApp')
        .controller('BookBorrowingController', BookBorrowingController);
}