namespace FinLib.Pages {
    export interface IDashboardScope extends IBaseScope<DashboardController> {
        userInfo: IUserProfileDto;
    }
}
