using AutoMapper;
using Company.G02.BLL;
using Company.G02.BLL.Interfaces;
using Company.G02.BLL.Repository;
using Company.G02.DAL.Models;
using Company.G02.PL.DTO;
using Company.G02.PL.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Company.G02.PL.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private IEmployeeRepository _EmployeeRepository;//Null
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            //_EmployeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                //employees = _EmployeeRepository.GetALL();
                employees = _unitOfWork.EmployeeRepository.GetALL();

            }
            else
            {
                //  employees = _EmployeeRepository.GetByName(SearchInput);
                employees = _unitOfWork.EmployeeRepository.GetByName(SearchInput);

            }
            //var employees = _EmployeeRepository.GetALL();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //  var department = _departmentRepository.GetALL();
            var department = _unitOfWork.DepartmentRepository.GetALL().ToList();

            ViewData["departments"] = department;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) //Server Side Validation
            {// Manual Mapping
             //var employee = new Employee()
             //{

                //    Name = model.Name,
                //    Age = model.Age,
                //    Email = model.Email,
                //    Phone = model.Phone,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    DateOfBirth = model.HiringDate,
                //    CreateAt = model.CreateAt,
                //    DepartmentId = model.DepartmentId,

                //};

                if (model.Image != null)
                {

                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                }


                var employee = _mapper.Map<CreateEmployeeDto, Employee>(model);
                //   var count = _EmployeeRepository.Add(employee);
                _unitOfWork.EmployeeRepository.Add(employee);
                var count = _unitOfWork.complete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            var departments = _unitOfWork.DepartmentRepository.GetALL();
            ViewData["departments"] = departments;

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null) return BadRequest("Invalid Id");//100
                                                            // var departments = _departmentRepository.GetALL();
            var departments = _unitOfWork.DepartmentRepository.GetALL();
            ViewData["departments"] = departments;

            // var employee = _EmployeeRepository.Get(id.Value);
            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee == null) return NotFound("Employee Not Found");//404
            return View(ViewName, employee);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            // var department = _departmentRepository.GetALL();
            var department = _unitOfWork.DepartmentRepository.GetALL();
            ViewData["departments"] = department;
            if (id is null) return BadRequest("IS INVALID MESSAGE");//400

            //  var employee = _EmployeeRepository.Get(id.Value);
            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);

            if (employee == null) return NotFound(new { StatusCode = 404, Message = $"Department with {id} is not valid" });

            //var EmployeeDto = new CreateEmployeeDto()
            //{

            //    Name = employee.Name,
            //    Age = employee.Age,
            //    Email = employee.Email,
            //    Phone = employee.Phone,
            //    Address = employee.Address,
            //    Salary = employee.Salary,
            //    IsActive = employee.IsActive,
            //    IsDeleted = employee.IsDeleted,
            //    HiringDate = employee.DateOfBirth,
            //    CreateAt = employee.CreateAt,
            //    DepartmentId = employee.DepartmentId,

            //};

            var EmployeeDto = _mapper.Map<Employee, CreateEmployeeDto>(employee);

            EmployeeDto.Id = employee.Id;
            EmployeeDto.EmpName = employee.Name;
            return View(EmployeeDto);

            //return View(department);

            // return Details(id, "Edit");





            //return View(employee);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // if (id != model.Id) return BadRequest("Invalid Id"); //400
        //        //var employee = new Employee()
        //        //{
        //        //    Id = id,
        //        //    Name = model.Name,
        //        //    Age = model.Age,
        //        //    Email = model.Email,
        //        //    Phone = model.Phone,
        //        //    Address = model.Address,
        //        //    Salary = model.Salary,
        //        //    IsActive = model.IsActive,
        //        //    IsDeleted = model.IsDeleted,
        //        //    DateOfBirth = model.HiringDate,
        //        //    CreateAt = model.CreateAt,
        //        //    DepartmentId = model.DepartmentId,
        //        //};

        //        if (model.ImageName != null && model.Image != null)
        //        {

        //            DocumentSetting.DeleteFile(model.ImageName, "images");
        //        }
        //        if (model.Image != null)
        //        {
        //            model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
        //        }


        //        DocumentSetting.UploadFile(model.Image, "images");


        //        var employee = _mapper.Map<CreateEmployeeDto, Employee>(model);

        //        _unitOfWork.EmployeeRepository.Update(employee);
        //        var count = _unitOfWork.complete();
        //        if (count > 0) return RedirectToAction(nameof(Index));


        //    }
        //    //  var departments = _departmentRepository.GetALL();
        //    var departments = _unitOfWork.DepartmentRepository.GetALL();
        //    ViewData["departments"] = departments;

        //    return View(model);
        //}











        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    // لو في صورة قديمة، امسحيها
                    if (!string.IsNullOrEmpty(model.ImageName))
                    {
                        DocumentSetting.DeleteFile(model.ImageName, "images");
                    }

                    // حمّلي الصورة الجديدة
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                }

                var employee = _mapper.Map<CreateEmployeeDto, Employee>(model);

                _unitOfWork.EmployeeRepository.Update(employee);
                var count = _unitOfWork.complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }

            var departments = _unitOfWork.DepartmentRepository.GetALL();
            ViewData["departments"] = departments;
            return View(model);
        }




        [HttpGet]
        public IActionResult Delete(int? id)
        {
            // var departments = _departmentRepository.GetALL();
            var departments = _unitOfWork.DepartmentRepository.GetALL();
            ViewData["departments"] = departments;

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee model)
        {
            // var departments = _departmentRepository.GetALL();
            var departments = _unitOfWork.DepartmentRepository.GetALL();
            ViewData["departments"] = departments;

            if (id != model.Id) return BadRequest("Invalid Id"); //400    
                                                                 // var count = _EmployeeRepository.Delete(model);
                                                                 //   var count = _unitOfWork.EmployeeRepository.Delete(model);
            _unitOfWork.EmployeeRepository.Delete(model);
            var count = _unitOfWork.complete();
            if (count > 0) {
                if (model.ImageName is not null)
                {
                    DocumentSetting.DeleteFile(model.ImageName, "images");
                }
                return RedirectToAction(nameof(Index)); }
            return View(model);
        }


    }
}
