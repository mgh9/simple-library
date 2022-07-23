var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var FinLib;
(function (FinLib) {
    var Pages;
    (function (Pages) {
        var Shared;
        (function (Shared) {
            var MainSidebarController = (function (_super) {
                __extends(MainSidebarController, _super);
                function MainSidebarController($root, $scope, $http, $timeout, mainSidebarService) {
                    var _this = _super.call(this, $root, $scope) || this;
                    _this.$root = $root;
                    _this.$scope = $scope;
                    _this.$http = $http;
                    _this.$timeout = $timeout;
                    _this.mainSidebarService = mainSidebarService;
                    _this.init();
                    return _this;
                }
                MainSidebarController.prototype.init = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    $scope.$on('onLastRepeat', function (scope, element, attrs) {
                        self.$timeout(function () {
                            if (!$root.miniSidebarActive) {
                                $('#sidebar_main').find('.current_section > a').trigger('click');
                            }
                            else {
                                var tooltip_elem = $('#sidebar_main').find('.menu_tooltip');
                                tooltip_elem.each(function () {
                                    var $this = $(this);
                                    $this.attr('title', $this.find('.menu_title').text());
                                    UIkit.tooltip($this, {
                                        pos: 'right'
                                    });
                                });
                            }
                        });
                    });
                    var self = this, $scope = self.$scope, $root = self.$root;
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
                                    $root.menus.filter(function (item) { return item.route == self.$root.$state.current.name; }).length == 0) {
                                    $root.notify("You don't have permission to this page", NotifyTypes.Warning);
                                    $root.$state.go('restricted.dashboard');
                                }
                                $scope.allMenus = result.data;
                                $scope.rootMenus = $scope.allMenus.filter(function (item) { return !item.parentId; });
                                $scope.finalMenu = $scope.rootMenus;
                                for (var _i = 0, _a = $scope.rootMenus; _i < _a.length; _i++) {
                                    var theRootMenuItem = _a[_i];
                                    theRootMenuItem.subMenus = $scope.allMenus.filter(function (x) { return x.parentId == theRootMenuItem.id; });
                                    for (var _b = 0, _c = theRootMenuItem.subMenus; _b < _c.length; _b++) {
                                        var theLevel2RootMenuItem = _c[_b];
                                        theLevel2RootMenuItem.subMenus = [];
                                        theLevel2RootMenuItem.subMenus = $scope.allMenus.filter(function (x) { return x.parentId == theLevel2RootMenuItem.id; });
                                    }
                                }
                                $scope.sections = $scope.finalMenu;
                            }
                            else
                                $root.handleError(arguments);
                        }, $root.handleError);
                    }
                    getMenuLinks();
                };
                MainSidebarController.prototype.getUserDisplayName = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    return $root.getUserDisplay();
                };
                MainSidebarController.$inject = ['$rootScope', '$scope', '$http', '$timeout', 'MainSidebarService'];
                return MainSidebarController;
            }(BaseController));
            Shared.MainSidebarController = MainSidebarController;
            angular
                .module('altairApp')
                .controller('MainSidebarController', MainSidebarController);
        })(Shared = Pages.Shared || (Pages.Shared = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=mainSidebarController.js.map