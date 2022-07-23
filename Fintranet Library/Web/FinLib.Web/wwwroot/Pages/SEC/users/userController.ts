namespace FinLib.Pages.SEC {
    export class UserController extends UpdatableEntityController<IUserDto, IUserView, IUserSearchFilter> {
        public static $inject = [
            '$rootScope',
            '$scope',
            '$http',
            '$window',
            '$location',

            'RoleService',
            'UserService'];

        constructor(public $root: IRootScope,
            public $scope: IUserScope,
            public $http: angular.IHttpService,
            public $window: angular.IWindowService,
            public $location: angular.ILocationService,

            public roleService: SEC.RoleService,
            public userService: UserService) {

            super($root, $scope, userService, 'userName');

            this.init();
            this.initSearchFilters();
        }

        init() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            self.initCustomColumns();
            self.initPasswordPolicyDialog();
            $scope.roles = [];

            // 
            $scope.gendersConfig = angular.extend({}, $root.selectizeConfig, {
                placeholder: 'Gender',
            });
            $scope.genders = GenderTitleValues;

            $scope.$on('editDialogShown', function () {

            });
        }

        initCustomColumns() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            $scope.gridViewConfig = {
                elementId: 'users_grid',
                showCheckbox: false,
                deleteLinkVisible: true,
                editLinkVisible: true,
                singleViewLinkVisible: true,
                lastUpdateTimeVisible: true,
                customActions: [
                    {
                        click: (event, row) => {
                            self.showPasswordResetDialog(event, row)
                        },

                        icon: "vpn_key",
                        title: "Reset password",
                        color: "md-color-red-500",
                        tooltip: "Reset password for this user"
                    },
                ]
            };
        }

        prepareDataOnEditDialogShown(data) {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

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
        }

        initSearchFilters(): IUserSearchFilter {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            var theSearchFilter = super.initSearchFilters();

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
        }

        clear() {
            var self = this,
                $scope = self.$scope;

            super.clear();

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
        }

        clearSearchFilters() {
            super.clearSearchFilters();

            var self = this,
                $scope = self.$scope;

            $scope.request.searchFilterModel.firstName.value = undefined;
            $scope.request.searchFilterModel.lastName.value = undefined;
            $scope.request.searchFilterModel.userName.value = undefined;
        }

        closeSingleViewDialog($event: ng.IAngularEvent) {
            var self = this,
                $scope = self.$scope;

            super.closeSingleViewDialog($event);

            // refresh the selected user row
            self.filterTable();
        }

        addRole($event: ng.IAngularEvent) {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            if (!$scope.roleId) {
                $event.preventDefault();
                $event.stopPropagation();

                $root.notify('Please select a role', NotifyTypes.Warning);
                return;
            }

            let selectedRoleToAdd = $scope.roles.filter(function (item) {
                return item.value == $scope.roleId;
            })[0];

            if (_.any($scope.entity.userRoles, item => item.roleId == selectedRoleToAdd.value)) {
                $root.notify('the selected role already added', NotifyTypes.Warning);

                $event.preventDefault();
                $event.stopPropagation();

                return;
            }

            let newUserRole: IUserRoleDto =
            {
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
        }

        toggleRoleDisableOrEnable($event: ng.IAngularEvent, item: IUserRoleDto, value: boolean) {
            var self = this,
                $scope = self.$scope;

            var index = $scope.entity.userRoles.indexOf(item);

            $scope.entity.userRoles[index].isActive = value;

            $event.preventDefault();
            $event.stopPropagation();
        }

        removeRole($event: ng.IAngularEvent, item: IUserRoleDto) {
            var self = this,
                $scope = self.$scope;

            var index = $scope.entity.userRoles.indexOf(item);

            $scope.entity.userRoles.splice(index, 1);

            $event.preventDefault();
            $event.stopPropagation();
        }

        // Reset Password Dialog \\
        initPasswordPolicyDialog() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

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
        }

        showPasswordResetDialog($event: ng.IAngularEvent, row: IUserView) {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

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
        }

        preparePasswordPolicyTexts(passwordPolicy: IPasswordPolicy) {
            var self = this,
                $scope = self.$scope;

            $scope.passwordLengthPolicyText = `The password length should be between ${passwordPolicy.requiredLength} and ${passwordPolicy.maxLength}`;

            if (passwordPolicy.requireDigit) {
                $scope.passwordDigitRequirePolicyText = `Must contain digits`;
            }

            if (passwordPolicy.requireLowercase) {
                $scope.passwordLowercaseRequirePolicyText = `Must contain lowercase`;
            }

            if (passwordPolicy.requireUppercase) {
                $scope.passwordUppercaseRequirePolicyText = `Must contain UPPERCASE`;
            }

            if (passwordPolicy.requireNonAlphanumeric) {
                $scope.passwordNonAlphabetRequirePolicyText = `Must contain non-alphanumeric`;
            }
        }

        closeResetPasswordDialog($event: angular.IAngularEvent) {
            var self = this,
                $scope = self.$scope;

            $scope.tableData.rows.forEach(function (row) { row.isSelected = false; })
            $scope.resetPasswordDialog.hide();

            $event.preventDefault();
            $event.stopPropagation();
        }

        resetPassword($event: angular.IAngularEvent, resetPasswordDialogData: IResetPasswordDialogData) {
            var self = this,
                $scope = self.$scope,
                $root = self.$root,
                tableData = $scope.tableData,
                request = $scope.request;

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

                            $root.notify(`The selected user's password "${resetPasswordDialogData.user.fullName}" successfully reset`, NotifyTypes.Success);
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
        }

        validateOnResetPassword(resetPasswordModel: IResetPasswordDto) {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            if (!resetPasswordModel.newPassword || resetPasswordModel.newPassword.trim().length == 0) {
                $root.notify('Please enter the new password', NotifyTypes.Warning);
                return false;
            }

            // we can have some client-side validations here
            // but we have the validations at the back-end

            return true;
        }
    }

    angular
        .module('altairApp')
        .controller('UserController', UserController);
}