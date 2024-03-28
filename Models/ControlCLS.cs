﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


// activa para las columnas
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class ControlCLS
    {

        //se pone para la columnas  del listado

        [Display(Name = "Codigo")]



        public int idcontrol{ get; set; }



        //******************************************************************************

        [Display(Name = "Descripcion")]

        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        [Required]

        [StringLength(50, ErrorMessage = "Caracteres Solo  50 Digitos")]

        public string descripcion { get; set; }


        //*********************************************************
        [Display(Name = "Motivo")]

        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        [Required]

        [StringLength(50, ErrorMessage = "Caracteres Solo  50 Digitos")]

        public string motivo { get; set; }





        //******************************************************************************

        //*********************************************************
        [Display(Name = "Hora")]

        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        [Required]

        [StringLength(50, ErrorMessage = "Caracteres Solo  50 Digitos")]

        public string hora { get; set; }




        //-----------------------
        [Display(Name = "Fecha ")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string fecha { get; set; }




        //*****************************************************************
        //se pone para la columnas  del listado

        [Display(Name = "Area")]

        public int idarea { get; set; }

        //**************************************************************


        //*****************************************************************
        //se pone para la columnas  del listado

        [Display(Name = "Vigilante")]

        public int idvigilante { get; set; }

        //**************************************************************


        //*****************************************************************
        //se pone para la columnas  del listado

        [Display(Name = "Visitante")]

        public int idvisitante { get; set; }

        //**************************************************************

        //*****************************************************************




        [Display(Name = "Vigilante")]

        public string nombrevigilante { get; set; }




        [Display(Name = "Visitante")]
        public string nombrevisitante { get; set; }




        [Display(Name = "Area")]
        public string nombrearea { get; set; }






        // validar  nombres  repetido
        public string mensajeERROR { get; set; }

    }

}