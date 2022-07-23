




interface IChangePasswordDto extends IBaseDto 
{   
    id: number;
    currentPassword: string;
    newPassword: string;
    newPasswordRepeat: string;
}