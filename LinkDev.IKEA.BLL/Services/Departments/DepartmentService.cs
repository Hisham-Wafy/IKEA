using LinkDev.IKEA.BLL.DTOs.Departments;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentService)//Ask CLR Generate object from class
        {
            _departmentRepository = departmentService;
        }

        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllAsIQueryable().Select(department => new DepartmentDTO()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreateDate = department.CreateDate,

            }).AsNoTracking().ToList();

            ///foreach (var department in departments)
            ///{
            ///    yield return new DepartmentDTO()
            ///    {
            ///           Id = department.Id,
            ///           Name = department.Name,
            ///           Code = department.Code,
            ///           Description = department.Description,
            ///           CreateDate = department.CreateDate,
            ///    };
            ///}
            
            return departments;
        }

        public DepartmentDetailsDTO? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is { })
            {
                return new DepartmentDetailsDTO()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    CreateDate = department.CreateDate,
                    LastModifiedBy = department.LastModifiedBy,
                    CreatedAt = department.CreatedAt,
                    CreatedBy = department.CreatedBy,
                    LastModifiyAt = department.LastModifiyAt,
                    IsDeleted = department.IsDeleted,
                };
            }
            return null;
        }

        public int CreateDepartment(CreatedDepartmentDTO department)
        {
           var createdDepartment = new Department()
           {
              Name = department.Name,
              Code = department.Code,
              Description = department.Description,
              CreateDate = department.CreateDate,
              CreatedBy=1,
              LastModifiedBy =1,
              CreatedAt=DateTime.UtcNow,
              LastModifiyAt=DateTime.UtcNow,

           };
            return _departmentRepository.Add(createdDepartment);
        }


        public int UpdateDepartment(UpdatedDepartmentDTO department)
        {
            var updatedDepartment = new Department()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreateDate = department.CreateDate,
                CreatedBy = 1,
                LastModifiedBy = 1,
                CreatedAt = DateTime.UtcNow,
                LastModifiyAt = DateTime.UtcNow,
            };
            return _departmentRepository.Update(updatedDepartment);
        }
        public bool DeleteDepartment(int id)
        {
           var deleteDepartment = _departmentRepository.GetById(id);
            if(deleteDepartment is { }) 
                return _departmentRepository.Delete(deleteDepartment)>0;
            return false;
        }


       

    }
}
