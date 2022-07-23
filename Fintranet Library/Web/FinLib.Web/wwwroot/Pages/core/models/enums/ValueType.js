var ValueType;
(function (ValueType) {
    ValueType[ValueType["Text"] = 0] = "Text";
    ValueType[ValueType["Number"] = 1] = "Number";
    ValueType[ValueType["DecimalNumber"] = 2] = "DecimalNumber";
    ValueType[ValueType["List"] = 3] = "List";
    ValueType[ValueType["Date"] = 4] = "Date";
    ValueType[ValueType["Enum"] = 5] = "Enum";
    ValueType[ValueType["Boolean"] = 6] = "Boolean";
})(ValueType || (ValueType = {}));
var ValueTypeTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Text', value: 0, color: '' },
    { title: 'Number', value: 1, color: '' },
    { title: 'DecimalNumber', value: 2, color: '' },
    { title: 'List', value: 3, color: '' },
    { title: 'Date', value: 4, color: '' },
    { title: 'Enum', value: 5, color: '' },
    { title: 'Boolean', value: 6, color: '' }
];
var ValueTypeItemsTitleValues = [
    { title: '{Empty}', value: -1 },
    { title: 'Text', value: 0 },
    { title: 'Number', value: 1 },
    { title: 'DecimalNumber', value: 2 },
    { title: 'List', value: 3 },
    { title: 'Date', value: 4 },
    { title: 'Enum', value: 5 },
    { title: 'Boolean', value: 6 }
];
angular.module('altairApp').constant('ValueTypeTitleValues', ValueTypeTitleValues);
angular.module('altairApp').filter('toValueTypeString', function () {
    return function (value) {
        var results = ValueTypeTitleValues.filter(function (item) {
            return item.value == value;
        });
        return (results.length && results[0].title) || '{Not Selected}';
    };
});
function ValueTypeProjectToTitleValueList() {
    return ValueTypeTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
function ValueTypeProjectToItemsTitleValueList() {
    return ValueTypeItemsTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
//# sourceMappingURL=ValueType.js.map