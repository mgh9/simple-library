var EventType;
(function (EventType) {
    EventType[EventType["Success"] = 0] = "Success";
    EventType[EventType["Failure"] = 1] = "Failure";
    EventType[EventType["Information"] = 2] = "Information";
    EventType[EventType["Error"] = 3] = "Error";
})(EventType || (EventType = {}));
var EventTypeTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Success', value: 0, color: '' },
    { title: 'Failure', value: 1, color: '' },
    { title: 'Information', value: 2, color: '' },
    { title: 'Error', value: 3, color: '' }
];
var EventTypeItemsTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Success', value: 0 },
    { title: 'Failure', value: 1 },
    { title: 'Information', value: 2 },
    { title: 'Error', value: 3 }
];
angular.module('altairApp').constant('EventTypeTitleValues', EventTypeTitleValues);
angular.module('altairApp').filter('toEventTypeString', function () {
    return function (value) {
        var results = EventTypeTitleValues.filter(function (item) {
            return item.value == value;
        });
        return (results.length && results[0].title) || '{Not Selected}';
    };
});
function EventTypeProjectToTitleValueList() {
    return EventTypeTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
function EventTypeProjectToItemsTitleValueList() {
    return EventTypeItemsTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
//# sourceMappingURL=EventType.js.map