namespace FinLib.Pages.SEC {
    export interface IUserScope extends ITableBaseScope<UserController
        , IUserDto, IUserView, IUserSearchFilter> {
        userProfile: IUserProfileDto;

        genders: ITitleValue<number>[];
        gendersConfig: any;

        selectedUserData: IUserView;

        // ROLES Management \\
        roles: ITitleValue<number>[];
        roleId: number;

        // RESET PASSWORD Dialog \\
        resetPasswordDialog: UIkit.ModalElement;
        resetPasswordDialogData: IResetPasswordDialogData;
        resetPasswordModel: IResetPasswordDto;
        passwordPolicy: IPasswordPolicy;

        passwordLengthPolicyText: string;
        passwordDigitRequirePolicyText: string;
        passwordNonAlphabetRequirePolicyText: string;
        passwordUppercaseRequirePolicyText: string;
        passwordLowercaseRequirePolicyText: string;
    }

    export interface IResetPasswordDialogData {
        user: IUserView;
    }
}