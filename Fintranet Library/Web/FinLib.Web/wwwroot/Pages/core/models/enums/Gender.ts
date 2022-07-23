
enum Gender {
    
    Male = 0,
    Female = 1

}

var GenderTitleValues = [ 
    { title: '{Empty}', value: -1},
    
    { title: 'Male', value: 0, color: ''},
    { title: 'Female', value: 1, color: ''}

];

var GenderItemsTitleValues = [ 
    { title: '{Empty}', value: -1},
    
    { title: 'Male', value: 0 },
    { title: 'Female', value: 1 }

];

angular.module('altairApp').constant('GenderTitleValues', GenderTitleValues);

angular.module('altairApp').filter('toGenderString', function () {
    return function (value) {
        var results = GenderTitleValues.filter(function (item) {
            return item.value == value;
        });

        return (results.length && results[0].title) || '{Not Selected}';
    };
});


function GenderProjectToTitleValueList(){
    return <ITitleValue<number>[]>GenderTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}

function GenderProjectToItemsTitleValueList(){
    return <ITitleValue<number>[]>GenderItemsTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}


