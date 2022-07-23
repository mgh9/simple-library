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
        var CNT;
        (function (CNT) {
            var MenuLinkController = (function (_super) {
                __extends(MenuLinkController, _super);
                function MenuLinkController($root, $scope, $http, roleService, menuLinkService) {
                    var _this = _super.call(this, $root, $scope, menuLinkService) || this;
                    _this.$root = $root;
                    _this.$scope = $scope;
                    _this.$http = $http;
                    _this.roleService = roleService;
                    _this.menuLinkService = menuLinkService;
                    _this.init();
                    return _this;
                }
                MenuLinkController.prototype.init = function () {
                    _super.prototype.init.call(this);
                    var self = this, $scope = self.$scope, $root = self.$root;
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
                };
                MenuLinkController.prototype.initSearchFilters = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    var theSearchFilter = _super.prototype.initSearchFilters.call(this);
                    theSearchFilter.parentMenuLinkTitle = {
                        value: undefined,
                        value2: undefined,
                        isIgnore: undefined,
                        conditionType: ConditionType.Contains,
                        columnName: undefined,
                        valueType: ValueType.Text
                    };
                    return theSearchFilter;
                };
                MenuLinkController.prototype.clear = function () {
                    var self = this, $scope = self.$scope;
                    _super.prototype.clear.call(this);
                };
                MenuLinkController.prototype.clearSearchFilters = function () {
                    var self = this, $scope = self.$scope;
                    _super.prototype.clearSearchFilters.call(this);
                    $scope.request.searchFilterModel.parentMenuLinkTitle.value = undefined;
                };
                MenuLinkController.prototype.addRole = function ($event) {
                    var self = this, $root = self.$root, $scope = self.$scope;
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
                    if (_.any($scope.entity.owners, function (item) { return item.roleId == role.value; })) {
                        $root.notify('نقش مورد نظر تکراری است.', NotifyTypes.Warning);
                        $event.preventDefault();
                        $event.stopPropagation();
                        return;
                    }
                    var menuLinkOwnerDto = {
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
                };
                MenuLinkController.prototype.removeRole = function ($event, item) {
                    var self = this, $scope = self.$scope;
                    var index = $scope.entity.owners.indexOf(item);
                    $scope.entity.owners.splice(index, 1);
                    $event.preventDefault();
                    $event.stopPropagation();
                };
                MenuLinkController.$inject = ['$rootScope', '$scope', '$http', 'RoleService', 'MenuLinkService'];
                return MenuLinkController;
            }(GeneralEntityController));
            CNT.MenuLinkController = MenuLinkController;
            angular
                .module('altairApp')
                .controller('MenuLinkController', MenuLinkController);
        })(CNT = Pages.CNT || (Pages.CNT = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=menuLinkController.js.map