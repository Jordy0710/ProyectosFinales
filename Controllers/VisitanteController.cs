using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class VisitanteController : Controller
    {
        // GET: Visitante
        public ActionResult Index(VisitanteCLS oVisitanteCLS)
        {

            string nombretipo = oVisitanteCLS.nombre;


            List<VisitanteCLS> listavisitante = null;

            using (var bd = new controlvisitanteEntities3())

            {


                if (oVisitanteCLS.nombre == null)

                {



                    listavisitante = (from visitante in bd.Visitante



                                      select new VisitanteCLS

                                      {


                                     idtipo = visitante.Id_Visitante,
                                     nombre = visitante.nombre,
                                          dni = visitante.dni,

                                      }).ToList();

                }
                else

                {


                    listavisitante= (from visitante in bd.Visitante

                                 where visitante.nombre.Contains(nombretipo)

                                 select new VisitanteCLS

                                 {




                                     idtipo = visitante.Id_Visitante,
                                     nombre = visitante.nombre,
                                     dni = visitante.dni

                                 }).ToList();

                }
            }
            return View(listavisitante);

        }



      





        public ActionResult Agregar()

        {

            return View();



        }

        // AQUI SOLO ENVIAR LOS DATOS  HACIA OTRO FORMULARIOS  ***************************************************

        public ActionResult Editar(int id)

        {

            //llama la clases 
            VisitanteCLS oVisitanteCLS = new VisitanteCLS();

            //  clinicadentalEntities1 se pone el nombre de la base de dato   -  entities  biene por defecto del mismo asp.net


            using (var bd = new controlvisitanteEntities3())
            {

                // LA tabla TIPO  CON MASYUSCULA 

                // oTipo es un objecto

                // where  buscar  el id_tipo 

                Visitante oVisitante = bd.Visitante.Where(p => p.Id_Visitante.Equals(id)).First();

                oVisitanteCLS.idtipo = oVisitante.Id_Visitante;

                oVisitanteCLS.nombre = oVisitante.nombre;

                oVisitanteCLS.dni = oVisitante.dni;

            }


            return View(oVisitanteCLS);





        }


        [HttpPost]
        public ActionResult Editar(VisitanteCLS oVisitanteCLS)
        {
            {
                if (!ModelState.IsValid)

                {

                    return View(oVisitanteCLS);

                }


                int idProducto = oVisitanteCLS.idtipo;






                using (var bd = new controlvisitanteEntities3())
                {


                    Visitante oVisitante= bd.Visitante.Where(p => p.Id_Visitante.Equals(idProducto)).First();



                    oVisitante.Id_Visitante = oVisitanteCLS.idtipo;

                    oVisitante.nombre = oVisitanteCLS.nombre;

                    oVisitante.dni = oVisitanteCLS.dni;

                 


                    bd.SaveChanges();


                }


                return RedirectToAction("Index");
            }
        }




        [HttpPost]
        public ActionResult Agregar(VisitanteCLS oVisitanteCLS)
        {


            //int registroEncontrado = 0;

            string repetido = oVisitanteCLS.nombre;

            //using (var bd = new controlvisitanteEntities3())

            //{
            //    registroEncontrado = bd.Visitante.Where(p => p.nombre.Equals(repetido)).Count();


            //}






            //if (!ModelState.IsValid || registroEncontrado >= 1)

            //{

            //    if (registroEncontrado >= 1) oVisitanteCLS.mensajeERROR = "Ya Exite el Registro";

            //    return View(oVisitanteCLS);

            //}
            //else

            {



                using (var bd = new controlvisitanteEntities3())

                {
                    Visitante oVisitante = new Visitante();

                    oVisitante.nombre = oVisitanteCLS.nombre;

                    oVisitante.dni = oVisitanteCLS.dni;

                    bd.Visitante.Add(oVisitante);

                    bd.SaveChanges();


                }

            }

            return RedirectToAction("Index");






        }





        public ActionResult Eliminar(int id)
        {

            using (var bd = new controlvisitanteEntities3())
            {

               

                Visitante oVisitante = bd.Visitante.Find(id);

                bd.Visitante.Remove(oVisitante);


                bd.SaveChanges();


            }


            return RedirectToAction("Index");
        }








    }
}