class AccountService extends BaseService {
    public static $inject = ['$http'];

    constructor(public $http: angular.IHttpService) {
        super();
    }

    getLoggedInUserInfo() {
        var self = this,
            $http = self.$http;

        return $http.get<IJsonResult<IUserInfoDto>>(`${self.apiPath}/Account/GetLoggedInUserInfo`);
    }
}

angular
    .module('altairApp')
    .service('AccountService', AccountService);
