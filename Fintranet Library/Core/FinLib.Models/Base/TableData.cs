namespace FinLib.Models.Base
{
    public class TableData<TEntity> 
        where TEntity : class, new()
    {
        public TableData(List<Column> columns, List<TEntity> rows, long count)
        {
            Columns = columns;
            Rows = rows;
            Count = count;
        }

        public List<Column> Columns { get; }
        public List<TEntity> Rows { get; }
        public long Count { get; }
    }
}
