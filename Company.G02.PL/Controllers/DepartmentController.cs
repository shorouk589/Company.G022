using Company.G02.BLL.Interfaces;
using Company.G02.BLL.Repository;
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
    }
}
