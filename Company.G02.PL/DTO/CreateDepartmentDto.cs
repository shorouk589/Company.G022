using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.DTO
{
    public class CreateDepartmentDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code Is Required !")]
        public int Code { get; set; }
        [Required(ErrorMessage = "Name Is Required !")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Create At Is Required !")]
        public DateTime CreatedAt { get; set; }













    }
}
