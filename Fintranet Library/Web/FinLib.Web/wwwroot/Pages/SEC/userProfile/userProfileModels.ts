namespace FinLib.Pages.SEC {
    export interface IUserProfileScope extends IBaseScope<UserProfileController> {
        isEditMode: boolean;

        userProfile: IUserProfileDto;
        userProfileConfig: IUserProfileConfigDto;
        userProfileForEdit: IUserProfileDto;

        genders: ITitleValue<number>[];
        gendersConfig: any;

        /**
         * Change Password 
         * */
        changePasswordModel: IChangePasswordDto;

        passwordLengthPolicyText: string;
        passwordDigitRequirePolicyText: string;
        passwordNonAlphabetRequirePolicyText: string;
        passwordUppercaseRequirePolicyText: string;
        passwordLowercaseRequirePolicyText: string;
    }
}