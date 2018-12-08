using System.Data.Entity;
namespace DataAccess
{
    public class DataAccessContext :DbContext
    {
       public  DbSet<EntityModel> entityContext { get; set; }
    }
}
