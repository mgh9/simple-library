var EventId;
(function (EventId) {
    EventId[EventId["Start"] = 0] = "Start";
    EventId[EventId["Processing"] = 1] = "Processing";
    EventId[EventId["End"] = 2] = "End";
    EventId[EventId["EntityCreate"] = 50] = "EntityCreate";
    EventId[EventId["EntityUpdate"] = 51] = "EntityUpdate";
    EventId[EventId["EntityDelete"] = 52] = "EntityDelete";
    EventId[EventId["EntityDeleteList"] = 53] = "EntityDeleteList";
    EventId[EventId["EntityRead"] = 54] = "EntityRead";
    EventId[EventId["EntityReadList"] = 55] = "EntityReadList";
    EventId[EventId["UserLoginSuccess"] = 1000] = "UserLoginSuccess";
    EventId[EventId["UserLoginFailure"] = 1001] = "UserLoginFailure";
    EventId[EventId["UserLogoutSuccess"] = 1002] = "UserLogoutSuccess";
    EventId[EventId["AdminLoginFailure"] = 1003] = "AdminLoginFailure";
    EventId[EventId["InactiveUserTryToLogin"] = 1004] = "InactiveUserTryToLogin";
    EventId[EventId["UserLockoutByWrongPasswordAttempts"] = 1005] = "UserLockoutByWrongPasswordAttempts";
    EventId[EventId["SendingOtpToUser"] = 1007] = "SendingOtpToUser";
    EventId[EventId["MultiFactorAuthenticationNeeded"] = 1008] = "MultiFactorAuthenticationNeeded";
    EventId[EventId["MultiFactorAuthenticationSuccess"] = 1009] = "MultiFactorAuthenticationSuccess";
    EventId[EventId["MultiFactorAuthenticationFailed"] = 1110] = "MultiFactorAuthenticationFailed";
    EventId[EventId["UnhandledException"] = 3000] = "UnhandledException";
    EventId[EventId["AccessDenied"] = 3002] = "AccessDenied";
    EventId[EventId["BadRequest"] = 3003] = "BadRequest";
    EventId[EventId["ResourceNotFound"] = 3004] = "ResourceNotFound";
    EventId[EventId["MethodNotAllowed"] = 3005] = "MethodNotAllowed";
    EventId[EventId["UserPasswordChange"] = 10000] = "UserPasswordChange";
    EventId[EventId["UserPasswordReset"] = 10001] = "UserPasswordReset";
    EventId[EventId["UserLockoutByAdmin"] = 10002] = "UserLockoutByAdmin";
    EventId[EventId["UserUnlockByAdmin"] = 10003] = "UserUnlockByAdmin";
    EventId[EventId["UserPasswordResetAndSendOtpToUser"] = 10004] = "UserPasswordResetAndSendOtpToUser";
})(EventId || (EventId = {}));
var EventIdTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Start', value: 0, color: '' },
    { title: 'Processing', value: 1, color: '' },
    { title: 'End', value: 2, color: '' },
    { title: 'EntityCreate', value: 50, color: '' },
    { title: 'EntityUpdate', value: 51, color: '' },
    { title: 'EntityDelete', value: 52, color: '' },
    { title: 'EntityDeleteList', value: 53, color: '' },
    { title: 'EntityRead', value: 54, color: '' },
    { title: 'EntityReadList', value: 55, color: '' },
    { title: 'UserLoginSuccess', value: 1000, color: '' },
    { title: 'UserLoginFailure', value: 1001, color: '' },
    { title: 'UserLogoutSuccess', value: 1002, color: '' },
    { title: 'AdminLoginFailure', value: 1003, color: '' },
    { title: 'InactiveUserTryToLogin', value: 1004, color: '' },
    { title: 'UserLockoutByWrongPasswordAttempts', value: 1005, color: '' },
    { title: 'SendingOtpToUser', value: 1007, color: '' },
    { title: 'MultiFactorAuthenticationNeeded', value: 1008, color: '' },
    { title: 'MultiFactorAuthenticationSuccess', value: 1009, color: '' },
    { title: 'MultiFactorAuthenticationFailed', value: 1110, color: '' },
    { title: 'UnhandledException', value: 3000, color: '' },
    { title: 'AccessDenied', value: 3002, color: '' },
    { title: 'BadRequest', value: 3003, color: '' },
    { title: 'ResourceNotFound', value: 3004, color: '' },
    { title: 'MethodNotAllowed', value: 3005, color: '' },
    { title: 'UserPasswordChange', value: 10000, color: '' },
    { title: 'UserPasswordReset', value: 10001, color: '' },
    { title: 'UserLockoutByAdmin', value: 10002, color: '' },
    { title: 'UserUnlockByAdmin', value: 10003, color: '' },
    { title: 'UserPasswordResetAndSendOtpToUser', value: 10004, color: '' }
];
var EventIdItemsTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Start', value: 0 },
    { title: 'Processing', value: 1 },
    { title: 'End', value: 2 },
    { title: 'EntityCreate', value: 50 },
    { title: 'EntityUpdate', value: 51 },
    { title: 'EntityDelete', value: 52 },
    { title: 'EntityDeleteList', value: 53 },
    { title: 'EntityRead', value: 54 },
    { title: 'EntityReadList', value: 55 },
    { title: 'UserLoginSuccess', value: 1000 },
    { title: 'UserLoginFailure', value: 1001 },
    { title: 'UserLogoutSuccess', value: 1002 },
    { title: 'AdminLoginFailure', value: 1003 },
    { title: 'InactiveUserTryToLogin', value: 1004 },
    { title: 'UserLockoutByWrongPasswordAttempts', value: 1005 },
    { title: 'SendingOtpToUser', value: 1007 },
    { title: 'MultiFactorAuthenticationNeeded', value: 1008 },
    { title: 'MultiFactorAuthenticationSuccess', value: 1009 },
    { title: 'MultiFactorAuthenticationFailed', value: 1110 },
    { title: 'UnhandledException', value: 3000 },
    { title: 'AccessDenied', value: 3002 },
    { title: 'BadRequest', value: 3003 },
    { title: 'ResourceNotFound', value: 3004 },
    { title: 'MethodNotAllowed', value: 3005 },
    { title: 'UserPasswordChange', value: 10000 },
    { title: 'UserPasswordReset', value: 10001 },
    { title: 'UserLockoutByAdmin', value: 10002 },
    { title: 'UserUnlockByAdmin', value: 10003 },
    { title: 'UserPasswordResetAndSendOtpToUser', value: 10004 }
];
angular.module('altairApp').constant('EventIdTitleValues', EventIdTitleValues);
angular.module('altairApp').filter('toEventIdString', function () {
    return function (value) {
        var results = EventIdTitleValues.filter(function (item) {
            return item.value == value;
        });
        return (results.length && results[0].title) || '{Not Selected}';
    };
});
function EventIdProjectToTitleValueList() {
    return EventIdTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
function EventIdProjectToItemsTitleValueList() {
    return EventIdItemsTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
//# sourceMappingURL=EventId.js.map