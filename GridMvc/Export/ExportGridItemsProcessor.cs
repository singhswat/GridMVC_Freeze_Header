using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Web;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using ExporterObjects;


namespace GridMvc.Exporting
{
    /// <summary>
    ///     Grid items filter proprocessor
    /// </summary>
    internal class ExportGridItemsProcessor<T> : IGridItemsProcessor<T> where T : class
    {
        private readonly IGrid _grid;
        private IGridExportSettings _settings;
        public string DownloadUrl = "";

        public ExportGridItemsProcessor(IGrid grid, IGridExportSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            _grid = grid;
            _settings = settings;
        }

        public void UpdateSettings(IGridExportSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            _settings = settings;
        }

        #region IGridItemsProcessor<T> Members

        public IQueryable<T> Process(IQueryable<T> items)
        {
            string exportTo = _settings.ExportTo;
            ExportToFormat exportType = ExportToFormat.PDFtextSharpXML;

            switch (exportTo.ToLower())
            {
                case "pdf":
                {
                    exportType = ExportToFormat.PDFtextSharpXML;
                    break;
                }
                case "excel":
                {
                    exportType = ExportToFormat.Excel2007;
                    break;
                }
                case "word":
                {
                    exportType = ExportToFormat.Word2007;
                    break;
                }
                case "html":
                {
                    exportType = ExportToFormat.HTML;
                    break;
                }

            }

            var exp = new ExportList<T>();
            exp.PathTemplateFolder = HttpContext.Current.Server.MapPath("~/content/module/templates");
            DownloadUrl = "~/content/module/exports/document" + ExportBase.GetFileExtension(exportType);
            var filePathExport = HttpContext.Current.Server.MapPath(DownloadUrl);
            exp.ExportTo(items, exportType, filePathExport);
            return Enumerable.Empty<T>().AsQueryable(); //Clear Items
        }

        #endregion
    }
}