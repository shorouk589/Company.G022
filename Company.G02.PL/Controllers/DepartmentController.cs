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
        public IActionResult Details(int? id, string ViewName = "Details")
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
            return View(ViewName, department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //    if (id is null)
            //    {
            //        return BadRequest("IS INVALID MESSAGE");//400

            //    }
            //    var department = _departmentRepository.Get(id.Value);
            //    if (department == null)
            //    {
            //        return NotFound(new { StatusCode = 404, Message = $"Department with {id} is not valid" });


            //    }
            //    return View(department);

            return Details(id, "Edit");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var count = _departmentRepository.Update(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                }
            }
            return View(department);

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken] // preferred for any post action
        //public IActionResult Edit([FromRoute] int id, UpdateDepatmentDto model)
        //{

        //    if (ModelState.IsValid)
        //    {


        //        var department = new Department()
        //        {
        //            Id = id,
        //            Name = model.Name,
        //            Code = model.Code,
        //            CreatedAt = model.CreateAt,
        //        };
        //        var count = _departmentRepository.Update(department);
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));

        //        }

        //    }
        //    return View(model);

        //}

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null)
            //{
            //    return BadRequest("IS INVALID MESSAGE");//400

            //}
            //var department = _departmentRepository.Get(id.Value);
            //if (department == null)
            //{
            //    return NotFound(new { StatusCode = 404, Message = $"Department with {id} is not valid" });


            //}
            //return View(department);

            return Details(id, "Delete");

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var count = _departmentRepository.Delete(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                }
            }
            return View(department);

        }

    }
}
