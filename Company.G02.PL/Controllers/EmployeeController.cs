using Company.G02.BLL.Interfaces;
using Company.G02.BLL.Repository;
using Company.G02.DAL.Models;
using Company.G02.PL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Company.G02.PL.Controllers
{

    public class EmployeeController : Controller
    {
        private IEmployeeRepository _EmployeeRepository;//Null
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _EmployeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var employees = _EmployeeRepository.GetALL();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepository.GetALL();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) //Server Side Validation
            {
                try
                {
                    var employee = new Employee()
                    {
                        Name = model.Name,
                        Age = model.Age,
                        Email = model.Email,
                        Phone = model.Phone,
                        Address = model.Address,
                        Salary = model.Salary,
                        IsActive = model.IsActive,
                        IsDeleted = model.IsDeleted,
                        DateOfBirth = model.HiringDate,
                        CreateAt = model.CreateAt
                    };
                    var count = _EmployeeRepository.Add(employee);

                    if (count > 0)
                    {
                        TempData["Message"] = "Employee Added Successfully";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);

                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null) return BadRequest("Invalid Id");//100

            var employee = _EmployeeRepository.Get(id.Value);
            if (employee == null) return NotFound("Employee Not Found");//404
            return View(ViewName, employee);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null) return BadRequest("IS INVALID MESSAGE");//400

            var employee = _EmployeeRepository.Get(id.Value);
            if (employee == null) return NotFound(new { StatusCode = 404, Message = $"Department with {id} is not valid" });

            var EmployeeDto = new CreateEmployeeDto()
            {

                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Phone = employee.Phone,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                HiringDate = employee.DateOfBirth,
                CreateAt = employee.CreateAt

            };


            //return View(department);

            return View(EmployeeDto);


            // return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                // if (id != model.Id) return BadRequest("Invalid Id"); //400
                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Age = model.Age,
                    Email = model.Email,
                    Phone = model.Phone,
                    Address = model.Address,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    DateOfBirth = model.HiringDate,
                    CreateAt = model.CreateAt
                };
                var count = _EmployeeRepository.Update(employee);
                if (count > 0) return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee model)
        {
            if (id != model.Id) return BadRequest("Invalid Id"); //400    
            var count = _EmployeeRepository.Delete(model);
            if (count > 0) { return RedirectToAction(nameof(Index)); }
            return View(model);
        }


    }
}
