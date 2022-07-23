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
        var SEC;
        (function (SEC) {
            var UserController = (function (_super) {
                __extends(UserController, _super);
                function UserController($root, $scope, $http, $window, $location, roleService, userService) {
                    var _this = _super.call(this, $root, $scope, userService, 'userName') || this;
                    _this.$root = $root;
                    _this.$scope = $scope;
                    _this.$http = $http;
                    _this.$window = $window;
                    _this.$location = $location;
                    _this.roleService = roleService;
                    _this.userService = userService;
                    _this.init();
                    _this.initSearchFilters();
                    return _this;
                }
                UserController.prototype.init = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    self.initCustomColumns();
                    self.initPasswordPolicyDialog();
                    $scope.roles = [];
                    $scope.gendersConfig = angular.extend({}, $root.selectizeConfig, {
                        placeholder: 'Gender',
                    });
                    $scope.genders = GenderTitleValues;
                    $scope.$on('editDialogShown', function () {
                    });
                };
                UserController.prototype.initCustomColumns = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    $scope.gridViewConfig = {
                        elementId: 'users_grid',
                        showCheckbox: false,
                        deleteLinkVisible: true,
                        editLinkVisible: true,
                        singleViewLinkVisible: true,
                        lastUpdateTimeVisible: true,
                        customActions: [
                            {
                                click: function (event, row) {
                                    self.showPasswordResetDialog(event, row);
                                },
                                icon: "vpn_key",
                                title: "Reset password",
                                color: "md-color-red-500",
                                tooltip: "Reset password for this user"
                            },
                        ]
                    };
                };
                UserController.prototype.prepareDataOnEditDialogShown = function (data) {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    $scope.roles = [];
                    self.roleService.getTitleValueList()
                        .then(function (response) {
                        var result = response.data;
                        if (result.success) {
                            $scope.roles = result.data;
                        }
                        else
                            self.$root.handleError(arguments);
                    }, self.$root.handleError);
                };
                UserController.prototype.initSearchFilters = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    var theSearchFilter = _super.prototype.initSearchFilters.call(this);
                    theSearchFilter.firstName =
                        {
                            columnName: undefined,
                            valueType: ValueType.Text,
                            value: undefined,
                            value2: undefined,
                            isIgnore: undefined,
                            conditionType: ConditionType.Contains
                        };
                    theSearchFilter.lastName =
                        {
                            columnName: undefined,
                            valueType: ValueType.Text,
                            value: undefined,
                            value2: undefined,
                            isIgnore: undefined,
                            conditionType: ConditionType.Contains
                        };
                    theSearchFilter.userName =
                        {
                            columnName: undefined,
                            valueType: ValueType.Text,
                            value: undefined,
                            value2: undefined,
                            isIgnore: undefined,
                            conditionType: ConditionType.Contains
                        };
                    theSearchFilter.isActive =
                        {
                            columnName: undefined,
                            valueType: ValueType.Boolean,
                            value: undefined,
                            value2: undefined,
                            isIgnore: undefined,
                            conditionType: ConditionType.Equals
                        };
                    return theSearchFilter;
                };
                UserController.prototype.clear = function () {
                    var self = this, $scope = self.$scope;
                    _super.prototype.clear.call(this);
                    $scope.entity = {
                        updateDate: undefined,
                        firstName: '',
                        lastName: '',
                        fullName: '',
                        lockoutDescription: '',
                        lastLoggedInTime: undefined,
                        gender: undefined,
                        imageUrl: '',
                        mobile: '',
                        birthDate: undefined,
                        id: 0,
                        userName: '',
                        password: undefined,
                        email: '',
                        userRoles: [],
                        isActive: undefined,
                    };
                };
                UserController.prototype.clearSearchFilters = function () {
                    _super.prototype.clearSearchFilters.call(this);
                    var self = this, $scope = self.$scope;
                    $scope.request.searchFilterModel.firstName.value = undefined;
                    $scope.request.searchFilterModel.lastName.value = undefined;
                    $scope.request.searchFilterModel.userName.value = undefined;
                };
                UserController.prototype.closeSingleViewDialog = function ($event) {
                    var self = this, $scope = self.$scope;
                    _super.prototype.closeSingleViewDialog.call(this, $event);
                    self.filterTable();
                };
                UserController.prototype.addRole = function ($event) {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    if (!$scope.roleId) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $root.notify('Please select a role', NotifyTypes.Warning);
                        return;
                    }
                    var selectedRoleToAdd = $scope.roles.filter(function (item) {
                        return item.value == $scope.roleId;
                    })[0];
                    if (_.any($scope.entity.userRoles, function (item) { return item.roleId == selectedRoleToAdd.value; })) {
                        $root.notify('the selected role already added', NotifyTypes.Warning);
                        $event.preventDefault();
                        $event.stopPropagation();
                        return;
                    }
                    var newUserRole = {
                        id: undefined,
                        updateDate: undefined,
                        roleTitle: selectedRoleToAdd.title,
                        roleId: selectedRoleToAdd.value,
                        isActive: true,
                        isDefault: false,
                        roleName: undefined,
                        userId: undefined,
                    };
                    $scope.entity.userRoles.push(newUserRole);
                    $scope.roleId = undefined;
                    $event.preventDefault();
                    $event.stopPropagation();
                };
                UserController.prototype.toggleRoleDisableOrEnable = function ($event, item, value) {
                    var self = this, $scope = self.$scope;
                    var index = $scope.entity.userRoles.indexOf(item);
                    $scope.entity.userRoles[index].isActive = value;
                    $event.preventDefault();
                    $event.stopPropagation();
                };
                UserController.prototype.removeRole = function ($event, item) {
                    var self = this, $scope = self.$scope;
                    var index = $scope.entity.userRoles.indexOf(item);
                    $scope.entity.userRoles.splice(index, 1);
                    $event.preventDefault();
                    $event.stopPropagation();
                };
                UserController.prototype.initPasswordPolicyDialog = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    $scope.resetPasswordDialog = UIkit.modal("#reset-password-dialog", dialogsOptions.notClosable);
                    self.userService.getPasswordPolicy()
                        .then(function (response) {
                        var result = response.data;
                        if (result.success) {
                            $scope.passwordPolicy = result.data;
                            self.preparePasswordPolicyTexts($scope.passwordPolicy);
                        }
                        else {
                            $root.handleError(arguments);
                        }
                    }, $root.handleError);
                };
                UserController.prototype.showPasswordResetDialog = function ($event, row) {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    $scope.resetPasswordDialogData = {
                        user: row
                    };
                    $scope.resetPasswordModel = {
                        subjectUserId: row.id,
                        newPassword: undefined
                    };
                    $scope.resetPasswordDialog.show();
                    $event.preventDefault();
                    $event.stopPropagation();
                };
                UserController.prototype.preparePasswordPolicyTexts = function (passwordPolicy) {
                    var self = this, $scope = self.$scope;
                    $scope.passwordLengthPolicyText = "The password length should be between ".concat(passwordPolicy.requiredLength, " and ").concat(passwordPolicy.maxLength);
                    if (passwordPolicy.requireDigit) {
                        $scope.passwordDigitRequirePolicyText = "Must contain digits";
                    }
                    if (passwordPolicy.requireLowercase) {
                        $scope.passwordLowercaseRequirePolicyText = "Must contain lowercase";
                    }
                    if (passwordPolicy.requireUppercase) {
                        $scope.passwordUppercaseRequirePolicyText = "Must contain UPPERCASE";
                    }
                    if (passwordPolicy.requireNonAlphanumeric) {
                        $scope.passwordNonAlphabetRequirePolicyText = "Must contain non-alphanumeric";
                    }
                };
                UserController.prototype.closeResetPasswordDialog = function ($event) {
                    var self = this, $scope = self.$scope;
                    $scope.tableData.rows.forEach(function (row) { row.isSelected = false; });
                    $scope.resetPasswordDialog.hide();
                    $event.preventDefault();
                    $event.stopPropagation();
                };
                UserController.prototype.resetPassword = function ($event, resetPasswordDialogData) {
                    var self = this, $scope = self.$scope, $root = self.$root, tableData = $scope.tableData, request = $scope.request;
                    if ($root.isLoading)
                        return;
                    if (!self.validateOnResetPassword($scope.resetPasswordModel)) {
                        return;
                    }
                    UIkit.modal.confirm("Are u sure to reset the selected user's password?", function () {
                        self.userService.resetPassword($scope.resetPasswordModel)
                            .then(function (response) {
                            var result = response.data;
                            if (result.success) {
                                self.loadData(request);
                                $scope.resetPasswordDialog.hide();
                                $root.notify("The selected user's password \"".concat(resetPasswordDialogData.user.fullName, "\" successfully reset"), NotifyTypes.Success);
                            }
                            else {
                                if (result.message) {
                                    $root.notify(result.message, NotifyTypes.Warning);
                                }
                                else
                                    $root.handleError(arguments);
                            }
                        }, $root.handleError)
                            .finally(function () {
                            $root.hidePreloader();
                        });
                    }, dialogsOptions.confirm);
                };
                UserController.prototype.validateOnResetPassword = function (resetPasswordModel) {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    if (!resetPasswordModel.newPassword || resetPasswordModel.newPassword.trim().length == 0) {
                        $root.notify('Please enter the new password', NotifyTypes.Warning);
                        return false;
                    }
                    return true;
                };
                UserController.$inject = [
                    '$rootScope',
                    '$scope',
                    '$http',
                    '$window',
                    '$location',
                    'RoleService',
                    'UserService'
                ];
                return UserController;
            }(UpdatableEntityController));
            SEC.UserController = UserController;
            angular
                .module('altairApp')
                .controller('UserController', UserController);
        })(SEC = Pages.SEC || (Pages.SEC = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=userController.js.map