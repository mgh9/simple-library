
enum ValueType {
    
    Text = 0,
    Number = 1,
    DecimalNumber = 2,
    List = 3,
    Date = 4,
    Enum = 5,
    Boolean = 6

}

var ValueTypeTitleValues = [ 
    { title: '{Empty}', value: -1},
    
    { title: 'Text', value: 0, color: ''},
    { title: 'Number', value: 1, color: ''},
    { title: 'DecimalNumber', value: 2, color: ''},
    { title: 'List', value: 3, color: ''},
    { title: 'Date', value: 4, color: ''},
    { title: 'Enum', value: 5, color: ''},
    { title: 'Boolean', value: 6, color: ''}

];

var ValueTypeItemsTitleValues = [ 
    { title: '{Empty}', value: -1},
    
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


function ValueTypeProjectToTitleValueList(){
    return <ITitleValue<number>[]>ValueTypeTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}

function ValueTypeProjectToItemsTitleValueList(){
    return <ITitleValue<number>[]>ValueTypeItemsTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}


