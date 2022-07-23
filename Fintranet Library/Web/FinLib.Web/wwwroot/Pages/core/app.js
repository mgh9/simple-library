var altairApp = angular.module('altairApp', [
    'ngCookies',
    'ui.router',
    'oc.lazyLoad',
    'ngSanitize',
    'ngRetina',
    'ncy-angular-breadcrumb',
    'angularMoment',
    'ConsoleLogger'
]);
altairApp.constant('variables', {
    header_main_height: 48,
    easing_swiftOut: [0.4, 0, 0.2, 1],
    bez_easing_swiftOut: $.bez([0.4, 0, 0.2, 1])
});
altairApp.config(function ($sceDelegateProvider) {
    $sceDelegateProvider.resourceUrlWhitelist([
        'self',
        'https://www.youtube.com/**',
        'https://w.soundcloud.com/**'
    ]);
});
altairApp.config(function ($breadcrumbProvider) {
    $breadcrumbProvider.setOptions({
        prefixStateName: 'restricted.dashboard',
        templateUrl: 'app/templates/breadcrumbs.tpl.html'
    });
});
function detectIE() { var a = window.navigator.userAgent, b = a.indexOf("MSIE "); if (0 < b)
    return parseInt(a.substring(b + 5, a.indexOf(".", b)), 10); if (0 < a.indexOf("Trident/"))
    return b = a.indexOf("rv:"), parseInt(a.substring(b + 3, a.indexOf(".", b)), 10); b = a.indexOf("Edge/"); return 0 < b ? parseInt(a.substring(b + 5, a.indexOf(".", b)), 10) : !1; }
;
altairApp
    .run([
    '$rootScope',
    '$state',
    '$stateParams',
    '$http',
    '$window',
    '$location',
    '$timeout',
    'preloaders',
    'variables',
    'AccountService',
    function ($rootScope, $state, $stateParams, $http, $window, $location, $timeout, preloaders, variables, accountService) {
        $rootScope.ver = ver;
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
        $rootScope.isActionExecuting = false;
        $rootScope.$on('GetUserInfoRequested', function () {
            accountService.getLoggedInUserInfo().then(function (response) {
                var result = response.data;
                if (result.success) {
                    $rootScope.userInfo = result.data;
                }
                else {
                    window.location.href = '/';
                }
            }, $rootScope.handleError);
        });
        $rootScope.getUserDisplay = function () {
            return ($rootScope.userInfo ? "".concat($rootScope.userInfo.firstName, " ").concat($rootScope.userInfo.lastName) : '');
        };
        $rootScope.getLoggedInUserRoleName = function () {
            var self = this, $scope = self.$scope, $root = self.$root;
            var hisActiveRole = _.find($rootScope.userInfo.userRoles, function (item) { return item.id == $root.userInfo.defaultUserRoleId; });
            return hisActiveRole.roleName;
        };
        $rootScope.getLoggedInUserApplicationRole = function () {
            var self = this, $scope = self.$scope, $root = self.$root;
            return ApplicationRole[$rootScope.getLoggedInUserRoleName()];
        };
        $rootScope.getLoggedInUserRoleId = function () {
            var self = this, $scope = self.$scope, $root = self.$root;
            return $rootScope.userInfo.defaultUserRoleId;
        };
        $rootScope.getLoggedInUserId = function () {
            var self = this, $scope = self.$scope, $root = self.$root;
            return $rootScope.userInfo.id;
        };
        $rootScope.getActiveRoleTitle = function () {
            if ($rootScope.userInfo) {
                var role = $rootScope.userInfo.userRoles.filter(function (item) {
                    return item.id == $rootScope.userInfo.defaultUserRoleId;
                })[0];
                return role.roleTitle;
            }
            else
                return '';
        };
        $rootScope.$broadcast('GetUserInfoRequested');
        $rootScope.admOptions = {
            calType: 'gregorian',
            dtpType: 'date',
            format: 'YYYY/MM/DD',
            autoClose: true,
            multiple: false
        };
        $rootScope.admTimeOptions = {
            calType: 'gregorian',
            dtpType: 'date&time',
            format: 'YYYY/MM/DD hh:mm',
            autoClose: true,
            multiple: false
        };
        $rootScope.selectizeConfig = {
            hideSelected: false,
            create: false,
            maxItems: 1,
            placeholder: 'Select...',
            valueField: 'value',
            labelField: 'title',
            searchField: 'title'
        };
        $rootScope.autocompleteConfig = {
            param: 'text',
            source: '/api/Users/Autocomplete',
            minLength: 2
        };
        $rootScope.checkAuthentication = function (isAuthenticated) {
            $rootScope.isAuthenticated = isAuthenticated;
            if (!isAuthenticated) {
                $timeout(function () {
                    window.location.href = '/';
                });
            }
        };
        $rootScope.handleError = function (arguments, preventNotify) {
            if (preventNotify)
                return;
            var self = this;
            if (arguments.status == 400 && arguments.data) {
                handleError400($rootScope, arguments.data);
                return;
            }
            if (arguments.status == 404) {
                handleError404($rootScope, arguments.data);
                return;
            }
            if (arguments.status == 405 && arguments.data) {
                handleError405($rootScope, arguments.data);
                return;
            }
            if (arguments.status == 429 && arguments.data) {
                handleError429($rootScope, arguments.data);
                return;
            }
            if (arguments.status == 500 && arguments.data) {
                handleError500($rootScope, arguments.data);
                return;
            }
            var isSuccessHttp = !(arguments.status);
            if (isSuccessHttp) {
                if (arguments && arguments[0].data) {
                    var data = arguments[0].data, error = data.error, errorClassName = error.ClassName, message = 'Something wrong happened in your request';
                    if (data.message) {
                        message = data.message;
                    }
                    else if (error && errorClassName.indexOf('LogoutException') >= 0) {
                        $rootScope.notify('You were logged out, please login again...', NotifyTypes.Warning);
                        return;
                    }
                    else if (error && error.InnerException && error.InnerException.Message) {
                        message = error.InnerException.Message;
                    }
                    else if (error && error.Message) {
                        message = error.Message;
                    }
                    $rootScope.notify(message, NotifyTypes.Error);
                }
            }
            else if (arguments && arguments.data && JSON.stringify(arguments.data) != '{}' && arguments.data.userFriendlyMessage) {
                var message = arguments.data.userFriendlyMessage + '\n' + 'Error code:' + arguments.data.errorId;
                $rootScope.notify(message, NotifyTypes.Error);
            }
            else {
                var message = 'Error in processing your request';
                $rootScope.notify(message, NotifyTypes.Error);
            }
        };
        $rootScope.handleHttpError = function (arguments, preventNotify) {
        };
        $rootScope.onKeyDown = function ($event) {
            console.log($event.keyCode == 65);
            if ($event.ctrlKey && $event.shiftKey && $event.keyCode == 65) {
                setTimeout(function () {
                    $('.btn-add').click();
                });
            }
        };
        $rootScope.notifySuccessfullAction = function () {
            $rootScope.notify('Successfully done', NotifyTypes.Success);
        };
        $rootScope.notify = function (message, notifyType) {
            message = message || 'Something happened in processing your request';
            if (notifyType == undefined)
                notifyType = NotifyTypes.Success;
            var status = '';
            switch (notifyType) {
                case NotifyTypes.Success:
                    status = 'success';
                    break;
                case NotifyTypes.Warning:
                    status = 'warning';
                    break;
                case NotifyTypes.Error:
                    status = 'error';
                    break;
                default:
                    break;
            }
            message = message.replace(/\n/g, "<br />");
            UIkit.notify({
                message: message,
                status: status,
                pos: 'top-center',
                timeout: 5000,
            });
        };
        $rootScope.$on('$stateChangeSuccess', function ($event, toStep, toParams, fromState, fromParams) {
            if ($('.uk-modal.uk-open').length) {
                UIkit.modal('.uk-modal.uk-open').hide(true);
            }
            $rootScope.fromParams = fromParams;
            $rootScope.fromState = fromState;
            $("html, body").animate({
                scrollTop: 0
            }, 200);
            if (detectIE()) {
                $('svg,canvas,video').each(function () {
                    $(this).css('height', 0);
                });
            }
            ;
            $timeout(function () {
                $rootScope.pageLoading = false;
            }, 300);
            $timeout(function () {
                $rootScope.pageLoaded = true;
                $rootScope.appInitialized = true;
                $window.Waves.attach('.md-btn-wave,.md-fab-wave', ['waves-button']);
                $window.Waves.attach('.md-btn-wave-light,.md-fab-wave-light', ['waves-button', 'waves-light']);
                if (detectIE()) {
                    $('svg,canvas,video').each(function () {
                        var $this = $(this), height = $(this).attr('height'), width = $(this).attr('width');
                        if (height) {
                            $this.css('height', height);
                        }
                        if (width) {
                            $this.css('width', width);
                        }
                        var peity = $this.prev('.peity_data,.peity');
                        if (peity.length) {
                            peity.peity().change();
                        }
                    });
                }
            }, 600);
        });
        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
            if (toState.name != 'restricted.management.userProfile' &&
                toState.name != 'login' &&
                $rootScope.menus &&
                $rootScope.menus.filter(function (item) { return item.route == toState.name; }).length == 0) {
            }
            $rootScope.mainSearchActive = false;
            $rootScope.sidebar_secondary = false;
            $rootScope.secondarySidebarHiddenLarge = false;
            if ($($window).width() < 1220) {
                $rootScope.primarySidebarActive = false;
                $rootScope.hide_content_sidebar = false;
            }
            if (!toParams.hasOwnProperty('hidePreloader')) {
                $rootScope.pageLoading = true;
                $rootScope.pageLoaded = false;
            }
        });
        $rootScope.$on('$locationChangeStart', function (event, current, previous) {
            $rootScope.previousRoute = previous;
        });
        $rootScope.backToPreviousRoute = function () {
            if ($rootScope.previousRoute) {
                $location.path($rootScope.previousRoute);
            }
        };
        FastClick.attach(document.body);
        $rootScope.Modernizr = Modernizr;
        var w = angular.element($window);
        $rootScope.largeScreen = w.width() >= 1220;
        w.on('resize', function () {
            return $rootScope.largeScreen = w.width() >= 1220;
        });
        $rootScope.primarySidebarOpen = $rootScope.largeScreen;
        $rootScope.pageLoading = true;
        $window.Waves.init();
    }
])
    .run(['PrintToConsole', function (PrintToConsole) {
        PrintToConsole.active = false;
    }]);
function handleError400(rootScope, errorResult) {
    var errorMessage = errorResult.message;
    rootScope.notify(errorMessage, NotifyTypes.Error);
}
function handleError404(rootScope, errorResult) {
    rootScope.notify('an error happened when processing your request. code 404', NotifyTypes.Error);
}
function handleError405(rootScope, errorResult) {
    var errorMessage = errorResult.message || "You don't have permission for this operation. code 405";
    rootScope.notify(errorMessage, NotifyTypes.Error);
}
function handleError429(rootScope, errorResult) {
    var errorMessage = errorResult.message;
    rootScope.notify(errorMessage, NotifyTypes.Error);
}
function handleError500(rootScope, errorResult) {
    var errorMessage = errorResult.message;
    rootScope.notify(errorMessage, NotifyTypes.Error);
}
function makePaging(totalPages, pageIndex) {
    var paging = [], i, j, k;
    i = pageIndex;
    j = pageIndex - 1;
    k = pageIndex + 1;
    while (j != 0 && j != i - 6) {
        paging.push(j);
        j--;
    }
    paging.reverse();
    paging.push(i);
    for (; k < totalPages + 1 && k < i + 6; k++) {
        paging.push(k);
    }
    return paging;
}
//# sourceMappingURL=app.js.map