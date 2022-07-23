namespace FinLib.Pages.SEC {
    export class UserService extends UpdatableEntityService<IUserDto, IUserView, IUserSearchFilter> {
        public static $inject = ['$http'];

        constructor($http: angular.IHttpService) {
            super($http, 'User');
        }

        public getUsersTitleValueList(text?: string, includeEmptySelector?: boolean, includeSelectAllSelector?: boolean) {
            var self = this,
                $http = self.$http;

            return $http.get<IJsonResult<ITitleValue<number>[]>>(`${self.webAPIController}/GetTitleValueList`, {
                params: (includeEmptySelector ? { includeEmptySelector: includeEmptySelector } : null)
            });
        }

        public getCustomersUserRoleIdsTitleValueList(): IJsonPromis<ITitleValue<number>[]> {
            var self = this,
                $http = self.$http;

            return $http.get<IJsonResult<ITitleValue<number>[]>>(`${self.webAPIController}/GetCustomersUserRoleIdsTitleValueList`, {
            });
        }

        public removeUserRole(userRoleId: number): IJsonWithoutDataPromis {
            var self = this,
                $http = self.$http;

            return $http.delete(`${self.webAPIController}/DeleteUserRole`, { params: { userRoleId: userRoleId } });
        }

        public getPasswordPolicy(): IJsonPromis<IPasswordPolicy> {
            var self = this,
                $http = self.$http;

            return $http.get(`${self.webAPIController}/GetPasswordPolicy`);
        }

        public resetPassword(model: IResetPasswordDto): IJsonWithoutDataPromis {
            var self = this,
                $http = self.$http;

            return $http.post(`${self.webAPIController}/ResetPassword`, model);
        }

        public saveUserRoles(model: IUserDto): IJsonWithoutDataPromis {
            var self = this,
                $http = self.$http;

            return $http.put(`${self.webAPIController}/SaveUserRoles`, model);
        }
    }

    angular
        .module('altairApp')
        .service('UserService', UserService);
}