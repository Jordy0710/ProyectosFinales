using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


// activa para las columnas
using System.ComponentModel.DataAnnotations;


namespace Proyecto.Models
{
    public class VisitanteCLS
    {

        //se pone para la columnas  del listado

        [Display(Name = "Codigo")]


        public int idtipo { get; set; }





        [Display(Name = "Visitante")]

        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        [Required]

        [StringLength(50, ErrorMessage = "Caracteres Solo  50 Digitos")]
        //**************************************************************
        public string nombre { get; set; }





        [Display(Name = "Dni")]

        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        [Required]

        [StringLength(50, ErrorMessage = "Caracteres Solo  50 Digitos")]
        //**************************************************************
        public string dni { get; set; }


        // validar  nombres  repetido
        public string mensajeERROR { get; set; }

    }
}