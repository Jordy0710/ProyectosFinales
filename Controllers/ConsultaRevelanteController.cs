using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

using System.IO;

// PDF   reports IMPRIMIR 
using iTextSharp.text.pdf;
using iTextSharp.text;


//cristal reports IMPRIMIR
using CrystalDecisions.CrystalReports.Engine;

// EXCEL  reports  IMPRIMIR

using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Proyecto.Controllers
{
    public class ConsultaRevelanteController : Controller
    {
        // GET: ConsultaRevelante
        public ActionResult Index(RevelanteCLS oRevelanteCLS)
        {
            string nombrevigilante = oRevelanteCLS.descripcion;


            List<RevelanteCLS> listarevelante = null;

            using (var bd = new controlvisitanteEntities3())

            {


                if (oRevelanteCLS.descripcion == null)

                {



                    listarevelante = (from revelante in bd.Revelante

                                      join area in bd.Area on revelante.Id_Area equals area.Id_Area



                                      join vigilante in bd.Vigilante on revelante.Id_Vigilante equals vigilante.Id_Vigilante



                                      select new RevelanteCLS

                                      {


                                          idrevelante = revelante.Id_Revelante,
                                          descripcion = revelante.Descripcion,

                                          observacion = revelante.Observacion,

                                          nombrearea = area.Nombre,

                                          nombrevigilante = vigilante.Nombre,



                                          hora = revelante.Hora,
                                          fecha = revelante.Fecha,



                                      }).ToList();


                    Session["lista"] = listarevelante;
                }
                else

                {

                    listarevelante = (from revelante in bd.Revelante


                                      join area in bd.Area on revelante.Id_Area equals area.Id_Area



                                      join vigilante in bd.Vigilante on revelante.Id_Vigilante equals vigilante.Id_Vigilante








                                      where revelante.Descripcion.Contains(nombrevigilante)

                                      select new RevelanteCLS

                                      {







                                          idrevelante = revelante.Id_Revelante,
                                          descripcion = revelante.Descripcion,

                                          observacion = revelante.Observacion,

                                          nombrearea = area.Nombre,

                                          nombrevigilante = vigilante.Nombre,



                                          hora = revelante.Hora,
                                          fecha = revelante.Fecha,



                                      }).ToList();

                    Session["lista"] = listarevelante;

                }
            }
            return View(listarevelante);

        }




        public FileResult Excel_Exportar()

        {
            byte[] buffer;


            using (MemoryStream ms = new MemoryStream())


            {

                //  documento de excel archivo 

                ExcelPackage ep = new ExcelPackage();

                ep.Workbook.Worksheets.Add("Reporte de Revelante");

                //  hoja 

                ExcelWorksheet ew = ep.Workbook.Worksheets[1];

                // nombre  las columnas  

                ew.Cells[1, 1].Value = "Descripcion";
                ew.Cells[1, 2].Value = "Observacion";

                ew.Cells[1, 3].Value = "Area";

                ew.Cells[1, 4].Value = "Vigilante";
                ew.Cells[1, 5].Value = "Hora";
                ew.Cells[1, 6].Value = "Fecha";
              




                // tamaño  columna
                ew.Column(1).Width = 20;
                ew.Column(2).Width = 20;
                ew.Column(3).Width = 20;

                ew.Column(4).Width = 30;
                ew.Column(5).Width = 30;
                ew.Column(6).Width = 20;

     


                // 1 1 1  total 3 columna
                using (var range = ew.Cells[1, 1, 1, 6])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;

                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkRed);



                }
                List<RevelanteCLS> lista = (List<RevelanteCLS>)Session["lista"];

                int fila = lista.Count;

                for (int i = 0; i < fila; i++)
                {
                    ew.Cells[i + 2, 1].Value = lista[i].descripcion;
                    ew.Cells[i + 2, 2].Value = lista[i].observacion;
                    ew.Cells[i + 2, 3].Value = lista[i].nombrearea;

                    ew.Cells[i + 2, 4].Value = lista[i].nombrevigilante;
           
                    ew.Cells[i + 2, 5].Value = lista[i].hora;

                    ew.Cells[i + 2, 6].Value = lista[i].fecha;

                }

                ep.SaveAs(ms);

                buffer = ms.ToArray();

            }

            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public FileResult generarPDF()
        {


            Document doc = new Document();

            byte[] buffer;


            using (MemoryStream ms = new MemoryStream())


            {
                PdfWriter.GetInstance(doc, ms);


                doc.Open();

                // titulo  de la hoja

                Paragraph title = new Paragraph("Listado de Revelante");

                // columnas de tabla 
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                // espacio vacio
                Paragraph espacio = new Paragraph(" ");
                doc.Add(espacio);



                //ancho de la columna  7  columna mostrara   menos   id control
                PdfPTable table = new PdfPTable(6);

                float[] values = new float[6] { 60, 60, 50, 50, 40, 67 };
                table.SetTotalWidth(values);

                //  celdas de la tabla filas de registro  y color y alineacion de fila

                PdfPCell cel1 = new PdfPCell(new Phrase("Descripcion"));

                cel1.BackgroundColor = new BaseColor(100, 100, 100);
                cel1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(cel1);

                PdfPCell cel2 = new PdfPCell(new Phrase("Observacion"));

                cel2.BackgroundColor = new BaseColor(100, 100, 100);
                cel2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(cel2);

                PdfPCell cel3 = new PdfPCell(new Phrase("Area"));

                cel3.BackgroundColor = new BaseColor(100, 100, 100);
                cel3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(cel3);


                PdfPCell cel4 = new PdfPCell(new Phrase("Vigilante"));

                cel4.BackgroundColor = new BaseColor(100, 100, 100);
                cel4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(cel4);


                PdfPCell cel5 = new PdfPCell(new Phrase("Hora"));

                cel5.BackgroundColor = new BaseColor(100, 100, 100);
                cel5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(cel5);


                PdfPCell cel6 = new PdfPCell(new Phrase("Fecha"));

                cel6.BackgroundColor = new BaseColor(100, 100, 100);
                cel6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(cel6);

              


                List<RevelanteCLS> lista = (List<RevelanteCLS>)Session["lista"];

                int fila = lista.Count;

                for (int i = 0; i < fila; i++)
                {

                    table.AddCell(lista[i].descripcion.ToString());
                    table.AddCell(lista[i].observacion.ToString());

                    table.AddCell(lista[i].nombrearea.ToString());
                    table.AddCell(lista[i].nombrevigilante.ToString());
                   

                    table.AddCell(lista[i].hora.ToString());
                    table.AddCell(lista[i].fecha.ToString());
                }

                doc.Add(table);

                doc.Close();


                buffer = ms.ToArray();


            }




            return File(buffer, "application/pdf");


        }



        public ActionResult cristal_reports()
        {
            ReportDocument rd = new ReportDocument();

            // la ruta de la carpeta  reportes   y el nonmbre del diseño del reportes cristal reportes

            rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "Reporte_Revelante.rpt"));

            //rd.SetDataSource(_bl.ConsultarCategoria().ToList());    ***********

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Listado_Revelante.pdf");


            }
            catch
            {






                throw;


            }


        }

    }
}