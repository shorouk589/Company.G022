using Company.G02.BLL.Interfaces;
using Company.G02.BLL.Repository;
using Company.G02.DAL.Models;
using Company.G02.PL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Company.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository =  departmentRepository;
        }

        [HttpGet] // GET: /Department/Index
        public IActionResult Index()
        {
          
            var department = _departmentRepository.GetALL();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            
            return View();

        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreatedAt = model.CreateAt

                };
              var count=  _departmentRepository.Add(department);

                if (count > 0) { return RedirectToAction(nameof(Index)); }

               
            }
          

            return View();

        }



    }
}
