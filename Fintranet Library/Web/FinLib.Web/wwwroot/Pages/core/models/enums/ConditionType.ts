﻿
enum ConditionType {
    
    Equals = 0,
    Contains = 1,
    NumericContains = 2,
    Between = 3,
    LessThan = 4,
    GreatherThan = 5

}

var ConditionTypeTitleValues = [ 
    { title: '{Empty}', value: -1},
    
    { title: 'Equals', value: 0, color: ''},
    { title: 'Contains', value: 1, color: ''},
    { title: 'NumericContains', value: 2, color: ''},
    { title: 'Between', value: 3, color: ''},
    { title: 'LessThan', value: 4, color: ''},
    { title: 'GreatherThan', value: 5, color: ''}

];

var ConditionTypeItemsTitleValues = [ 
    { title: '{Empty}', value: -1},
    
    { title: 'Equals', value: 0 },
    { title: 'Contains', value: 1 },
    { title: 'NumericContains', value: 2 },
    { title: 'Between', value: 3 },
    { title: 'LessThan', value: 4 },
    { title: 'GreatherThan', value: 5 }

];

angular.module('altairApp').constant('ConditionTypeTitleValues', ConditionTypeTitleValues);

angular.module('altairApp').filter('toConditionTypeString', function () {
    return function (value) {
        var results = ConditionTypeTitleValues.filter(function (item) {
            return item.value == value;
        });

        return (results.length && results[0].title) || '{Not Selected}';
    };
});


function ConditionTypeProjectToTitleValueList(){
    return <ITitleValue<number>[]>ConditionTypeTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}

function ConditionTypeProjectToItemsTitleValueList(){
    return <ITitleValue<number>[]>ConditionTypeItemsTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}


