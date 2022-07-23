class BaseController {
    constructor(
        public $root: IRootScope,
        public $scope: IBaseScope<BaseController>) {
        var self = $scope.self = this;
    }
}
