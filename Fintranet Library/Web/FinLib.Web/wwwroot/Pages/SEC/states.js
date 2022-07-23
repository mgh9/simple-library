altairApp
    .config([
    '$stateProvider',
    '$urlRouterProvider',
    '$locationProvider',
    function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $stateProvider
            .state("restricted.management.users", {
            url: "/users",
            templateUrl: "/pages/SEC/users/userView.html?ver=".concat(ver),
            controller: 'UserController',
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'ADM',
                            "/pages/core/models/enums/Gender.js?ver=".concat(ver),
                            "/pages/SEC/roles/roleService.js?ver=".concat(ver),
                            "/pages/SEC/users/userService.js?ver=".concat(ver),
                            "/pages/SEC/users/userController.js?ver=".concat(ver)
                        ]);
                    }]
            },
            data: {
                pageTitle: 'Users Management'
            },
            ncyBreadcrumb: {
                label: 'Home'
            }
        })
            .state("restricted.management.userProfile", {
            url: "/user-profile",
            templateUrl: "/pages/SEC/userProfile/userProfileView.html?ver=".concat(ver),
            controller: 'UserProfileController',
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'ADM',
                            "/pages/core/models/enums/Gender.js?ver=".concat(ver),
                            "/pages/core/models/enums/EventCategory.js?ver=".concat(ver),
                            "/pages/core/models/enums/EventType.js?ver=".concat(ver),
                            "/pages/core/models/enums/EventId.js?ver=".concat(ver),
                            "/pages/SEC/userProfile/userProfileService.js?ver=".concat(ver),
                            "/pages/SEC/userProfile/userProfileController.js?ver=".concat(ver)
                        ]);
                    }]
            },
            data: {
                pageTitle: 'User Profile'
            },
            ncyBreadcrumb: {
                label: 'Home'
            }
        });
    }
]);
//# sourceMappingURL=states.js.map