




interface IUserRoleDto extends IUpdatableDto 
{   
    userId: number;
    roleId: number;
    roleName: string;
    roleTitle: string;
    isDefault: boolean;
    isActive: boolean;
}