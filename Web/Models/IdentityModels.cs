using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Web.Models.Category> CategoryItems { get; set; }

        public System.Data.Entity.DbSet<Web.Models.Product> ProductItems { get; set; }

        public System.Data.Entity.DbSet<Web.Models.ProductPrice> PriceItems { get; set; }

        public System.Data.Entity.DbSet<Web.Models.Partner> Partners { get; set; }

        public System.Data.Entity.DbSet<Web.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<Web.Models.OrderDetail> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<Web.Models.Payment> Payments { get; set; }

        public System.Data.Entity.DbSet<Web.Models.PaymentDetail> PaymentDetails { get; set; }

        public System.Data.Entity.DbSet<Web.Models.CashInjection> CashInjections { get; set; }

        public System.Data.Entity.DbSet<Web.Models.UserSetting> UserSettings { get; set; }

        public System.Data.Entity.DbSet<Web.Models.Invoice> Invoices { get; set; }

        public System.Data.Entity.DbSet<Web.Models.Credit> Credits { get; set; }

        public System.Data.Entity.DbSet<Web.Models.CreditInvoice> CreditInvoice { get; set; }
    }
}