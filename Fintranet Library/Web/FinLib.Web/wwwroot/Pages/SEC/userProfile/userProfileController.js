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
            var UserProfileController = (function (_super) {
                __extends(UserProfileController, _super);
                function UserProfileController($root, $scope, $http, userProfileService) {
                    var _this = _super.call(this, $root, $scope) || this;
                    _this.$root = $root;
                    _this.$scope = $scope;
                    _this.$http = $http;
                    _this.userProfileService = userProfileService;
                    $scope.self = _this;
                    _this.init();
                    return _this;
                }
                UserProfileController.prototype.init = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    $scope.gendersConfig = angular.extend({}, $root.selectizeConfig, {
                        placeholder: 'جنسیت',
                    });
                    $scope.genders = GenderTitleValues.slice(1, 3);
                    self.userProfileService.get().then(function (response) {
                        var result = response.data;
                        if (result.success) {
                            $scope.userProfile = result.data;
                            $scope.userProfileConfig = result.dataConfig;
                            self.initChangePasswordDialog();
                            $scope.userProfileForEdit = {
                                firstName: result.data.firstName,
                                lastName: result.data.lastName,
                                fullName: result.data.fullName,
                                mobile: result.data.mobile,
                                gender: result.data.gender,
                                imageUrl: result.data.imageUrl,
                                imageAbsoluteUrl: result.data.imageAbsoluteUrl,
                                birthDate: result.data.birthDate,
                                id: result.data.id,
                                userName: result.data.userName,
                                email: result.data.email,
                                isActive: result.data.isActive,
                                lastLoggedInTime: result.data.lastLoggedInTime,
                                lockoutEnd: result.data.lockoutEnd,
                                roles: result.data.roles,
                            };
                        }
                        else
                            self.$root.handleError(arguments);
                    }, self.$root.handleError);
                    $scope.isEditMode = false;
                };
                UserProfileController.prototype.cancelUpdateProfile = function () {
                    var self = this, $scope = self.$scope;
                    $scope.isEditMode = false;
                };
                UserProfileController.prototype.updateProfile = function () {
                    var self = this, $scope = self.$scope;
                    if ($scope.isEditMode) {
                        self.userProfileService.saveData($scope.userProfileForEdit).then(function (response) {
                            var result = response.data;
                            if (result.success) {
                                $scope.isEditMode = false;
                                $scope.userProfile.firstName = $scope.userProfileForEdit.firstName;
                                $scope.userProfile.lastName = $scope.userProfileForEdit.lastName;
                                $scope.userProfile.gender = $scope.userProfileForEdit.gender;
                                $scope.userProfile.mobile = $scope.userProfileForEdit.mobile;
                                $scope.userProfile.imageUrl = $scope.userProfileForEdit.imageUrl;
                            }
                            else
                                self.$root.handleError(arguments);
                        }, self.$root.handleError);
                    }
                    else {
                        $scope.isEditMode = true;
                    }
                };
                UserProfileController.prototype.initChangePasswordDialog = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    self.changePasswordDialog = UIkit.modal("#change-password-dialog", dialogsOptions.notClosable);
                    $scope.passwordLengthPolicyText = "The password length should be between ".concat($scope.userProfileConfig.changePasswordConfig.passwordPolicy.requiredLength, " and ").concat($scope.userProfileConfig.changePasswordConfig.passwordPolicy.maxLength);
                    if ($scope.userProfileConfig.changePasswordConfig.passwordPolicy.requireDigit) {
                        $scope.passwordDigitRequirePolicyText = "Must contain digits";
                    }
                    if ($scope.userProfileConfig.changePasswordConfig.passwordPolicy.requireLowercase) {
                        $scope.passwordLowercaseRequirePolicyText = "Must contain lowercase";
                    }
                    if ($scope.userProfileConfig.changePasswordConfig.passwordPolicy.requireUppercase) {
                        $scope.passwordUppercaseRequirePolicyText = "Must contain UPPERCASE";
                    }
                    if ($scope.userProfileConfig.changePasswordConfig.passwordPolicy.requireNonAlphanumeric) {
                        $scope.passwordNonAlphabetRequirePolicyText = "Must contain non-alphanumeric";
                    }
                };
                UserProfileController.prototype.showChangePasswordDialog = function ($event) {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    $scope.changePasswordModel = {
                        id: $scope.userProfile.id,
                        currentPassword: undefined,
                        newPassword: undefined,
                        newPasswordRepeat: undefined
                    };
                    self.changePasswordDialog.show();
                };
                UserProfileController.prototype.cancelChangePassword = function ($event) {
                    var self = this, $scope = self.$scope;
                    self.changePasswordDialog.hide();
                    $event.preventDefault();
                    $event.stopPropagation();
                };
                UserProfileController.prototype.validateOnChangePassword = function () {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    if (!$scope.changePasswordModel.currentPassword || $scope.changePasswordModel.currentPassword.length < 1) {
                        $root.notify('Please enter your current password', NotifyTypes.Warning);
                        return;
                    }
                    if (!$scope.changePasswordModel.newPassword || $scope.changePasswordModel.newPassword.length < 1) {
                        $root.notify('Please enter your new password', NotifyTypes.Warning);
                        return;
                    }
                    if (!$scope.changePasswordModel.newPasswordRepeat || $scope.changePasswordModel.newPasswordRepeat.length < 1) {
                        $root.notify('Please enter new password repeat', NotifyTypes.Warning);
                        return;
                    }
                    if ($scope.changePasswordModel.newPassword != $scope.changePasswordModel.newPasswordRepeat) {
                        $root.notify('Your new password and repeat password should be the same', NotifyTypes.Warning);
                        return;
                    }
                    return true;
                };
                UserProfileController.prototype.changePassword = function ($event) {
                    var self = this, $scope = self.$scope, $root = self.$root;
                    if ($root.isLoading)
                        return;
                    if (!self.validateOnChangePassword()) {
                        return;
                    }
                    self.userProfileService.changePassword($scope.changePasswordModel)
                        .then(function (response) {
                        var result = response.data;
                        if (result.success) {
                            $root.notify('Your password successfully changed. Please signout of your account', NotifyTypes.Success);
                            self.changePasswordDialog.hide();
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
                };
                UserProfileController.$inject = [
                    '$rootScope',
                    '$scope',
                    '$http',
                    'UserProfileService'
                ];
                return UserProfileController;
            }(BaseController));
            SEC.UserProfileController = UserProfileController;
            angular
                .module('altairApp')
                .controller('UserProfileController', UserProfileController);
        })(SEC = Pages.SEC || (Pages.SEC = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=userProfileController.js.map