var ApplicationRole;
(function (ApplicationRole) {
    ApplicationRole[ApplicationRole["Admin"] = 1] = "Admin";
    ApplicationRole[ApplicationRole["Librarian"] = 2] = "Librarian";
    ApplicationRole[ApplicationRole["Customer"] = 3] = "Customer";
})(ApplicationRole || (ApplicationRole = {}));
var ApplicationRoleTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Admin', value: 1, color: '' },
    { title: 'Librarian', value: 2, color: '' },
    { title: 'Customer', value: 3, color: '' }
];
var ApplicationRoleItemsTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Admin', value: 1 },
    { title: 'Librarian', value: 2 },
    { title: 'Customer', value: 3 }
];
angular.module('altairApp').constant('ApplicationRoleTitleValues', ApplicationRoleTitleValues);
angular.module('altairApp').filter('toApplicationRoleString', function () {
    return function (value) {
        var results = ApplicationRoleTitleValues.filter(function (item) {
            return item.value == value;
        });
        return (results.length && results[0].title) || '{Not Selected}';
    };
});
function ApplicationRoleProjectToTitleValueList() {
    return ApplicationRoleTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
function ApplicationRoleProjectToItemsTitleValueList() {
    return ApplicationRoleItemsTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
//# sourceMappingURL=ApplicationRole.js.map