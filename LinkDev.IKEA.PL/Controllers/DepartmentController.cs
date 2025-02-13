using LinkDev.IKEA.BLL.DTOs.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {

        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService,
            ILogger<DepartmentController> logger,
            IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var department = _departmentService.GetAllDepartments();


            return View(department);
        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDTO department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var message = string.Empty;

            try
            {
                var result = _departmentService.CreateDepartment(department);

                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    message = "Department Is Not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(department);

                }
            }
            catch (Exception ex)
            {
                // 1.Log Exception
                _logger.LogError(ex, ex.Message);

                // 2.Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the department :(";

            }

            ModelState.AddModelError(string.Empty, message);
            return View(department);

        }
        #endregion

        #region Details

        [HttpGet]  //Get:/Department/Details
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
            {
                return NotFound();
            }
            return View(department);
        }

        #endregion

        #region Edit

        [HttpGet] // Get :/Department/Edit
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
            {
                return NotFound(); //404
            }

            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreateDate = department.CreateDate,
            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);
            var message = string.Empty;
            try
            {
                var departmentToUpdate = new UpdatedDepartmentDTO()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreateDate = departmentVM.CreateDate,

                };

                var result = _departmentService.UpdateDepartment(departmentToUpdate) > 0;

                if (result)
                    return RedirectToAction("Index");

                message = "an error has occured during updating the department :(";


            }
            catch (Exception ex)
            {
                // 1.Log Exception
                _logger.LogError(ex, ex.Message);

                // 2.Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the department :(";

            }

            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);
        }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = _departmentService.DeleteDepartment(id);

                if (deleted)
                {
                    return RedirectToAction("Index");
                }

                message = "An error occurred while deleting the department.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "An error occurred.";
            }

            var department = _departmentService.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }

            ModelState.AddModelError(string.Empty, message);
            return View(department);
        }


        #endregion


    }

    // min = 18 : 8
}
