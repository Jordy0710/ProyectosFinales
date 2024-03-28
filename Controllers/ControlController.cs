using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
namespace Proyecto.Controllers
{
    public class ControlController : Controller
    {
        // GET: Control
        public ActionResult Index(ControlCLS oControlCLS)
        {
            string nombrevigilante = oControlCLS.descripcion;


            List<ControlCLS> listacontrol = null;

            using (var bd = new controlvisitanteEntities3())

            {


                if (oControlCLS.descripcion == null)

                {



                    listacontrol = (from Control in bd.Control_Visitante_Ocurrencias

                                      join area in bd.Area on  Control.Id_Area equals area.Id_Area



                                      join vigilante in bd.Vigilante on Control.Id_Vigilante equals vigilante.Id_Vigilante


                                    join visitante in bd.Visitante on Control.Id_Visitante equals visitante.Id_Visitante


                                    select new ControlCLS

                                      {


                                         idcontrol = Control.Id_Control,
                                         descripcion = Control.Descripcion,

                                         motivo= Control.Motivo,
                                         
                                          nombrearea = area.Nombre,

                                          nombrevigilante= vigilante.Nombre,

                                          nombrevisitante = visitante.nombre,


                                        hora = Control.Hora,
                                        fecha = Control.Fecha,



                                    }).ToList();

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

                }
            }
            return View(listacontrol);
        }



        private void llenararea()

        {
            List<SelectListItem> lista;

            using (var bd = new controlvisitanteEntities3())

            {

                lista = (from area in bd.Area

                         select new SelectListItem
                         {
                             // visible el nombre 
                             Text = area.Nombre,

                             // aqui va el id  
                             Value = area.Id_Area.ToString()

                         }).ToList();


                lista.Insert(0, new SelectListItem { Text = "--Seleccionar--", Value = "" });

                ViewBag.listaarea = lista;

            }

        }

        private void llenarvigilante()

        {
            List<SelectListItem> lista;

            using (var bd = new controlvisitanteEntities3())

            {

                lista = (from vigilante in bd.Vigilante

                         select new SelectListItem
                         {
                             // visible el nombre 
                             Text = vigilante.Nombre,

                             // aqui va el id  
                             Value = vigilante.Id_Vigilante.ToString()

                         }).ToList();


                lista.Insert(0, new SelectListItem { Text = "--Seleccionar--", Value = "" });

                ViewBag.listavigilante = lista;

            }


        }


        private void llenarvisitante()

        {
            List<SelectListItem> lista;

            using (var bd = new controlvisitanteEntities3())

            {

                lista = (from visitante in bd.Visitante

                         select new SelectListItem
                         {
                             // visible el nombre 
                             Text = visitante.nombre,

                             // aqui va el id  
                             Value = visitante.Id_Visitante.ToString()

                         }).ToList();


                lista.Insert(0, new SelectListItem { Text = "--Seleccionar--", Value = "" });


                ViewBag.listavisitante = lista;
            }


        }



        public ActionResult Agregar()
        {


            // metodo
            listarCombos();


            return View();
        }

        public void listarCombos()
        {
            llenararea();
            llenarvigilante();

            llenarvisitante();

        }

        // AQUI SOLO ENVIAR LOS DATOS  HACIA OTRO FORMULARIOS  ***************************************************

        public ActionResult Editar(int id)
        {
            //llama la clases 
            ControlCLS oControlCLS = new ControlCLS();




            using (var bd = new controlvisitanteEntities3())
            {


                listarCombos();




                Control_Visitante_Ocurrencias oControl = bd.Control_Visitante_Ocurrencias.Where(p => p.Id_Control.Equals(id)).First();

                oControlCLS.idcontrol = oControl.Id_Control;

                oControlCLS.descripcion = oControl.Descripcion;

                oControlCLS.motivo = oControl.Motivo;

                oControlCLS.idarea =  (int)oControl.Id_Area;



                oControlCLS.idvigilante = (int)oControl.Id_Vigilante;

                oControlCLS.idvisitante = (int)oControl.Id_Visitante;


                oControlCLS.hora = oControl.Hora;
                oControlCLS.fecha = oControl.Fecha;

            }


            return View(oControlCLS);



        }


        [HttpPost]
        public ActionResult Agregar(ControlCLS oControlCLS)
        {



            ////**************************************

            int registroEncontrado = 0;

            int repetido = oControlCLS.idcontrol;

            using (var bd = new controlvisitanteEntities3())

            {
                registroEncontrado = bd.Control_Visitante_Ocurrencias.Where(p => p.Id_Control.Equals(repetido)).Count();


            }






            if (!ModelState.IsValid || registroEncontrado >= 1)

            {




                if (registroEncontrado >= 1) oControlCLS.mensajeERROR = "Ya Exite ";


                listarCombos();


                return View(oControlCLS);

            }
            else

            {



                using (var bd = new controlvisitanteEntities3())

                {
                   Control_Visitante_Ocurrencias oControl = new Control_Visitante_Ocurrencias();

     


                    oControl.Descripcion = oControlCLS.descripcion;

                    oControl.Motivo = oControlCLS.motivo;

                    oControl.Id_Area = oControlCLS.idarea;



                    oControl.Id_Vigilante = oControlCLS.idvigilante;

                    oControl.Id_Visitante = oControlCLS.idvisitante;


                    oControl.Hora = oControlCLS.hora;
                    oControl.Fecha = oControlCLS.fecha;




                    bd.Control_Visitante_Ocurrencias.Add(oControl);

                    bd.SaveChanges();


                }

            }

            return RedirectToAction("Index");



        }


        [HttpPost]
        public ActionResult Editar(ControlCLS oControlCLS)

        {
            if (!ModelState.IsValid)

            {

                return View(oControlCLS);

            }

            int idProducto = oControlCLS.idcontrol;


            using (var bd = new controlvisitanteEntities3())
            {


               Control_Visitante_Ocurrencias oControl = bd.Control_Visitante_Ocurrencias.Where(p => p.Id_Control.Equals(idProducto)).First();



                oControl.Id_Control = oControlCLS.idcontrol;

                oControl.Descripcion = oControlCLS.descripcion;

                oControl.Motivo = oControlCLS.motivo;

                oControl.Id_Area = oControlCLS.idarea;



                oControl.Id_Vigilante = oControlCLS.idvigilante;

                oControl.Id_Visitante = oControlCLS.idvisitante;


                oControl.Hora= oControlCLS.hora;
                oControl.Fecha = oControlCLS.fecha;








                bd.SaveChanges();


            }


            return RedirectToAction("Index");



        }




       
        public ActionResult Eliminar(int id)

        {

            using (var bd = new controlvisitanteEntities3())
            {

                

                Control_Visitante_Ocurrencias oControl_Visitante_Ocurrencias = bd.Control_Visitante_Ocurrencias.Find(id);

                bd.Control_Visitante_Ocurrencias.Remove(oControl_Visitante_Ocurrencias);


                bd.SaveChanges();




            }


            return RedirectToAction("Index");

        }





    }





}
