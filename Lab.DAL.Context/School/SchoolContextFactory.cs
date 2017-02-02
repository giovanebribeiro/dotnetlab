using System.Data.Entity.Infrastructure;

namespace Lab.DAL.Context.School
{
    public class SchoolContextFactory : IDbContextFactory<SchoolContext>
    {
        public SchoolContext Create()
        {
            return new SchoolContext();
        }
    }
}
