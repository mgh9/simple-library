namespace FinLib.Pages.SEC {
    export class UserProfileController extends BaseController {
        public static $inject = [
            '$rootScope',
            '$scope',
            '$http',

            'UserProfileService'
        ];

        public changePasswordDialog: UIkit.ModalElement;

        constructor(public $root: IRootScope,
            public $scope: IUserProfileScope,
            public $http: angular.IHttpService,
            public userProfileService: UserProfileService
        ) {
            super($root, $scope);
            $scope.self = this;

            this.init();
        }

        init() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            $scope.gendersConfig = angular.extend({}, $root.selectizeConfig, {
                placeholder: 'Gender',
            });
            $scope.genders = GenderTitleValues.slice(1, 3);

            //
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
        }

        cancelUpdateProfile() {
            var self = this,
                $scope = self.$scope;

            $scope.isEditMode = false;
        }

        updateProfile() {
            var self = this,
                $scope = self.$scope;

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
            } else {
                $scope.isEditMode = true;
            }
        }

        initChangePasswordDialog() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            self.changePasswordDialog = UIkit.modal("#change-password-dialog", dialogsOptions.notClosable);

            $scope.passwordLengthPolicyText = `The password length should be between ${$scope.userProfileConfig.changePasswordConfig.passwordPolicy.requiredLength} and ${$scope.userProfileConfig.changePasswordConfig.passwordPolicy.maxLength}`;

            if ($scope.userProfileConfig.changePasswordConfig.passwordPolicy.requireDigit) {
                $scope.passwordDigitRequirePolicyText = `Must contain digits`;
            }

            if ($scope.userProfileConfig.changePasswordConfig.passwordPolicy.requireLowercase) {
                $scope.passwordLowercaseRequirePolicyText = `Must contain lowercase`;
            }

            if ($scope.userProfileConfig.changePasswordConfig.passwordPolicy.requireUppercase) {
                $scope.passwordUppercaseRequirePolicyText = `Must contain UPPERCASE`;
            }

            if ($scope.userProfileConfig.changePasswordConfig.passwordPolicy.requireNonAlphanumeric) {
                $scope.passwordNonAlphabetRequirePolicyText = `Must contain non-alphanumeric`;
            }
        }

        showChangePasswordDialog($event: ng.IAngularEvent) {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            $scope.changePasswordModel = {
                id: $scope.userProfile.id,
                currentPassword: undefined,
                newPassword: undefined,
                newPasswordRepeat: undefined
            };

            self.changePasswordDialog.show();
        }

        cancelChangePassword($event: angular.IAngularEvent) {
            var self = this,
                $scope = self.$scope;

            self.changePasswordDialog.hide();

            $event.preventDefault();
            $event.stopPropagation();
        }

        validateOnChangePassword() {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

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
        }

        changePassword($event: angular.IAngularEvent) {
            var self = this,
                $scope = self.$scope,
                $root = self.$root;

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
        }
    }

    angular
        .module('altairApp')
        .controller('UserProfileController', UserProfileController);
}