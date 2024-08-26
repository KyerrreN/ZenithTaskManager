using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public string? Employer { get; set; }
        public string UserId { get; set; }
        public int? BossId { get; set; } // FK
        public Boss Boss { get; set; } // FK Constraint

        public Employee(string name, string surname, string patronymic, string department, string position, string employer, string userId)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Department = department;
            Position = position;
            Employer = employer;
            UserId = userId;
        }
    }
}
