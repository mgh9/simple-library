var DatabaseName;
(function (DatabaseName) {
    DatabaseName[DatabaseName["Main"] = 0] = "Main";
    DatabaseName[DatabaseName["Auditing"] = 1] = "Auditing";
})(DatabaseName || (DatabaseName = {}));
var DatabaseNameTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Main', value: 0, color: '' },
    { title: 'Auditing', value: 1, color: '' }
];
var DatabaseNameItemsTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Main', value: 0 },
    { title: 'Auditing', value: 1 }
];
angular.module('altairApp').constant('DatabaseNameTitleValues', DatabaseNameTitleValues);
angular.module('altairApp').filter('toDatabaseNameString', function () {
    return function (value) {
        var results = DatabaseNameTitleValues.filter(function (item) {
            return item.value == value;
        });
        return (results.length && results[0].title) || '{Not Selected}';
    };
});
function DatabaseNameProjectToTitleValueList() {
    return DatabaseNameTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
function DatabaseNameProjectToItemsTitleValueList() {
    return DatabaseNameItemsTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
//# sourceMappingURL=DatabaseName.js.map