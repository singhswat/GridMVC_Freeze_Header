using System;
using System.Web;

namespace GridMvc.Exporting
{
    /// <summary>
    ///     Grid export settings takes from query string
    /// </summary>
    public class QueryStringExportSettings : IGridExportSettings
    {
        public const string DefaultExportQueryParameter = "export-to";
        public readonly HttpContext Context;
        private string _exportQueryParameterName;
        

        public QueryStringExportSettings()
            : this(HttpContext.Current)
        {
        }

        public QueryStringExportSettings(HttpContext context)
        {
            if (context == null)
                throw new ArgumentException("No http context here!");
            Context = context;
            ExportQueryParameterName = DefaultExportQueryParameter;
            
        }

        public string ExportQueryParameterName
        {
            get { return _exportQueryParameterName; }
            set
            {
                _exportQueryParameterName = value;
                RefreshColumn();
            }
        }


        #region IGridExportSettings Members

        public string ExportTo { get; set; }

        #endregion

        private void RefreshColumn()
        {
            string currentExportTo = Context.Request.QueryString[ExportQueryParameterName] ?? string.Empty;
            ExportTo = currentExportTo;
        }
       
        
    }
}