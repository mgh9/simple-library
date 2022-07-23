namespace FinLib.Pages.CNT {
    export interface IMenuLinkScope extends ITableBaseScope<MenuLinkController, IMenuLinkDto, IMenuLinkView, IMenuLinkSearchFilter> {
        menuLinks: ITitleValue<number>[];

        roleId: number;
        roles: ITitleValue<number>[];
    }
}