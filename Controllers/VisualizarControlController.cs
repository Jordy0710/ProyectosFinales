using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using CrystalDecisions.CrystalReports.Engine;


using System.IO;

namespace Proyecto.Controllers
{
    public class VisualizarControlController : Controller
    {
        // GET: VisualizarControl
        public ActionResult Index()
        {

            ReportDocument rd = new ReportDocument();

            // la ruta de la carpeta  reportes   y el nonmbre del diseño del reportes cristal reportes

            rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "Reporte_Ocurrencia.rpt"));

            //rd.SetDataSource(_bl.ConsultarCategoria().ToList());    ***********

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Listado_Ocurrencia.pdf");


            }
            catch
            {






                throw;


            }

           
        }
    }
}