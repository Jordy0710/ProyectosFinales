using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;


namespace Proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index(UsuarioCLS oUsuarioCLS)
        {

            string nombretipo = oUsuarioCLS.nombreusuario;


            List<UsuarioCLS> listausuario = null;

         


            using (var bd = new controlvisitanteEntities3())

            {


                if (oUsuarioCLS.nombreusuario == null)

                {



                    listausuario = (from usuario in bd.Usuario



                                    select new UsuarioCLS

                                    {


                                        iidusuario = usuario.idusuario,
                                        nombreusuario = usuario.nombre,
                                        contra = usuario.password,

                                        tipo = usuario.tipo


                                    }).ToList();

                }
                else

                {


                    listausuario = (from usuario in bd.Usuario

                                    where usuario.nombre.Contains(nombretipo)

                                    select new UsuarioCLS

                                    {



                                        iidusuario = usuario.idusuario,
                                        nombreusuario = usuario.nombre,
                                        contra = usuario.password,

                                        tipo = usuario.tipo



                                    }).ToList();

               

            }
        }
            return View(listausuario);

    }



        public ActionResult Agregar()

        {

            return View();



        }

        // AQUI SOLO ENVIAR LOS DATOS  HACIA OTRO FORMULARIOS  ***************************************************

        public ActionResult Editar(int id)

        {

            //llama la clases 
            UsuarioCLS oUsuarioCLS = new UsuarioCLS();

            //  clinicadentalEntities1 se pone el nombre de la base de dato   -  entities  biene por defecto del mismo asp.net


            using (var bd = new controlvisitanteEntities3())
            {

                // LA tabla TIPO  CON MASYUSCULA 

                // oTipo es un objecto

                // where  buscar  el id_tipo 

                Usuario oUsuario = bd.Usuario.Where(p => p.idusuario.Equals(id)).First();

                oUsuarioCLS.iidusuario = oUsuario.idusuario;

                oUsuarioCLS.nombreusuario = oUsuario.nombre;

                oUsuarioCLS.contra = oUsuario.password;

                oUsuarioCLS.tipo  = oUsuario.tipo;

            }


            return View(oUsuarioCLS);





        }


        [HttpPost]
        public ActionResult Editar(UsuarioCLS oUsuarioCLS)
        {
            {
                if (!ModelState.IsValid)

                {

                    return View(oUsuarioCLS);

                }


                int idProducto = oUsuarioCLS.iidusuario;






                using (var bd = new controlvisitanteEntities3())
                {


                    Usuario oUsuario = bd.Usuario.Where(p => p.idusuario.Equals(idProducto)).First();



                    oUsuario.idusuario  = oUsuarioCLS.iidusuario;

                    oUsuario.nombre = oUsuarioCLS.nombreusuario;

                    oUsuario.password = oUsuarioCLS.contra;

                    oUsuario.tipo = oUsuarioCLS.tipo;


                    bd.SaveChanges();


                }


                return RedirectToAction("Index");
            }
        }




        [HttpPost]
        public ActionResult Agregar(UsuarioCLS oUsuarioCLS)
        {


            //int registroEncontrado = 0;

            string repetido = oUsuarioCLS.nombreusuario;

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
                    Usuario oUsuario = new Usuario();

                    oUsuario.nombre = oUsuarioCLS.nombreusuario;

                    oUsuario.password = oUsuarioCLS.contra;

                    oUsuario.tipo = oUsuarioCLS.tipo;


                    bd.Usuario.Add(oUsuario);

                    bd.SaveChanges();


                }

            }

            return RedirectToAction("Index");






        }





        public ActionResult Eliminar(int id)
        {

            using (var bd = new controlvisitanteEntities3())
            {



                Usuario oUsuario = bd.Usuario.Find(id);

                bd.Usuario.Remove(oUsuario);


                bd.SaveChanges();


            }


            return RedirectToAction("Index");
        }








    }
}