namespace FinLib.Pages {
    export class DashboardController implements angular.IController {

        public static $inject = ['$rootScope', '$scope', '$http', '$window', 'UserProfileService', 'DashboardService'];

        constructor(public $root: IRootScope,
            public $scope: IDashboardScope,
            public $http: angular.IHttpService,
            public $window: any,
            public userProfileService: SEC.UserProfileService,
            public dashboardService: DashboardService) {
            $scope.self = this;

            this.init();
        }

        init() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            //$scope.audits = [];

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

            $root.$on('ActiveUserRoleChanged', (event) => {

            });
        }
    }

    angular
        .module('altairApp')
        .controller('DashboardController', DashboardController);
}