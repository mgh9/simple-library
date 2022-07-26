altairApp
    .config([
    '$stateProvider',
    '$urlRouterProvider',
    '$locationProvider',
    function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $locationProvider.hashPrefix('');
        $urlRouterProvider
            .when('/dashboard', '/')
            .otherwise('/');
        $stateProvider
            .state("error", {
            url: "/error",
            templateUrl: "/app/views/error.html?ver=".concat(ver),
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'lazy_uikit'
                        ]);
                    }]
            }
        })
            .state("error.404", {
            url: "/404",
            templateUrl: "/app/components/pages/error_404View.html?ver=".concat(ver)
        })
            .state("error.500", {
            url: "/500",
            templateUrl: "/app/components/pages/error_500View.html?ver=".concat(ver)
        })
            .state("restricted", {
            abstract: true,
            url: "",
            templateUrl: "/app/views/restricted.html?ver=".concat(ver),
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'lazy_uikit',
                            'lazy_selectizeJS',
                            'lazy_switchery',
                            'lazy_prismJS',
                            'lazy_autosize',
                            'lazy_iCheck',
                            'lazy_themes',
                            'angucomplete-alt',
                        ]);
                    }]
            }
        })
            .state("login", {
            url: "/login",
            templateUrl: "/pages/login/loginView.html?ver=".concat(ver),
            controller: 'LoginController',
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'lazy_uikit',
                            'lazy_iCheck',
                            "/pages/core/models/enums/loginResult.js?ver=".concat(ver),
                            "/pages/login/loginService.js?ver=".concat(ver),
                            "/pages/login/loginController.js?ver=".concat(ver)
                        ]);
                    }]
            },
            data: {
                pageTitle: 'Login'
            },
            ncyBreadcrumb: {
                label: 'Home'
            }
        })
            .state("restricted.dashboard", {
            url: "/",
            templateUrl: "/pages/dashboard/dashboardView.html?ver=".concat(ver),
            controller: 'DashboardController',
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'lazy_charts_c3',
                            'lazy_charts_chartist',
                            "/pages/core/models/enums/EventCategory.js?ver=".concat(ver),
                            "/pages/core/models/enums/EventType.js?ver=".concat(ver),
                            "/pages/core/models/enums/EventId.js?ver=".concat(ver),
                            "/pages/SEC/userProfile/userProfileService.js?ver=".concat(ver),
                            "/pages/dashboard/dashboardService.js?ver=".concat(ver),
                            "/pages/dashboard/dashboardController.js?ver=".concat(ver),
                        ]);
                    }]
            },
            data: {
                pageTitle: 'Dashboard'
            },
            ncyBreadcrumb: {
                label: 'Home'
            }
        });
    }
]);
//# sourceMappingURL=states.js.map