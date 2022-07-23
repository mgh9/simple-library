namespace FinLib.Pages.Shared {
    export interface IHeaderScope extends IBaseScope<HeaderController> {
        user_data: any;
        alerts_length: number;
        messages_length: number;

        currentDate: string;
        currentTime: string;

        userDisplayName: string;
    }
}