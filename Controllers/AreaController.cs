using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Proyecto.Models;


namespace Proyecto.Controllers
{
    public class AreaController : Controller
    {
        // GET: Area
        public ActionResult Index(AreaCLS oAreaCLS)
        {


            string nombretipo = oAreaCLS.nombre;


            List<AreaCLS> listaarea = null;

            using (var bd = new controlvisitanteEntities3())

            {


                if (oAreaCLS.nombre == null)

                {



                    listaarea = (from area in bd.Area



                                 select new AreaCLS

                                 {


                                      idtipo = area.Id_Area,
                                      nombre = area.Nombre
                                  }).ToList();

                }
                else

                {


                    listaarea = (from area in bd.Area

                                  where area.Nombre.Contains(nombretipo)

                                  select new AreaCLS

                                  {



                                      idtipo = area.Id_Area,
                                      nombre = area.Nombre
                                  }).ToList();

                }
            }
            return View(listaarea);



        }


        public ActionResult Agregar()

        {

            return View();



        }


        public ActionResult Editar(int id)
        {
            //llama la clases 
            AreaCLS oAreaCLS = new AreaCLS();

            //  clinicadentalEntities1 se pone el nombre de la base de dato   -  entities  biene por defecto del mismo asp.net


            using (var bd = new controlvisitanteEntities3())
            {

                // LA tabla TIPO  CON MASYUSCULA 

                // oTipo es un objecto

                // where  buscar  el id_tipo 

                Area oMarca = bd.Area.Where(p => p.Id_Area.Equals(id)).First();

                oAreaCLS.idtipo = oMarca.Id_Area;

                oAreaCLS.nombre = oMarca.Nombre;

            }


            return View(oAreaCLS);



        }





        [HttpPost]
        public ActionResult Agregar(AreaCLS oAreaCLS)

        {

            //int registroEncontrado = 0;

            //string repetido = oAreaCLS.nombre;

            //using (var bd = new controlvisitanteEntities3())

            //{
            //    //registroEncontrado = bd.Area.Where(p => p.Nombre.Equals(repetido)).Count();


            //}






            //if (!ModelState.IsValid || registroEncontrado >= 1)

            //{

            //    if (registroEncontrado >= 1) oAreaCLS.mensajeERROR = "Ya Exite el Registro";

            //    return View(oAreaCLS);

            //}
            //else

            {



                using (var bd = new controlvisitanteEntities3())

                {
                    Area oArea = new Area();

                    oArea.Nombre = oAreaCLS.nombre;


                    bd.Area.Add(oArea);

                    bd.SaveChanges();


                }

            }

            return RedirectToAction("Index");




        }

        [HttpPost]
        public ActionResult Editar(AreaCLS oAreaCLS)
        {
            {
                if (!ModelState.IsValid)

                {

                    return View(oAreaCLS);

                }


                int idProducto = oAreaCLS.idtipo;






                using (var bd = new controlvisitanteEntities3())
                {


                    Area oArea = bd.Area.Where(p => p.Id_Area.Equals(idProducto)).First();



                    oArea.Id_Area= oAreaCLS.idtipo;

                    oArea.Nombre= oAreaCLS.nombre;






                    bd.SaveChanges();


                }


                return RedirectToAction("Index");
            }
        }



        // Eliminar los registro de una Fila 

        //    [HttpPost]
        public ActionResult Eliminar(int id)

        {

            using (var bd = new controlvisitanteEntities3())
            {

               

         
               Area oarea = bd.Area.Find(id);

                bd.Area.Remove(oarea);


                bd.SaveChanges();


            }


            return RedirectToAction("Index");

        }
    }
}