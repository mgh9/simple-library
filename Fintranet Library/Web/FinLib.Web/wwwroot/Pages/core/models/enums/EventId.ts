
enum EventId {
    
    Start = 0,
    Processing = 1,
    End = 2,
    EntityCreate = 50,
    EntityUpdate = 51,
    EntityDelete = 52,
    EntityDeleteList = 53,
    EntityRead = 54,
    EntityReadList = 55,
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
    UnhandledException = 3000,
    AccessDenied = 3002,
    BadRequest = 3003,
    ResourceNotFound = 3004,
    MethodNotAllowed = 3005,
    UserPasswordChange = 10000,
    UserPasswordReset = 10001,
    UserLockoutByAdmin = 10002,
    UserUnlockByAdmin = 10003,
    UserPasswordResetAndSendOtpToUser = 10004

}

var EventIdTitleValues = [ 
    { title: '{Empty}', value: -1},
    
    { title: 'Start', value: 0, color: ''},
    { title: 'Processing', value: 1, color: ''},
    { title: 'End', value: 2, color: ''},
    { title: 'EntityCreate', value: 50, color: ''},
    { title: 'EntityUpdate', value: 51, color: ''},
    { title: 'EntityDelete', value: 52, color: ''},
    { title: 'EntityDeleteList', value: 53, color: ''},
    { title: 'EntityRead', value: 54, color: ''},
    { title: 'EntityReadList', value: 55, color: ''},
    { title: 'UserLoginSuccess', value: 1000, color: ''},
    { title: 'UserLoginFailure', value: 1001, color: ''},
    { title: 'UserLogoutSuccess', value: 1002, color: ''},
    { title: 'AdminLoginFailure', value: 1003, color: ''},
    { title: 'InactiveUserTryToLogin', value: 1004, color: ''},
    { title: 'UserLockoutByWrongPasswordAttempts', value: 1005, color: ''},
    { title: 'SendingOtpToUser', value: 1007, color: ''},
    { title: 'MultiFactorAuthenticationNeeded', value: 1008, color: ''},
    { title: 'MultiFactorAuthenticationSuccess', value: 1009, color: ''},
    { title: 'MultiFactorAuthenticationFailed', value: 1110, color: ''},
    { title: 'UnhandledException', value: 3000, color: ''},
    { title: 'AccessDenied', value: 3002, color: ''},
    { title: 'BadRequest', value: 3003, color: ''},
    { title: 'ResourceNotFound', value: 3004, color: ''},
    { title: 'MethodNotAllowed', value: 3005, color: ''},
    { title: 'UserPasswordChange', value: 10000, color: ''},
    { title: 'UserPasswordReset', value: 10001, color: ''},
    { title: 'UserLockoutByAdmin', value: 10002, color: ''},
    { title: 'UserUnlockByAdmin', value: 10003, color: ''},
    { title: 'UserPasswordResetAndSendOtpToUser', value: 10004, color: ''}

];

var EventIdItemsTitleValues = [ 
    { title: '{Empty}', value: -1},
    
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


function EventIdProjectToTitleValueList(){
    return <ITitleValue<number>[]>EventIdTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}

function EventIdProjectToItemsTitleValueList(){
    return <ITitleValue<number>[]>EventIdItemsTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}


