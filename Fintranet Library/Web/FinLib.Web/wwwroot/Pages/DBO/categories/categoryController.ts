namespace FinLib.Pages.DBO {
    export class CategoryController extends GeneralEntityController<ICategoryDto, ICategoryView, IGeneralEntitySearchFilter> {
        public static $inject = ['$rootScope', '$scope', '$http', 'CategoryService'];

        constructor(public $root: IRootScope,
            public $scope: ICategoryScope,
            public $http: angular.IHttpService,
            public categoryService: CategoryService) {

            super($root, $scope, categoryService);

            this.init();
        }

        init() {
            super.init();

            var self = this,
                $scope = self.$scope,
                $root = self.$root;

            $scope.$on('editDialogShown', function () {
            });
        }

        clear() {
            var self = this,
                $scope = self.$scope;

            super.clear();
        }
    }

    angular
        .module('altairApp')
        .controller('CategoryController', CategoryController);
}