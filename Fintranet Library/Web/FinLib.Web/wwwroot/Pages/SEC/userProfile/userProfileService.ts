namespace FinLib.Pages.SEC {
    export class UserProfileService extends BaseService {
        public static $inject = ['$http'];

        constructor(public $http: angular.IHttpService) {
            super();
        }

        public get(): IJsonWithConfigPromis<IUserProfileDto, IUserProfileConfigDto> {
            var self = this,
                $http = self.$http;

            return $http.get(`${self.apiPath}/UserProfile/Get`);
        }

        public saveData(data: IUserProfileDto): IJsonWithoutDataPromis {
            var self = this,
                $http = self.$http;

            return $http.put(`${self.apiPath}/UserProfile/Update`, data);
        }

        public changePassword(model: IChangePasswordDto): IJsonWithoutDataPromis {
            var self = this,
                $http = self.$http;

            return $http.put(`${self.apiPath}/UserProfile/ChangePassword`, model);
        }
    }

    angular
        .module('altairApp')
        .service('UserProfileService', UserProfileService);
}