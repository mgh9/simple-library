using System.ComponentModel;

namespace FinLib.Models.Enums
{
    public enum EventId
    {
        /* General */
        Start = 0,
        Processing = 1,
        End = 2,

        /* Entity Management */
        EntityCreate = 50,
        EntityUpdate = 51,
        EntityDelete = 52,
        EntityDeleteList = 53,
        EntityRead = 54,
        EntityReadList = 55,

        /* AUTHENTICATION */
        UserLoginSuccess = 1000,
        UserLoginFailure = 1001,
        UserLogoutSuccess = 1002,
        AdminLoginFailure = 1003,
        InactiveUserTryToLogin = 1004,
        UserLockoutByWrongPasswordAttempts = 1005,

        SendingOtpToUser = 1007,

        MultiFactorAuthenticationNeeded = 1008,
        MultiFactorAuthenticationSuccess = 1009,
        MultiFactorAuthenticationFailed = 1110,

        /* ERROR */
        UnhandledException = 3000,
        AccessDenied = 3002,
        BadRequest = 3003,
        ResourceNotFound = 3004,
        MethodNotAllowed = 3005,

        /* USER MANAGEMENT */
        UserPasswordChange = 10000,
        UserPasswordReset = 10001,
        UserLockoutByAdmin = 10002,
        UserUnlockByAdmin = 10003,
        UserPasswordResetAndSendOtpToUser = 10004,
    }
}
