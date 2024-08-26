namespace WebApplication2.Models
{
    public class Tasks
    {
        public int Id { get; set; } // FK
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AssignDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatus Status { get; set; }

        // Reference to Employee
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; } // FK

        // Reference to Boss
        public Boss Boss { get; set; }
        public int BossId { get; set; } // FK
    }
}
