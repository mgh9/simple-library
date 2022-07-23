




interface IUserSearchFilter extends IUpdatableEntitySearchFilter
{

    firstName: ISearchFilterItem<string>;
    lastName: ISearchFilterItem<string>;
    userName: ISearchFilterItem<string>;
    isActive: ISearchFilterItem<boolean>;
    
}