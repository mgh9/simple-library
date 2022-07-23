namespace FinLib.Pages.Shared {
    export class MainSidebarController extends BaseController {
        public static $inject = ['$rootScope', '$scope', '$http', '$timeout', 'MainSidebarService'];

        constructor(public $root: IRootScope,
            public $scope: IMainSidebarScope,
            public $http: angular.IHttpService,
            public $timeout: angular.ITimeoutService,
            public mainSidebarService: MainSidebarService) {
            super($root, $scope);

            this.init();
        }

        init() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            $scope.$on('onLastRepeat', function (scope, element, attrs) {
                self.$timeout(function () {
                    if (!$root.miniSidebarActive) {
                        // activate current section
                        $('#sidebar_main').find('.current_section > a').trigger('click');
                    } else {
                        // add tooltips to mini sidebar
                        var tooltip_elem = $('#sidebar_main').find('.menu_tooltip');
                        tooltip_elem.each(function () {
                            var $this = $(this);

                            $this.attr('title', $this.find('.menu_title').text());
                            UIkit.tooltip($this, {
                                pos: 'right'
                            });
                        });
                    }
                })
            });

            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            $scope.$on('ActiveUserRoleChanged', getMenuLinks);

            function getMenuLinks() {

                self.mainSidebarService.getMenuLinks()
                    .then(function (response) {

                        var result = response.data;

                        if (result.success) {
                            $root.menus = result.data;

                            if (self.$root.$state.current.name != 'restricted.management.userProfile' &&
                                self.$root.$state.current.name != 'login' &&
                                $root.menus &&
                                $root.menus.filter(item => item.route == self.$root.$state.current.name).length == 0) {

                                $root.notify("You don't have permission to this page", NotifyTypes.Warning);
                                //$root.$state.go('login');
                                $root.$state.go('restricted.dashboard');
                            }

                            $scope.allMenus = result.data;
                            $scope.rootMenus = $scope.allMenus.filter(item => !item.parentId);
                            $scope.finalMenu = $scope.rootMenus;

                            for (var theRootMenuItem of $scope.rootMenus) {
                                // fill its submenus (level 2)
                                theRootMenuItem.subMenus = $scope.allMenus.filter(x => x.parentId == theRootMenuItem.id);

                                // fill the level 3 subMenus (if any)
                                for (var theLevel2RootMenuItem of theRootMenuItem.subMenus) {
                                    theLevel2RootMenuItem.subMenus = [];
                                    theLevel2RootMenuItem.subMenus = $scope.allMenus.filter(x => x.parentId == theLevel2RootMenuItem.id);
                                }
                            }

                            $scope.sections = $scope.finalMenu;
                        }
                        else
                            $root.handleError(arguments);
                    }, $root.handleError);
            }

            getMenuLinks();
        }

        getUserDisplayName() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            return $root.getUserDisplay();
        }
    }

    angular
        .module('altairApp')
        .controller('MainSidebarController', MainSidebarController);
}
