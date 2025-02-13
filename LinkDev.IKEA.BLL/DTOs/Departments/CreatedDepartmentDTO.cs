using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.DTOs.Departments
{
    public class CreatedDepartmentDTO
    {
        [Required(ErrorMessage ="Name Is Required!!")]
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; } = null!;
        [Display(Name ="Creating Date")]
        public DateTime CreateDate { get; set; }




    }
}
