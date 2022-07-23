interface IGridViewScope<TSearchFilter extends IBaseEntitySearchFilter>
    extends IBaseScope<ITableController<TSearchFilter>> {
    request: IGetRequestDto<TSearchFilter>;
    tableData: ITableData<IBaseEntityDto>;
    //searchFilters: TSearchFilter;
    gridViewConfig: IGridViewConfig,

    selectedPageSize: number;
    pageSizes: any[];
    pageCount: number;
    pageSizeConfig: any;
    pages: number[];

    firstPageDisable: boolean;
    lastPageDisable: boolean;
    goto($event: angular.IAngularEvent, index: number);
    sortBy($event: angular.IAngularEvent, columnName: string);
    //sortBy($event: angular.IAngularEvent, column: IColumn);

    selectAll: boolean;
    selectAllChanged: () => void;
}

interface IGridViewConfig {
    elementId : string,
    showCheckbox: boolean,
    editLinkVisible: boolean,
    deleteLinkVisible: boolean,
    singleViewLinkVisible: boolean,
    lastUpdateTimeVisible: boolean,
    customActions?: ICustomAction[],
}

interface ICustomAction {
    click: (event, row) => void;
    icon: string;
    title?: string;
    tooltip?: string;
    color?: string;
}

altairApp.directive('gridView', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/pages/templates/gridView.html',
        controller: function ($rootScope: IRootScope, $scope: IGridViewScope<IBaseEntitySearchFilter>) {
            var self = $scope.self;

            $scope.selectAll = false;
            if (!$scope.gridViewConfig)
                $scope.gridViewConfig = {
                    elementId : 'main_grid',
                    showCheckbox: false,
                    deleteLinkVisible: true,
                    editLinkVisible: true,
                    singleViewLinkVisible: false,
                    lastUpdateTimeVisible: true,
                };

            $scope.pageSizes = [{ title: '10', value: 10 }, { title: '20', value: 20 }, { title: '50', value: 50 }, { title: '100', value: 100 }, { title: '200', value: 200 }, { title: '500', value: 500 }];
            $scope.pageSizeConfig = {
                maxItems: 1,
                labelField: 'title',
                valueField: 'value',
                onChange: function (value) {
                    // page sizes changed ~> go to first page.
                    $scope.request.pageIndex = 0;
                    $scope.request.pageSize = parseInt(value);

                    self.loadData($scope.request);
                }
            };
            $scope.selectedPageSize = 10;

            $scope.$on('tableDataLoaded', function () {
                $scope.pageCount = Math.ceil($scope.tableData.count / (parseFloat($scope.request.pageSize.toString()) || $scope.tableData.count));

                $scope.pages = [];
                $scope.pages = makePaging($scope.pageCount, $scope.request.pageIndex + 1);
                $scope.firstPageDisable = ($scope.request.pageIndex == 0);
                $scope.lastPageDisable = $scope.pageCount == 0 || ($scope.request.pageIndex == ($scope.pageCount - 1));
            });

            $scope.goto = function ($event: angular.IAngularEvent, index: number) {
                $scope.request.pageIndex = index;

                //var theRequest: IGetRequestJson<IBaseEntitySearchFilter> =
                //{
                //    pageIndex: index,
                //    pageSize: $scope.tableData.request.pageSize,
                //    pageOrder: $scope.tableData.request.pageOrder,
                //    searchFilterModel: $scope.tableData.request.searchFilterModel
                //};
                self.loadData($scope.request);

                $event.preventDefault();
                $event.stopPropagation();
            };

            $scope.sortBy = function ($event: any, columnName: string) {//column: IColumn) {
                //var field = column.field;
                var desc = ($scope.request.pageOrder.indexOf('desc') > 0);

                //debugger;

                if ($scope.request.pageOrder == 'default')
                    desc = true;

                //var theRequest: IGetRequestJson<IBaseEntitySearchFilter> =
                //{
                //    pageIndex: 0,
                //    pageSize: $scope.tableData.request.pageSize,
                //    pageOrder: `${field} ${desc ? 'asc' : 'desc'}`,
                //    searchFilterModel: $scope.tableData.request.searchFilterModel
                //};

                $scope.request.pageIndex = 0;
                $scope.request.pageOrder = `${columnName} ${desc ? 'asc' : 'desc'}`;

                self.loadData($scope.request);

                //self.loadData(0,
                //    $scope.tableData.pageSize,
                //    `${field} ${desc ? 'asc' : 'desc'}`,
                //    $scope.searchFilters);
            };

            $scope.selectAllChanged = function () {
                for (var item of $scope.tableData.rows) {
                    item.isSelected = $scope.selectAll;
                }
            };
        }
    };
});

altairApp.directive('pageHeader', function () {
    return {
        restrict: 'E',
        replace: true,
        transclude: true,
        template: '<div id="page_heading"><h1><ng-transclude></ng-transclude></h1></div>',
        link: function ($scope: any, element, attributes) {
        },
        controller: function ($scope: any) {
        }
    };
});

altairApp.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });
                event.preventDefault();
            }
        });
    };
});

altairApp.directive('generalEntityHeader', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/pages/templates/searchFiltersFormHeaders/generalEntityHeader.html',
        link: function ($scope: any, element, attributes) {
        },
        controller: function ($scope: any) {
        }
    };
});

altairApp.directive('codeValueEntityHeader', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/pages/templates/searchFiltersFormHeaders/codeValueEntityHeader.html',
        link: function ($scope: any, element, attributes) {
        },
        controller: function ($scope: any) {
        }
    };
});

altairApp.directive('fileUploader', function ($rootScope) {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            oncomplete: '&'
        },
        templateUrl: '/pages/templates/fileUploader.html',
        link: function ($scope: any, element, attributes) {
            $scope.id = Math.round(Math.random() * 9999);
            $scope.type = attributes.type;

            var progressbar = $("#Progressbar"),
                bar = progressbar.find('.uk-progress-bar'),
                settings = {
                    action: `/FileUploader/Upload?categoryName=${attributes.folder}`,
                    allow: '*.(jpg|jpeg|png)', /* TODO: Config */
                    loadstart: function () {
                        bar.css("width", "0%").text("0%");
                        progressbar.removeClass("uk-hidden");
                    },
                    progress: function (percent) {
                        percent = Math.ceil(percent);
                        bar.css("width", percent + "%").text(percent + "%");
                    },
                    allcomplete: function (response, item: any) {
                        $(`#FileUploader-${$scope.id}`).val(undefined);

                        bar.css("width", "100%").text("100%");

                        setTimeout(function () {
                            progressbar.addClass("uk-hidden");
                        }, 250);

                        $scope.oncomplete({ result: JSON.parse(response) });

                        $scope.$apply();
                    }
                };

            setTimeout(function () {
                var select = UIkit.uploadSelect($(`#FileUploader-${$scope.id}`), settings),
                    drop = UIkit.uploadDrop($(`#FileUploaderDrop-${$scope.id}`), settings);
            });
        },
        controller: function ($scope: any) {
        }
    };
});

altairApp.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl: ng.INgModelController) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

altairApp.directive('compile', ['$compile', function ($compile) {
    return function (scope, element, attrs) {
        scope.$watch(
            function (scope) {
                // watch the 'compile' expression for changes
                return scope.$eval(attrs.compile);
            },
            function (value) {
                // when the 'compile' expression changes
                // assign it into the current DOM
                element.html(value);

                // compile the new DOM and link it to the current
                // scope.
                // NOTE: we only compile .childNodes so that
                // we don't get into infinite loop compiling ourselves
                $compile(element.contents())(scope);
            }
        );
    };
}]);

