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
    public class ConsultaControlController : Controller
    {
  

        // GET: ConsultaControl
        public ActionResult Index(ControlCLS oControlCLS)
        {
            string nombrevigilante = oControlCLS.descripcion;


            List<ControlCLS> listacontrol = null;

            using (var bd = new controlvisitanteEntities3())

            {


                if (oControlCLS.descripcion == null)

                {



                    listacontrol = (from Control in bd.Control_Visitante_Ocurrencias

                                    join area in bd.Area on Control.Id_Area equals area.Id_Area



                                    join vigilante in bd.Vigilante on Control.Id_Vigilante equals vigilante.Id_Vigilante


                                    join visitante in bd.Visitante on Control.Id_Visitante equals visitante.Id_Visitante


                                    select new ControlCLS

                                    {


                                        idcontrol = Control.Id_Control,
                                        descripcion = Control.Descripcion,

                                        motivo = Control.Motivo,

                                        nombrearea = area.Nombre,

                                        nombrevigilante = vigilante.Nombre,

                                        nombrevisitante = visitante.nombre,


                                        hora = Control.Hora,
                                        fecha = Control.Fecha,



                                    }).ToList();


                    Session["lista"] = listacontrol;

                }
                else

                {

                    listacontrol = (from Control in bd.Control_Visitante_Ocurrencias


                                    join area in bd.Area on Control.Id_Area equals area.Id_Area



                                    join vigilante in bd.Vigilante on Control.Id_Vigilante equals vigilante.Id_Vigilante


                                    join visitante in bd.Visitante on Control.Id_Visitante equals visitante.Id_Visitante








                                    where Control.Descripcion.Contains(nombrevigilante)

                                    select new ControlCLS

                                    {







                                        idcontrol = Control.Id_Control,
                                        descripcion = Control.Descripcion,

                                        motivo = Control.Motivo,

                                        nombrearea = area.Nombre,

                                        nombrevigilante = vigilante.Nombre,

                                        nombrevisitante = visitante.nombre,


                                        hora = Control.Hora,
                                        fecha = Control.Fecha,



                                    }).ToList();
                    Session["lista"] = listacontrol;
                }
            }
            return View(listacontrol);
        }

        public FileResult Excel_Exportar()

        {
            byte[] buffer;


            using (MemoryStream ms = new MemoryStream())


            {

                //  documento de excel archivo 

                ExcelPackage ep = new ExcelPackage();

                ep.Workbook.Worksheets.Add("Reporte de Visitante");

                //  hoja 

                ExcelWorksheet ew = ep.Workbook.Worksheets[1];

                // nombre  las columnas  

                ew.Cells[1, 1].Value = "Dirige";
                ew.Cells[1, 2].Value = "Motivo";

                ew.Cells[1, 3].Value = "Area";

                ew.Cells[1, 4].Value = "Vigilante";
                ew.Cells[1, 5].Value = "Visitante";
                ew.Cells[1, 6].Value = "Hora";
                ew.Cells[1, 7].Value = "Fecha";




                // tamaño  columna
                ew.Column(1).Width = 20;
                ew.Column(2).Width = 20;
                ew.Column(3).Width = 20;

                ew.Column(4).Width = 30;
                ew.Column(5).Width = 30;
                ew.Column(6).Width = 20;

                ew.Column(7).Width = 20;
             

                // 1 1 1  total 3 columna
                using (var range = ew.Cells[1, 1, 1, 7])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;

                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkRed);



                }
                List<ControlCLS> lista = (List<ControlCLS>)Session["lista"];

                int fila = lista.Count;

                for (int i = 0; i < fila; i++)
                {
                    ew.Cells[i + 2, 1].Value = lista[i].descripcion;
                    ew.Cells[i + 2, 2].Value = lista[i].motivo;
                    ew.Cells[i + 2, 3].Value = lista[i].nombrearea;

                    ew.Cells[i + 2, 4].Value = lista[i].nombrevigilante;
                    ew.Cells[i + 2, 5].Value = lista[i].nombrevisitante;
                    ew.Cells[i + 2, 6].Value = lista[i].hora;

                    ew.Cells[i + 2, 7].Value = lista[i].fecha;

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

                Paragraph title = new Paragraph("Listado de Visitante");

                // columnas de tabla 
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                // espacio vacio
                Paragraph espacio = new Paragraph(" ");
                doc.Add(espacio);



                //ancho de la columna  7  columna mostrara   menos   id control
                PdfPTable table = new PdfPTable(7);

                float[] values = new float[7] { 60, 40, 70 , 50, 70, 50 , 70 };
                table.SetTotalWidth(values);

                //  celdas de la tabla filas de registro  y color y alineacion de fila

                PdfPCell cel1 = new PdfPCell(new Phrase("Dirige"));

                cel1.BackgroundColor = new BaseColor(100, 100, 100);
                cel1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(cel1);

                PdfPCell cel2 = new PdfPCell(new Phrase("Motivo"));

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


                PdfPCell cel5 = new PdfPCell(new Phrase("Visitante"));

                cel5.BackgroundColor = new BaseColor(100, 100, 100);
                cel5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(cel5);


                PdfPCell cel6 = new PdfPCell(new Phrase("Hora"));

                cel6.BackgroundColor = new BaseColor(100, 100, 100);
                cel6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(cel6);

                PdfPCell cel7 = new PdfPCell(new Phrase("Fecha"));

                cel7.BackgroundColor = new BaseColor(100, 100, 100);
                cel7.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(cel7);


                List<ControlCLS>lista = (List<ControlCLS>)Session["lista"] ;

                int fila = lista.Count;

                for(int i = 0; i < fila;i++)
                {

                    table.AddCell(lista[i].descripcion.ToString());
                    table.AddCell(lista[i].motivo.ToString());

                    table.AddCell(lista[i].nombrearea.ToString());
                    table.AddCell(lista[i].nombrevigilante.ToString());
                    table.AddCell(lista[i].nombrevisitante.ToString());

                    table.AddCell(lista[i].hora .ToString());
                    table.AddCell(lista[i].fecha.ToString());
                }

                doc.Add(table);

                doc.Close();


                buffer = ms.ToArray();


            }




            return File(buffer,"application/pdf");


        }


        public ActionResult cristal_reports()
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