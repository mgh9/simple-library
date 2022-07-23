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
            var HeaderController = (function (_super) {
                __extends(HeaderController, _super);
                function HeaderController($root, $scope, $http, $timeout, $interval, $filter, $window, $state, $cookies, headerService) {
                    var _this = _super.call(this, $root, $scope) || this;
                    _this.$root = $root;
                    _this.$scope = $scope;
                    _this.$http = $http;
                    _this.$timeout = $timeout;
                    _this.$interval = $interval;
                    _this.$filter = $filter;
                    _this.$window = $window;
                    _this.$state = $state;
                    _this.$cookies = $cookies;
                    _this.headerService = headerService;
                    _this.init();
                    return _this;
                }
                HeaderController.prototype.init = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    function setDate() {
                        var persianDateTimeFilter = self.$filter('toPersianDateTime');
                        var persianDateTime = persianDateTimeFilter(new Date());
                        var persianDate = persianDateTime.substr(0, 10);
                        var persianTime = persianDateTime.substr(11, 5);
                        $scope.currentDate = new Date().toLocaleDateString();
                        $scope.currentTime = persianTime;
                    }
                    self.$interval(setDate, 5000);
                    setDate();
                    $('#menu_top').children('[data-uk-dropdown]').on('show.uk.dropdown', function () {
                        self.$timeout(function () {
                            $(self.$window).resize();
                        }, 280);
                    });
                    $('.header_main_search_form').on('click', '#autocomplete_results .item', function (e) {
                        e.preventDefault();
                        var $this = $(this);
                        self.$state.go($this.attr('href'));
                        $('.header_main_search_input').val('');
                    });
                };
                HeaderController.prototype.setDefaultUserRole = function ($event, userRole) {
                    var self = this, $root = self.$root, $scope = self.$scope, $cookies = self.$cookies;
                    $root.userInfo.defaultUserRoleId = userRole.id;
                    self.headerService.setDefaultUserRole(userRole.id)
                        .then(function () {
                        $root.$broadcast('ActiveUserRoleChanged');
                        self.$state.go('restricted.dashboard');
                    });
                    $event.preventDefault();
                    $event.stopPropagation();
                };
                HeaderController.prototype.getCurrentPersianDate = function () {
                    return Date.now();
                };
                HeaderController.prototype.getUserDisplayName = function () {
                    var self = this, $root = self.$root, $scope = self.$scope, $cookies = self.$cookies;
                    var userInfo = $root.userInfo;
                    if (!userInfo)
                        return 'Unknown';
                    var role = userInfo.userRoles.filter(function (item) {
                        return item.id == userInfo.defaultUserRoleId;
                    })[0];
                    $scope.userDisplayName = "".concat(userInfo.firstName, " - ").concat(userInfo.lastName, " (").concat(role.roleTitle, ")");
                    return $scope.userDisplayName;
                };
                HeaderController.prototype.logout = function ($event) {
                    var self = this, $root = self.$root, $scope = self.$scope, $state = self.$state;
                    window.location.href = "/Account/Logout";
                    $event.preventDefault();
                    $event.stopPropagation();
                };
                HeaderController.$inject = ['$rootScope', '$scope', '$http', '$timeout', '$interval', '$filter', '$window', '$state', '$cookies', 'HeaderService'];
                return HeaderController;
            }(BaseController));
            Shared.HeaderController = HeaderController;
            angular
                .module('altairApp')
                .controller('HeaderController', HeaderController);
        })(Shared = Pages.Shared || (Pages.Shared = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=headerController.js.map