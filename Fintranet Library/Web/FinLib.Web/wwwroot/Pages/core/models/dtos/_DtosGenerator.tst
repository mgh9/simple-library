${
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
        else if(type.Name.StartsWith("TS"))
        {
            return $"{property.name}: {type.Name};";
        }
        else if(type.Name.StartsWith("TV"))
        {
            return $"{property.name}: {type.Name};";
        }

        else if (type.IsGeneric && property.Name == "Attendees")
        {
            return $"{property.name}: IAngucompleteDto<ITitleValue<IMeetingUserAutocompleteDto>>[];";
        }
        else if (type.IsGeneric && type.Name.Contains("AngucompleteDto"))
        {
            return $"{property.name}: IAngucompleteJson<ITitleValue<number>>;";
        }
        else if (type.IsGeneric && type.Name.Contains("TitleValue"))
        {
            return $"{property.name}: I{type.Name};"; 
        }
        else if(type.Name.ToUpper().Equals("ANY"))
        {
            return $"{property.name}: any;";
        }
        else
        {
            return $"{property.name}: I{type.Name};";
        }
    }

    string GetName(Class @class)
    {
       //return $"{@class.Name}{(@class.IsGeneric ? "<T>" : String.Empty)}";
        var retval = "";//@class.Name;

        if(@class.Name == "BaseEntityDto")
        {
            return retval;
        }

        var baseClass = @class.BaseClass ?? "BaseEntityDto";
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

${
    string GetBaseClassName(Class currentClass)
    {      
        if(currentClass.Name == "BaseDto")
            return "extends IBaseDtoExtended";        
        else if(currentClass.BaseClass == null)
            return String.Empty;

        var itsBaseClass = currentClass.BaseClass;
        
        if (itsBaseClass.Name == null)
            return "***Error in Interface Inheritanceeeee***";
        else if(currentClass.Name.Contains ("TwoValuesMod"))
            return "<TValue1, TValue2> extends IBaseDto";
        else if(itsBaseClass.Name == "UserClaimDto")
            return "extends IUserClaimDto";

        else if (itsBaseClass.Name == "GetRequestDto")
            return GetName(currentClass);
        else if (currentClass.Name == "GetRequestDto")
            return "<TSearchFilter> extends IBaseDto";
        else if (currentClass.Name == "GetResultDto")
            return "<TView> extends IBaseDto";
        else if (currentClass.Name == "AngucompleteDto")
            return "<T> extends IBaseDto";

        else if (itsBaseClass.Name == "GeneralDto")
            return "extends IGeneralDto";
        else if (itsBaseClass.Name == "UpdatableDto")
            return "extends IUpdatableDto";
        else if (itsBaseClass.Name == "BaseEntityDto")
            return "extends IBaseEntityDto";
        else if(itsBaseClass.Name == "BaseConfigDto")
            return "extends IBaseDto";
        else if(itsBaseClass.Name == "BaseProviderDto")
            return "extends IBaseProviderDto";
        else if (itsBaseClass.Name == "BaseDto")
            return "extends IBaseDto";
        else
            return "**error in ClassName**";
    }
}

$Classes(*Dto)[
interface I$Name $GetBaseClassName 
{   $Properties[
    $TypeMap]
}]