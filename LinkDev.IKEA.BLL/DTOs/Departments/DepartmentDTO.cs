using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.DTOs.Departments
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        //public bool IsDeleted { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public int CreatedBy { get; set; }
        //public int LastModifiedBy { get; set; }
        //public DateTime LastModifiyAt { get; set; }

        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; } = null!;
        [Display(Name = "Data of Creation")]
        public DateTime CreateDate { get; set; }

        //public static explicit operator DepartmentDTO(Department department)
        //{

        //  return new DepartmentDTO()
        //  {
        //      Id = department.Id,
        //      Name = department.Name,
        //      Code = department.Code,
        //      Description = department.Description,
        //      CreateDate = department.CreateDate,




        //  };
        //}
    

    }
}
