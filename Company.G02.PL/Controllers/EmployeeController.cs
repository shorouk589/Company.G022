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
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                //employees = _EmployeeRepository.GetALL();
                employees = await _unitOfWork.EmployeeRepository.GetALLAsync();

            }
            else
            {
                //  employees = _EmployeeRepository.GetByNameAsync(SearchInput);
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);

            }
            //var employees = _EmployeeRepository.GetALL();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //  var department = _departmentRepository.GetALL();
            var department = await _unitOfWork.DepartmentRepository.GetALLAsync();

            ViewData["departments"] = department;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
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
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await _unitOfWork.completeAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            var departments = await _unitOfWork.DepartmentRepository.GetALLAsync();
            ViewData["departments"] = departments;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null) return BadRequest("Invalid Id");//100
                                                            // var departments = _departmentRepository.GetALL();
            var departments = await _unitOfWork.DepartmentRepository.GetALLAsync();
            ViewData["departments"] = departments;

            // var employee = _EmployeeRepository.Get(id.Value);
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee == null) return NotFound("Employee Not Found");//404
            return View(ViewName, employee);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            // var department = _departmentRepository.GetALL();
            var department = await _unitOfWork.DepartmentRepository.GetALLAsync();
            ViewData["departments"] = department;
            if (id is null) return BadRequest("IS INVALID MESSAGE");//400

            //  var employee = _EmployeeRepository.Get(id.Value);
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

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












        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model)
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
                var count = await _unitOfWork.completeAsync();
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }

            var departments = await _unitOfWork.DepartmentRepository.GetALLAsync();
            ViewData["departments"] = departments;
            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            // var departments = _departmentRepository.GetALL();
            var departments = await _unitOfWork.DepartmentRepository.GetALLAsync();
            ViewData["departments"] = departments;

            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Employee model)
        {
            // var departments = _departmentRepository.GetALL();
            var departments = await _unitOfWork.DepartmentRepository.GetALLAsync();
            ViewData["departments"] = departments;

            if (id != model.Id) return BadRequest("Invalid Id"); //400    
                                                                 // var count = _EmployeeRepository.Delete(model);
                                                                 //   var count = _unitOfWork.EmployeeRepository.Delete(model);
            _unitOfWork.EmployeeRepository.Delete(model);
            var count = await _unitOfWork.completeAsync();
            if (count > 0)
            {
                if (model.ImageName is not null)
                {
                    DocumentSetting.DeleteFile(model.ImageName, "images");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


    }
}
