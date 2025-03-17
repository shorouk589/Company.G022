using Company.G02.BLL.Interfaces;
using Company.G02.BLL.Repository;
using Company.G02.DAL.Models;
using Company.G02.PL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Company.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
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
                var count = _departmentRepository.Add(department);

                if (count > 0) { return RedirectToAction(nameof(Index)); }


            }


            return View();


        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest("IS INVALID MESSAGE");//400
            }
            var department = _departmentRepository.Get(id.Value);
            if (department == null)
            {
                return NotFound(new { StatusCode = 404, Message = $"Department with {id} is not valid" });


            }
            return View(department);
        }



    }
}
