var FinLib;
(function (FinLib) {
    var Pages;
    (function (Pages) {
        var DashboardController = (function () {
            function DashboardController($root, $scope, $http, $window, userProfileService, dashboardService) {
                this.$root = $root;
                this.$scope = $scope;
                this.$http = $http;
                this.$window = $window;
                this.userProfileService = userProfileService;
                this.dashboardService = dashboardService;
                $scope.self = this;
                this.init();
            }
            DashboardController.prototype.init = function () {
                var self = this, $scope = self.$scope, $root = self.$root;
                self.userProfileService.get()
                    .then(function (response) {
                    var result = response.data;
                    if (result.success) {
                        $scope.userInfo = result.data;
                    }
                    else {
                        self.$root.handleError(arguments);
                    }
                }, $root.handleError);
                $root.$on('ActiveUserRoleChanged', function (event) {
                });
            };
            DashboardController.$inject = ['$rootScope', '$scope', '$http', '$window', 'UserProfileService', 'DashboardService'];
            return DashboardController;
        }());
        Pages.DashboardController = DashboardController;
        angular
            .module('altairApp')
            .controller('DashboardController', DashboardController);
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=dashboardController.js.map