using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class VigilanteController : Controller
    {
        
       

        public ActionResult Index(VigilanteCLS oVigilanteCLS)
        {
            string nombrevigilante = oVigilanteCLS.nombre;


            List<VigilanteCLS> listavigilante = null;

            using (var bd = new controlvisitanteEntities3())

            {


                if (oVigilanteCLS.nombre == null)

                {



                   listavigilante = (from vigilante in bd.Vigilante

                                     join empresa in bd.Empresa on vigilante.Id_Empresa equals empresa.Id_Empresa

                                  

                                     join usuario in bd.Usuario on vigilante.idusuario equals usuario.idusuario


                                     select new VigilanteCLS

                                     {


                                        idvigilante = vigilante.Id_Vigilante,
                                         nombre = vigilante.Nombre,

                                         dni = vigilante.Dni,

                                         turno = vigilante.Turno,
                                         nombreempresa = empresa.Nombre,

                                         nombreusuario = usuario.nombre,


                                         
                                       



                                     }).ToList();

                }
                else

                {

                    listavigilante = (from vigilante in bd.Vigilante


                                      join empresa in bd.Empresa on vigilante.Id_Empresa equals empresa.Id_Empresa



                                      join usuario in bd.Usuario on vigilante.idusuario equals usuario.idusuario







                                      where vigilante.Nombre.Contains(nombrevigilante)

                                      select new VigilanteCLS

                                      {


                                       



                                         idvigilante = vigilante.Id_Vigilante,
                                         nombre = vigilante.Nombre,

                                         dni = vigilante.Dni,

                                         turno = vigilante.Turno,
                                         nombreempresa = empresa.Nombre,

                                         nombreusuario = usuario.nombre,






                                     }).ToList();

                }
            }
            return View(listavigilante);
        }



      

        private void llenarempresa()

        {
            List<SelectListItem> lista;

            using (var bd = new controlvisitanteEntities3())

            {

                lista = (from empresa in bd.Empresa

                         select new SelectListItem
                         {
                             // visible el nombre 
                             Text = empresa.Nombre,

                             // aqui va el id  
                             Value = empresa.Id_Empresa.ToString()

                         }).ToList();


                lista.Insert(0, new SelectListItem { Text = "--Seleccionar--", Value = "" });

                ViewBag.listaempresa = lista;

            }


        }


        //**************************************************




        private void llenarusuario()

        {
            List<SelectListItem> lista;

            using (var bd = new controlvisitanteEntities3())

            {

                lista = (from usuario in bd.Usuario

                         select new SelectListItem
                         {
                             // visible el nombre 
                             Text = usuario.nombre,

                             // aqui va el id  
                             Value = usuario.idusuario.ToString()

                         }).ToList();


                lista.Insert(0, new SelectListItem { Text = "--Seleccionar--", Value = "" });


                ViewBag.listausuario = lista;
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
            llenarempresa();
            llenarusuario();
           
        }



        // AQUI SOLO ENVIAR LOS DATOS  HACIA OTRO FORMULARIOS  ***************************************************

        public ActionResult Editar(int id)

        {
            //llama la clases 
            VigilanteCLS oVigilanteCLS = new VigilanteCLS();

           


            using (var bd = new controlvisitanteEntities3())
            {


                listarCombos();


  

                Vigilante oVigilante = bd.Vigilante.Where(p => p.Id_Vigilante.Equals(id)).First();

                oVigilanteCLS.idvigilante= oVigilante.Id_Vigilante;

                oVigilanteCLS.nombre = oVigilante.Nombre;

                oVigilanteCLS.dni = oVigilante.Dni;

                oVigilanteCLS.turno = oVigilante.Turno;



                oVigilanteCLS.idempresa = (int)oVigilante.Id_Empresa;

                oVigilanteCLS.idusuario = (int)oVigilante.idusuario;




            }


            return View(oVigilanteCLS);



        }




        [HttpPost]
        public ActionResult Agregar(VigilanteCLS oVigilanteCLS)

        {


            ////**************************************

            int registroEncontrado = 0;

            string repetido = oVigilanteCLS.nombre;

            using (var bd = new controlvisitanteEntities3())

            {
                registroEncontrado = bd.Vigilante.Where(p => p.Nombre.Equals(repetido)).Count();


            }






            if (!ModelState.IsValid || registroEncontrado >= 1)

            {




                if (registroEncontrado >= 1) oVigilanteCLS.mensajeERROR = "Ya Exite el Vigilante ";


                listarCombos();


                return View(oVigilanteCLS);

            }
            else

            {



                using (var bd = new controlvisitanteEntities3())

                {
                    Vigilante oVigilante = new Vigilante();

                    oVigilante.Nombre = oVigilanteCLS.nombre;

                    oVigilante.Dni= oVigilanteCLS.dni;

                    oVigilante.Turno = oVigilanteCLS.turno;

                    oVigilante.Id_Empresa = oVigilanteCLS.idempresa;

                    oVigilante.idusuario = oVigilanteCLS.idusuario;

         

                  


                    bd.Vigilante.Add(oVigilante);

                    bd.SaveChanges();


                }

            }

            return RedirectToAction("Index");




        }



        [HttpPost]
        public ActionResult Editar(VigilanteCLS oVigilanteCLS)

        {
            if (!ModelState.IsValid)

            {

                return View(oVigilanteCLS);

            }


            int idProducto = oVigilanteCLS.idvigilante;






            using (var bd = new controlvisitanteEntities3())
            {


                Vigilante oVigilante = bd.Vigilante.Where(p =>p.Id_Vigilante.Equals(idProducto)).First();



                oVigilante.Id_Vigilante = oVigilanteCLS.idvigilante;

                oVigilante.Nombre = oVigilanteCLS.nombre;

                oVigilante.Dni = oVigilanteCLS.dni;

                oVigilante.Turno = oVigilanteCLS.turno;



                oVigilante.Id_Empresa = oVigilanteCLS.idempresa;

                oVigilante.idusuario = oVigilanteCLS.idusuario;












                bd.SaveChanges();


            }


            return RedirectToAction("Index");



        }





        public ActionResult Eliminar(int id)

        {

            using (var bd = new controlvisitanteEntities3())
            {

              




                Vigilante oVigilante = bd.Vigilante.Find(id);

                bd.Vigilante.Remove(oVigilante);


                bd.SaveChanges();
            }


            return RedirectToAction("Index");

        }











    }
}