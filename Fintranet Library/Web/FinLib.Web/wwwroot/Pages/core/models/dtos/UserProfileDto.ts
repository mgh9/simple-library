




interface IUserProfileDto extends IBaseDto 
{   
    firstName: string;
    lastName: string;
    fullName: string;
    mobile: string;
    gender: Gender;
    imageUrl: string;
    imageAbsoluteUrl: string;
    birthDate: Date;
    id: number;
    userName: string;
    email: string;
    lockoutEnd: Date;
    isActive: boolean;
    lastLoggedInTime: Date;
    roles: IRoleDto[];
}