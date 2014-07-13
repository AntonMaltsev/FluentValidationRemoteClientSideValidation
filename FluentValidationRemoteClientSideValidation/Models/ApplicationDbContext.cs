using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FluentValidationRemoteClientSideValidation.Models
{
    public interface IApplicationDbContext
    {
        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<IdentityRole> Roles { get; set; }

        Task<int> SaveChangesAsync();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}