using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace History_Web
{
    public class OneViewModel
    {
        public int FigureID { get; set; }
        [Required]
        [Display(Name = "FigureName")]
        public string FigureName { get; set; }
        [Required]
        [Display(Name = "FigureDOB")]
        public DateTime FigureDOB { get; set; }
        [Required]
        [Display(Name = "FigureDOB")]
        public DateTime FigureDOD { get; set; }
        public int CivID { get; set; }
        [Required]
        [Display(Name = "CivName")]
        public string CivName { get; set; }
        public DateTime CivStart { get; set; }
        public DateTime CivEnd { get; set; }
        public SelectList Civs { get; set; }
        public string NewCivName { get; set; }
    }
}