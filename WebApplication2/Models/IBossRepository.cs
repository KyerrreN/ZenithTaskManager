namespace WebApplication2.Models
{
    public interface IBossRepository
    {
        public Boss AddBoss(Boss boss);
        public Boss UpdateBoss(Boss boss);
        public Boss DeleteBoss(int id);
        public Boss GetBoss(int id);
        public Boss GetBoss(string name, string surname, string? patronymic);
        public Boss GetSignedInBoss(string userId);
        public Boss GetBossByDepartment(int departmentId);
        public List<Boss> GetAllBoss();
        public List<Boss> GetUnassignedBosses();
        public Boss AssignBossToDepartment(Boss boss, string depName);
        public Boss GetBossByUserId(string userId);
    }
}
