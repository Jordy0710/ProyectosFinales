using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyecto.Models
{
    public class UsuarioCLS
    {

        //se pone para la columnas  del listado

        [Display(Name = "Codigo")]
        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        
        public int iidusuario { get; set; }



        [Display(Name = "Usuario")]
        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        [Required]

        [StringLength(50, ErrorMessage = "Caracteres Solo  50 Digitos")]
        //**************************************************************
        public string nombreusuario { get; set; }




        [Display(Name = "Contraseña")]

        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        [Required]

        [StringLength(50, ErrorMessage = "Caracteres Solo  50 Digitos")]
        //**************************************************************
        public string contra { get; set; }





        //se pone para la columnas  del listado

        [Display(Name = "Tipo")]

        // validar mensaje a la caja de textos  - que escriba solo 50 digitos para abajo
        [Required]

        [StringLength(50, ErrorMessage = "Caracteres Solo  50 Digitos")]
        //**************************************************************
        public string tipo { get; set; }
      




    }


    
}