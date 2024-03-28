using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
namespace Proyecto.Controllers
{
    public class RevelanteController : Controller
    {
        // GET: Revelante
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


                                        idrevelante  = revelante.Id_Revelante,
                                        descripcion =  revelante.Descripcion,

                                        observacion = revelante.Observacion,

                                        nombrearea = area.Nombre,

                                        nombrevigilante = vigilante.Nombre,

                                      

                                        hora = revelante.Hora,
                                        fecha = revelante.Fecha,



                                    }).ToList();

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

                }
            }
            return View(listarevelante);

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

        

        }


        // AQUI SOLO ENVIAR LOS DATOS  HACIA OTRO FORMULARIOS  ***************************************************

        public ActionResult Editar(int id)
        {
            //llama la clases 
            RevelanteCLS oRevelanteCLS = new RevelanteCLS();




            using (var bd = new controlvisitanteEntities3())
            {


                listarCombos();




                Revelante oRevelante = bd.Revelante.Where(p => p.Id_Revelante.Equals(id)).First();

                oRevelanteCLS.idrevelante = oRevelante.Id_Revelante;

                oRevelanteCLS.descripcion = oRevelante.Descripcion;

                oRevelanteCLS.observacion = oRevelante.Observacion;

                oRevelanteCLS.idarea = (int)oRevelante.Id_Area;



                oRevelanteCLS.idvigilante = (int)oRevelante.Id_Vigilante;




                oRevelanteCLS.hora = oRevelante.Hora;
                oRevelanteCLS.fecha = oRevelante.Fecha;

            }


            return View(oRevelanteCLS);

        }






        [HttpPost]
        public ActionResult Agregar(RevelanteCLS oRevelanteCLS)
        {



            ////**************************************

            int registroEncontrado = 0;

            int repetido = oRevelanteCLS.idrevelante;

            using (var bd = new controlvisitanteEntities3())

            {
                registroEncontrado = bd.Revelante.Where(p => p.Id_Revelante.Equals(repetido)).Count();


            }






            if (!ModelState.IsValid || registroEncontrado >= 1)

            {




                if (registroEncontrado >= 1) oRevelanteCLS.mensajeERROR = "Ya Exite ";


                listarCombos();


                return View(oRevelanteCLS);

            }
            else

            {



                using (var bd = new controlvisitanteEntities3())

                {
                    Revelante oRevelante = new Revelante();




                    oRevelante.Descripcion = oRevelanteCLS.descripcion;

                    oRevelante.Observacion= oRevelanteCLS.observacion;

                    oRevelante.Id_Area = oRevelanteCLS.idarea;



                    oRevelante.Id_Vigilante = oRevelanteCLS.idvigilante;



                    oRevelante.Hora = oRevelanteCLS.hora;
                    oRevelante.Fecha = oRevelanteCLS.fecha;




                    bd.Revelante.Add(oRevelante);

                    bd.SaveChanges();


                }

            }

            return RedirectToAction("Index");



        }


        [HttpPost]
        public ActionResult Editar(RevelanteCLS oRevelanteCLS)

        {
            if (!ModelState.IsValid)

            {

                return View(oRevelanteCLS);

            }


            int idProducto = oRevelanteCLS.idrevelante;






            using (var bd = new controlvisitanteEntities3())
            {


                Revelante oRevelante = bd.Revelante.Where(p => p.Id_Revelante.Equals(idProducto)).First();





                oRevelante.Descripcion = oRevelanteCLS.descripcion;

                oRevelante.Observacion = oRevelanteCLS.observacion;

                oRevelante.Id_Area = oRevelanteCLS.idarea;



                oRevelante.Id_Vigilante = oRevelanteCLS.idvigilante;



                oRevelante.Hora = oRevelanteCLS.hora;
                oRevelante.Fecha = oRevelanteCLS.fecha;




                bd.SaveChanges();


            }


            return RedirectToAction("Index");



        }




        public ActionResult Eliminar(int id)

        {

            using (var bd = new controlvisitanteEntities3())
            {

               



                Revelante oRevelante = bd.Revelante.Find(id);

                bd.Revelante.Remove(oRevelante);


                bd.SaveChanges();


            }


            return RedirectToAction("Index");

        }





    }





}