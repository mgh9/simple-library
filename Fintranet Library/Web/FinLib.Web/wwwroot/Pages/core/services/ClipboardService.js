var ClipboardService = (function () {
    function ClipboardService() {
    }
    ClipboardService.prototype.Copy = function (data) {
        var copyElement = document.createElement("textarea");
        copyElement.style.position = 'fixed';
        copyElement.style.opacity = '0';
        copyElement.textContent = data;
        var body = document.getElementsByTagName('body')[0];
        body.appendChild(copyElement);
        copyElement.select();
        document.execCommand('copy');
        body.removeChild(copyElement);
    };
    ;
    return ClipboardService;
}());
angular
    .module('altairApp')
    .service('ClipboardService', ClipboardService);
//# sourceMappingURL=ClipboardService.js.map