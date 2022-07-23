var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var FinLib;
(function (FinLib) {
    var Pages;
    (function (Pages) {
        var CNT;
        (function (CNT) {
            var MenuLinkService = (function (_super) {
                __extends(MenuLinkService, _super);
                function MenuLinkService($http) {
                    return _super.call(this, $http, 'MenuLink') || this;
                }
                MenuLinkService.$inject = ['$http'];
                return MenuLinkService;
            }(GeneralEntityService));
            CNT.MenuLinkService = MenuLinkService;
            angular
                .module('altairApp')
                .service('MenuLinkService', MenuLinkService);
        })(CNT = Pages.CNT || (Pages.CNT = {}));
    })(Pages = FinLib.Pages || (FinLib.Pages = {}));
})(FinLib || (FinLib = {}));
//# sourceMappingURL=menuLinkService.js.map