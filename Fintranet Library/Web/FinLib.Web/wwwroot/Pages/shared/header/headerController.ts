namespace FinLib.Pages.Shared {
    export class HeaderController extends BaseController {
        public static $inject = ['$rootScope', '$scope', '$http', '$timeout', '$interval', '$filter', '$window', '$state', '$cookies', 'HeaderService'];

        constructor(public $root: IRootScope,
            public $scope: IHeaderScope,
            public $http: angular.IHttpService,
            public $timeout: angular.ITimeoutService,
            public $interval: angular.IIntervalService,
            public $filter: ng.IFilterService,
            public $window: any,
            public $state: any,
            public $cookies: any,
            public headerService: HeaderService) {
            super($root, $scope);

            this.init();
        }

        init() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;
         
            function setDate() {
                var persianDateTimeFilter = <any>self.$filter('toPersianDateTime');
                var persianDateTime = persianDateTimeFilter(new Date());
                var persianDate: string = persianDateTime.substr(0, 10);
                var persianTime: string = persianDateTime.substr(11, 5);

                $scope.currentDate = new Date().toLocaleDateString();
                $scope.currentTime = persianTime;
            }

            self.$interval(setDate, 5000);
            setDate();

            $('#menu_top').children('[data-uk-dropdown]').on('show.uk.dropdown', function () {
                self.$timeout(function () {
                    $(self.$window).resize();
                }, 280)
            });

            // autocomplete
            $('.header_main_search_form').on('click', '#autocomplete_results .item', function (e) {
                e.preventDefault();
                var $this = $(this);
                self.$state.go($this.attr('href'));
                $('.header_main_search_input').val('');
            });

            //$root.$on('GetUserInfoRequested', function () {
            //    //self.setUserDisplayName();
            //});

            ////self.setUserDisplayName();
        }

        setDefaultUserRole($event: ng.IAngularEvent, userRole) {
            var self = this,
                $root = self.$root,
                $scope = self.$scope,
                $cookies = self.$cookies;

            $root.userInfo.defaultUserRoleId = userRole.id;

            self.headerService.setDefaultUserRole(userRole.id)
                .then(function () {
                    $root.$broadcast('ActiveUserRoleChanged');
                    self.$state.go('restricted.dashboard');
                });

            $event.preventDefault();
            $event.stopPropagation();
        }

        getCurrentPersianDate() {
            return Date.now();
        }

        getUserDisplayName() {
            var self = this,
                $root = self.$root,
                $scope = self.$scope,
                $cookies = self.$cookies;

            var userInfo = $root.userInfo;

            if (!userInfo)
                return 'Unknown';

            var role = userInfo.userRoles.filter(function (item) {
                return item.id == userInfo.defaultUserRoleId;
            })[0];

            $scope.userDisplayName = `${userInfo.firstName} - ${userInfo.lastName} (${role.roleTitle})`;

            return $scope.userDisplayName;
        }

        logout($event: ng.IAngularEvent) {
            var self = this,
                $root = self.$root,
                $scope = self.$scope,
                $state = self.$state;

            window.location.href = "/Account/Logout";

            $event.preventDefault();
            $event.stopPropagation();
        }
    }

    angular
        .module('altairApp')
        .controller('HeaderController', HeaderController);
}