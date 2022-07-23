interface ITableData<TRow> {
    columns: IColumn[];
    rows: TRow[];
    //pageSize: number;
    //pageIndex: number;
    //pageOrder: string;
    //request: IGetRequestJson<TSearchFilter>;
    count: number;
}
