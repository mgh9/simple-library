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

        if(@class.Name == "BaseView")
        {
            return retval;
        }

        var baseClass = @class.BaseClass ?? "BaseView";
        if(@class.BaseClass.IsGeneric)
        {
            var itsTypeArguments = string.Join("," ,@class.TypeArguments.Select(x=> x.Name).ToArray());
            baseClass += $"<{itsTypeArguments}>";
        }

        if(@class.IsGeneric)
        {
            var itsTypeArguments =string.Join(",", @class.TypeArguments.Select(x=> x.FullName).ToArray());
            retval += $"<{itsTypeArguments}>";
        }

        retval += $" extends I{baseClass}";

        return retval;
    }
}
$Classes(*View)[
interface I$GetName {
    $Properties[$TypeMap
    ]
}]
