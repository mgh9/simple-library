




interface IMenuLinkDto extends IGeneralDto 
{   
    parentId: number;
    route: string;
    icon: string;
    orderNumber: number;
    subMenus: IMenuLinkDto[];
    owners: IMenuLinkOwnerDto[];
}