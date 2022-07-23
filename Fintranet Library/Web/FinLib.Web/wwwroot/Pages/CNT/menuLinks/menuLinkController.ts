namespace FinLib.Pages.CNT {
    export class MenuLinkController extends GeneralEntityController<IMenuLinkDto, IMenuLinkView, IMenuLinkSearchFilter> {
        public static $inject = ['$rootScope', '$scope', '$http', 'RoleService', 'MenuLinkService'];

        constructor(public $root: IRootScope,
            public $scope: IMenuLinkScope,
            public $http: angular.IHttpService,
            public roleService: SEC.RoleService,
            public menuLinkService: MenuLinkService) {

            super($root, $scope, menuLinkService);

            this.init();
        }

        init() {
            super.init();

            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            self.$scope.roles = [];
            self.$scope.menuLinks = [];

            $scope.$on('addOrEditDialogShown', function () {
                self.menuLinkService.getTitleValueList()
                    .then(function (response) {
                        var result = response.data;

                        if (result.success) {
                            $scope.menuLinks = result.data;
                        }
                        else
                            self.$root.handleError(arguments);

                    }, self.$root.handleError);
            });
        }

        initSearchFilters(): IMenuLinkSearchFilter {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            var theSearchFilter = super.initSearchFilters();

            theSearchFilter.parentMenuLinkTitle = {
                value: undefined,
                value2: undefined,
                isIgnore: undefined,
                conditionType: ConditionType.Contains,
                columnName: undefined,
                valueType: ValueType.Text
            };

            return theSearchFilter;
        }

        clear() {
            var self = this,
                $scope = self.$scope;

            super.clear();
        }

        clearSearchFilters() {
            var self = this,
                $scope = self.$scope;

            super.clearSearchFilters();

            $scope.request.searchFilterModel.parentMenuLinkTitle.value = undefined;
        }

        addRole($event: ng.IAngularEvent) {
            var self = this,
                $root = self.$root,
                $scope = self.$scope;

            if ($scope.entity.owners == undefined)
                $scope.entity.owners = [];

            if (!$scope.roleId) {
                $root.notify('لطفا یک نقش از لیست انتخاب کنید', NotifyTypes.Warning);

                $event.preventDefault();
                $event.stopPropagation();

                return;
            }

            var role = $scope.roles.filter(function (item) {
                return item.value == $scope.roleId;
            })[0];

            if (_.any($scope.entity.owners, item => item.roleId == role.value)) {
                $root.notify('نقش مورد نظر تکراری است.', NotifyTypes.Warning);

                $event.preventDefault();
                $event.stopPropagation();

                return;
            }

            var menuLinkOwnerDto: IMenuLinkOwnerDto = {
                id: undefined,
                menuLinkId: 0,
                roleId: role.value,
                roleTitle: role.title,
                updateDate: undefined,
            };

            $scope.entity.owners.push(menuLinkOwnerDto);

            $scope.roleId = undefined;

            $event.preventDefault();
            $event.stopPropagation();
        }

        removeRole($event: ng.IAngularEvent, item: IMenuLinkOwnerDto) {
            var self = this,
                $scope = self.$scope;

            var index = $scope.entity.owners.indexOf(item);

            $scope.entity.owners.splice(index, 1);

            $event.preventDefault();
            $event.stopPropagation();
        }
    }

    angular
        .module('altairApp')
        .controller('MenuLinkController', MenuLinkController);
}