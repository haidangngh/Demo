using System.ComponentModel.DataAnnotations;

namespace AppData.Models
{
    public partial class staff
    {
        public staff()
        {
            DepartmentFacilities = new HashSet<DepartmentFacility>();
            StaffMajorFacilities = new HashSet<StaffMajorFacility>();
        }

        public byte? Status { get; set; }

        [Required]
        public long? CreatedDate { get; set; }

        [Required]
        public long? LastModifiedDate { get; set; }

        [Required]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^\s@]+@fe\.edu\.vn$", ErrorMessage = "Email FE must end with @fe.edu.vn and cannot contain spaces.")]
        public string? AccountFe { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^\s@]+@fpt\.edu\.vn$", ErrorMessage = "Email FPT must end with @fpt.edu.vn and cannot contain spaces.")]
        public string? AccountFpt { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "StaffCode cannot be longer than 15 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "StaffCode cannot contain special characters or spaces.")]
        public string? StaffCode { get; set; }

        public virtual ICollection<DepartmentFacility> DepartmentFacilities { get; set; }
        public virtual ICollection<StaffMajorFacility> StaffMajorFacilities { get; set; }
    }
}
