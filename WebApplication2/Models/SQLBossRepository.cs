
namespace WebApplication2.Models
{
    public class SQLBossRepository : IBossRepository
    {
        private readonly AppDbContext context;
        public SQLBossRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Boss AddBoss(Boss boss)
        {
            context.Bosses.Add(boss);
            context.SaveChanges();

            return boss;
        }

        public Boss DeleteBoss(int id)
        {
            Boss boss = context.Bosses.Find(id);

            if (boss is not null)
            {
                context.Bosses.Remove(boss);
                context.SaveChanges();
            }

            return boss;
        }

        public List<Boss> GetAllBoss()
        {
            return context.Bosses.ToList();
        }

        public Boss GetBoss(int id)
        {
            return context.Bosses.Find(id);
        }
        public Boss GetBoss(string name, string surname, string? patronymic)
        {
            var bossToGet = context.Bosses.FirstOrDefault(cred => cred.Name == name
                                                              && cred.Surname == surname
                                                              && cred.Patronymic == patronymic);

            return bossToGet;
        }

        public Boss GetBossByDepartment(int departmentId)
        {
            return context.Bosses.FirstOrDefault(boss => boss.DepartmentId == departmentId);
        }

        public Boss UpdateBoss(Boss boss)
        {
            var bossToUpdate = context.Bosses.Attach(boss);
            bossToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return boss;
        }

        public List<Boss> GetUnassignedBosses()
        {
            return context.Bosses.Where(dep => dep.DepartmentId == null).ToList();
        }
        public Boss AssignBossToDepartment(Boss boss, string depName)
        {
            var department = context.Departments.FirstOrDefault(dep => dep.Name == depName);
            boss.DepartmentId = department.Id;

            UpdateBoss(boss);

            return boss;
        }
        public Boss GetSignedInBoss(string userId)
        {
            return context.Bosses.FirstOrDefault(boss => boss.UserId == userId);
        }

        public Boss GetBossByUserId(string userId)
        {
            return context.Bosses.FirstOrDefault(boss => boss.UserId == userId);
        }
    }
}
