




interface IUserDto extends IUpdatableDto 
{   
    firstName: string;
    lastName: string;
    fullName: string;
    gender: Gender;
    birthDate: Date;
    mobile: string;
    imageUrl: string;
    userName: string;
    password: string;
    email: string;
    isActive: boolean;
    lastLoggedInTime: Date;
    lockoutDescription: string;
    userRoles: IUserRoleDto[];
}