interface ISearchFilterItem<TRow> {
    columnName: string
    value: TRow,
    value2: TRow,
    valueType: ValueType,
    conditionType: ConditionType,
    isIgnore: boolean;
}


