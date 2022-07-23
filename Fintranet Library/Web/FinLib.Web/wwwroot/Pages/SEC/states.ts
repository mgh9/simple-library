altairApp
    .config([
        '$stateProvider',
        '$urlRouterProvider',
        '$locationProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider) {

            $stateProvider

                //// the Security parent state
                //.state("restricted.management", {
                //    abstract: true,
                //    url: '/management',
                //    template: '<ui-view/>',
                //})

                .state("restricted.management.users", {
                    url: "/users",
                    templateUrl: `/pages/SEC/users/userView.html?ver=${ver}`,
                    controller: 'UserController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                'ADM',
                                `/pages/core/models/enums/Gender.js?ver=${ver}`,
                                `/pages/SEC/roles/roleService.js?ver=${ver}`,
                                `/pages/SEC/users/userService.js?ver=${ver}`,
                                `/pages/SEC/users/userController.js?ver=${ver}`
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
                    templateUrl: `/pages/SEC/userProfile/userProfileView.html?ver=${ver}`,
                    controller: 'UserProfileController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                'ADM',
                                `/pages/core/models/enums/Gender.js?ver=${ver}`,
                                `/pages/core/models/enums/EventCategory.js?ver=${ver}`,
                                `/pages/core/models/enums/EventType.js?ver=${ver}`,
                                `/pages/core/models/enums/EventId.js?ver=${ver}`,

                                `/pages/SEC/userProfile/userProfileService.js?ver=${ver}`,
                                `/pages/SEC/userProfile/userProfileController.js?ver=${ver}`
                            ]);
                        }]
                    },
                    data: {
                        pageTitle: 'User Profile'
                    },
                    ncyBreadcrumb: {
                        label: 'Home'
                    }
                })

                ;
        }
    ]);
