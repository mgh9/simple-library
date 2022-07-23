﻿${
    string TypeMap(Property property)
    {
        var type = property.Type;

        if (type.IsPrimitive)
        {
            string typeName;

            if (type.IsDate)
                typeName = "Date";
            else if (type.IsEnum)
                typeName = type.Name;
            else
                typeName = type.name;

            return $"{property.name}: {typeName};";
        }
        else if(type.Name.Contains("any[]"))
        {
            return $"{property.name}: any[];";
        }
        else if(type.Name.Equals("T"))
        {
            return $"{property.name}: T;";
        }
        else if (type.IsGeneric && type.Name.Contains("AngucompleteJson"))
        {
            return $"{property.name}: IAngucompleteJson<ITitleValue<number>>;";
        }
        else
        {
            return $"{property.name}: I{type.Name};";
        }
    }

    string GetName(Class @class)
    {
        return $"{@class.Name}{(@class.IsGeneric ? "<T>" : String.Empty)}";
    }
}

${
    string GetBaseClassName(Class pCurrentClass)
    {
        if (pCurrentClass.BaseClass == null)
            return String.Empty;
        else if (pCurrentClass.BaseClass.Name == "BaseEntitySearchFilter")
            return "extends IBaseEntitySearchFilter";
        else if (pCurrentClass.BaseClass.Name == "UpdatableEntitySearchFilter")
            return "extends IUpdatableEntitySearchFilter";
        else if (pCurrentClass.BaseClass.Name == "GeneralEntitySearchFilter")
            return "extends IGeneralEntitySearchFilter";
        else
            return $"**error in ClassName ({pCurrentClass.BaseClass.Name})**";
    }
}

$Classes(*SearchFilter)[
interface I$GetName $GetBaseClassName
{

    $Properties[$TypeMap
    ]
}]