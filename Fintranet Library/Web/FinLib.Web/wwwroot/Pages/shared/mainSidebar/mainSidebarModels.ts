namespace FinLib.Pages.Shared {
    export interface IMainSidebarScope extends IBaseScope<MainSidebarController> {
        sections: any[];
                
        langSwitcherModel: any;
        langSwitcherOptions: any[];
        langSwitcherConfig: any;
        allMenus: IMenuLinkDto[];
        rootMenus: IMenuLinkDto[];
        finalMenu: IMenuLinkDto[];
    }
}