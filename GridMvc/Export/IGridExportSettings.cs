namespace GridMvc.Exporting
{
    public interface IGridExportSettings
    {
        /// <summary>
        ///     Export type
        /// </summary>
        string ExportTo { get; set; }
        
    }
}