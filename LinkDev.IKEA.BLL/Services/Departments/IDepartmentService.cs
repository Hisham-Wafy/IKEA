using LinkDev.IKEA.BLL.DTOs.Departments;
using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDTO> GetAllDepartments();
        DepartmentDetailsDTO? GetDepartmentById(int id);

        int CreateDepartment(CreatedDepartmentDTO department);
        int UpdateDepartment(UpdatedDepartmentDTO department);

        bool DeleteDepartment(int id);




    }
}
