using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Company.G02.PL.DTO
{
    public class CreateEmployeeDto
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Name is Required !!")]
        public string EmpName { get; set; }

        [Range(22, 60, ErrorMessage = "Age Must Be Between 22 and 60")]
        public int? Age { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email is Not Valid !!")]
        public string Email { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
                           , ErrorMessage = "Address is Not Valid !!")]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Hiring Date ")]
        public DateTime HiringDate { get; set; }
        [DisplayName(" Date Of Creation ")]
        public DateTime CreateAt { get; set; }

        public int? DepartmentId { get; set; }
    }
}
