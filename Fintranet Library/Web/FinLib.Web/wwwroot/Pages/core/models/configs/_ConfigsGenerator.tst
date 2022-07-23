${
    string TypeMap(Property property)
    {
        var thePropertyType = property.Type;

        if (thePropertyType.IsPrimitive)
        {
            string typeName;

            if (thePropertyType.IsDate)
                typeName = "Date";
            else if (thePropertyType.IsEnum)
                typeName = thePropertyType.Name;
            else
                typeName = thePropertyType.name;

            return $"{property.name}: {typeName};";
        }
        else if(thePropertyType.Name.Contains("any[]"))
        {
            return $"{property.name}: any[];";
        }
        else if(thePropertyType.Name.Equals("T"))
        {
            return $"{property.name}: T;";
        }
        else if (thePropertyType.IsGeneric && thePropertyType.Name.Contains("AngucompleteJson"))
        {
            return $"{property.name}: IAngucompleteJson<ITitleValue<number>>;";
        }
        else if(thePropertyType.Name.StartsWith("T"))
        {
            return $"{property.name}: {thePropertyType.Name};";
        }
        else
        {
            return $"{property.name}: I{thePropertyType.Name};";
        }
    }

    string GetName(Class @class)
    {
        var retval = @class.Name;
        return retval;
    }

    bool IsMappableClass(Class @class)
    {
        if(!@class.Namespace.StartsWith("FinLib.Models.Configs"))
            return false;

        if(@class.Attributes.Any(x=> x.Name == "IgnoreTypewriterMapping"))
        {
            return false;
        }

        return true;
    }
}

$Classes(c=> IsMappableClass(c))[
interface I$GetName {
    $Properties[$TypeMap
    ]
}]
