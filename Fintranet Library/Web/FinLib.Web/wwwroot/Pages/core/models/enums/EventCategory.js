var EventCategory;
(function (EventCategory) {
    EventCategory[EventCategory["Application"] = 0] = "Application";
    EventCategory[EventCategory["Authentication"] = 1] = "Authentication";
    EventCategory[EventCategory["EntityManagement"] = 2] = "EntityManagement";
    EventCategory[EventCategory["UserAccountManagement"] = 3] = "UserAccountManagement";
})(EventCategory || (EventCategory = {}));
var EventCategoryTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Application', value: 0, color: '' },
    { title: 'Authentication', value: 1, color: '' },
    { title: 'EntityManagement', value: 2, color: '' },
    { title: 'UserAccountManagement', value: 3, color: '' }
];
var EventCategoryItemsTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Application', value: 0 },
    { title: 'Authentication', value: 1 },
    { title: 'EntityManagement', value: 2 },
    { title: 'UserAccountManagement', value: 3 }
];
angular.module('altairApp').constant('EventCategoryTitleValues', EventCategoryTitleValues);
angular.module('altairApp').filter('toEventCategoryString', function () {
    return function (value) {
        var results = EventCategoryTitleValues.filter(function (item) {
            return item.value == value;
        });
        return (results.length && results[0].title) || '{Not Selected}';
    };
});
function EventCategoryProjectToTitleValueList() {
    return EventCategoryTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
function EventCategoryProjectToItemsTitleValueList() {
    return EventCategoryItemsTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
//# sourceMappingURL=EventCategory.js.map