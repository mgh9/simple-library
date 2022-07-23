altairApp
    .config([
        '$stateProvider',
        '$urlRouterProvider',
        '$locationProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider) {

            $stateProvider

                // the Content parent state
                .state("restricted.management", {
                    abstract: false,
                    url: '/management',
                    template: '<ui-view/>',
                })

                .state("restricted.management.menu-links", {
                    url: "/menu-links",
                    templateUrl: `/pages/CNT/menuLinks/menuLinkView.html?ver=${ver}`,
                    controller: 'MenuLinkController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                'ADM',
                                `/pages/SEC/roles/roleService.js?ver=${ver}`,
                                `/pages/CNT/menuLinks/menuLinkService.js?ver=${ver}`,
                                `/pages/CNT/menuLinks/menuLinkController.js?ver=${ver}`
                            ]);
                        }]
                    },
                    data: {
                        pageTitle: 'Menus'
                    },
                    ncyBreadcrumb: {
                        label: 'Home'
                    }
                });

        }
    ]);
