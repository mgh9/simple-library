altairApp.directive('gridView', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/pages/templates/gridView.html',
        controller: function ($rootScope, $scope) {
            var self = $scope.self;
            $scope.selectAll = false;
            if (!$scope.gridViewConfig)
                $scope.gridViewConfig = {
                    elementId: 'main_grid',
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
            $scope.goto = function ($event, index) {
                $scope.request.pageIndex = index;
                self.loadData($scope.request);
                $event.preventDefault();
                $event.stopPropagation();
            };
            $scope.sortBy = function ($event, columnName) {
                var desc = ($scope.request.pageOrder.indexOf('desc') > 0);
                if ($scope.request.pageOrder == 'default')
                    desc = true;
                $scope.request.pageIndex = 0;
                $scope.request.pageOrder = "".concat(columnName, " ").concat(desc ? 'asc' : 'desc');
                self.loadData($scope.request);
            };
            $scope.selectAllChanged = function () {
                for (var _i = 0, _a = $scope.tableData.rows; _i < _a.length; _i++) {
                    var item = _a[_i];
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
        link: function ($scope, element, attributes) {
        },
        controller: function ($scope) {
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
        link: function ($scope, element, attributes) {
        },
        controller: function ($scope) {
        }
    };
});
altairApp.directive('codeValueEntityHeader', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/pages/templates/searchFiltersFormHeaders/codeValueEntityHeader.html',
        link: function ($scope, element, attributes) {
        },
        controller: function ($scope) {
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
        link: function ($scope, element, attributes) {
            $scope.id = Math.round(Math.random() * 9999);
            $scope.type = attributes.type;
            var progressbar = $("#Progressbar"), bar = progressbar.find('.uk-progress-bar'), settings = {
                action: "/FileUploader/Upload?categoryName=".concat(attributes.folder),
                allow: '*.(jpg|jpeg|png)',
                loadstart: function () {
                    bar.css("width", "0%").text("0%");
                    progressbar.removeClass("uk-hidden");
                },
                progress: function (percent) {
                    percent = Math.ceil(percent);
                    bar.css("width", percent + "%").text(percent + "%");
                },
                allcomplete: function (response, item) {
                    $("#FileUploader-".concat($scope.id)).val(undefined);
                    bar.css("width", "100%").text("100%");
                    setTimeout(function () {
                        progressbar.addClass("uk-hidden");
                    }, 250);
                    $scope.oncomplete({ result: JSON.parse(response) });
                    $scope.$apply();
                }
            };
            setTimeout(function () {
                var select = UIkit.uploadSelect($("#FileUploader-".concat($scope.id)), settings), drop = UIkit.uploadDrop($("#FileUploaderDrop-".concat($scope.id)), settings);
            });
        },
        controller: function ($scope) {
        }
    };
});
altairApp.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
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
            scope.$watch(function (scope) {
                return scope.$eval(attrs.compile);
            }, function (value) {
                element.html(value);
                $compile(element.contents())(scope);
            });
        };
    }]);
//# sourceMappingURL=directives.js.map