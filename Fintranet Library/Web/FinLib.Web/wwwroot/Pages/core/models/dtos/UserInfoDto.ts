




interface IUserInfoDto extends IBaseEntityDto 
{   
    firstName: string;
    lastName: string;
    fullName: string;
    userName: string;
    email: string;
    mobile: string;
    imageUrl: string;
    imageAbsoluteUrl: string;
    activeRole: IRoleDto;
    defaultUserRoleId: number;
    userRoles: IUserRoleDto[];
}