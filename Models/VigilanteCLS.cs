using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


// activa para las columnas
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class VigilanteCLS
    {

        //se pone para la columnas  del listado

        [Display(Name = "Codigo")]



        public int idvigilante { get; set; }



        //******************************************************************************

        [Display(Name = "Vigilante")]

        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        [Required]

        [StringLength(50, ErrorMessage = "Caracteres Solo  50 Digitos")]

        public string nombre { get; set; }


        //*********************************************************
        [Display(Name = "Dni")]

        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        [Required]

        [StringLength(50, ErrorMessage = "Caracteres Solo  50 Digitos")]

        public string dni { get; set; }


       


        //******************************************************************************

        //*********************************************************
        [Display(Name = "Turno")]

        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        [Required]

        [StringLength(50, ErrorMessage = "Caracteres Solo  50 Digitos")]

        public string turno { get; set; }






        //*****************************************************************
        //se pone para la columnas  del listado

        [Display(Name = "Empresa")]

        public int idempresa { get; set; }

        //**************************************************************


        //*****************************************************************
        //se pone para la columnas  del listado

        [Display(Name = "Usuario")]

        public int idusuario { get; set; }

        //**************************************************************

        //*****************************************************************
      



        [Display(Name = "Empresa")]

        public string nombreempresa { get; set; }

        [Display(Name = "Usuario")]
        public string nombreusuario { get; set; }



        // validar  nombres  repetido
        public string mensajeERROR { get; set; }

    }

}