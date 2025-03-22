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
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _EmployeeRepository = employeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employee = _EmployeeRepository.GetALL();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
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
                    return RedirectToAction(nameof(Index));
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

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Id"); //400    
                var count = _EmployeeRepository.Update(model);
                if (count > 0) { return RedirectToAction(nameof(Index)); }
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
