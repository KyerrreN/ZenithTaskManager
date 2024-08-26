namespace WebApplication2.Models
{
    public class Boss
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public int? DepartmentId { get; set; } // FK

        // Reference to FK in Department table
        public Department Department { get; set; }

        public string UserId { get; set; }
    }
}
