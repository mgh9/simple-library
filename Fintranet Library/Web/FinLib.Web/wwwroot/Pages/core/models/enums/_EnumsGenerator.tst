$Enums(e=> IsMappableEnum(e))[
enum $Name {
    $Values[
    $Name = $Value][,]

}]

$Enums(e=> IsMappableEnum(e))[var $GetEnumNameAndTitleValue = [ 
    { title: '{Empty}', value: -1},
    $Values[
    { title: '$GetEnumNameDescription', value: $Value, color: '$GetEnumNameColor'}][,]

];]

$Enums(e=> IsMappableEnum(e))[var $GetEnumNameAndItemsTitleValues = [ 
    { title: '{Empty}', value: -1},
    $Values[
    { title: '$GetEnumItemName', value: $Value }][,]

];]

$Enums(e=> IsMappableEnum(e))[angular.module('altairApp').constant('$GetEnumNameAndTitleValue', $GetEnumNameAndTitleValue);]

$Enums(e=> IsMappableEnum(e))[angular.module('altairApp').filter('$GetEnumFilterName', function () {
    return function (value) {
        var results = $GetEnumNameAndTitleValue.filter(function (item) {
            return item.value == value;
        });

        return (results.length && results[0].title) || '{Not Selected}';
    };
});]

$Enums(e=> IsMappableEnum(e))[
function $GetProjectToTitleValue(){
    return <ITitleValue<number>[]>$GetEnumNameAndTitleValue.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}

function $GetProjectToItemsTitleValue(){
    return <ITitleValue<number>[]>$GetEnumNameAndItemsTitleValues.sort(function (a, b) { return (a.title > b.title) ? 1 : ((b.title > a.title) ? -1 : 0); });
}
]

${
    bool IsMappableEnum(Enum @enum)
    {
        if(!@enum.Namespace.StartsWith("FinLib.Models.Enums"))
            return false;

        if(@enum.Attributes.Any(x=> x.Name == "IgnoreTypewriterMapping"))
        {
            return false;
        }

        return true;
    }

    string GetProjectToTitleValue(Enum @enum) {    
        return $"{@enum.Name}ProjectToTitleValueList";
    }

    string GetProjectToItemsTitleValue(Enum @enum) {    
        return $"{@enum.Name}ProjectToItemsTitleValueList";
    }

    string GetEnumFilterName(Enum @enum) {    
        return $"to{@enum.Name}String";
    }

    string GetEnumNameAndTitleValue(Enum @enum) {
        return $"{@enum.Name}TitleValues";
    }

    string GetEnumNameAndItemsTitleValues(Enum @enum) {
        return $"{@enum.Name}ItemsTitleValues";
    }

    string GetEnumItemName(EnumValue pEnumValue)
    {
        return pEnumValue.Name;
    }

    string GetEnumNameDescription(EnumValue pEnumValue)
    {
        var itsDescriptionAttribute = pEnumValue.Attributes.FirstOrDefault();
        if(itsDescriptionAttribute == null)
            return pEnumValue;

        var itsDescriptionAttributeValue = itsDescriptionAttribute.Value;
        return itsDescriptionAttributeValue.Split(',')[0].Replace("\"",string.Empty);
    }

    string GetEnumNameColor(EnumValue pEnumValue)
    {
        var itsDescriptionAttribute = pEnumValue.Attributes.FirstOrDefault();
        if(itsDescriptionAttribute == null)
            return "";

        var itsDescriptionAttributeValue = itsDescriptionAttribute.Value;
        var splittedValue = itsDescriptionAttributeValue.Split(',');
        var retval = splittedValue.Length>1 ? splittedValue[1].Replace("\"",string.Empty):string.Empty;
        return retval.Trim();
    }
}