using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StoreForColman.Helpers;

namespace StoreForColman.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [DefaultValue(false)]
        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public string FullName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    [DisplayName("מוצר")]
    public class Product
    {
        [DisplayName("מקט")]
        public int ID { get; set; }

        [DisplayName("שם המוצר")]
        [Required]
        public string Name { get; set; }

        [DisplayName("מחיר בש\"ח")]
        [Required]
        public double PriceInNIS { get; set; }

        [DisplayName("יצרן")]
        [Required]
        public string ManufactorName { get; set; }

        [DisplayName("כמות במלאי")]
        [Required]
        public int AmountInStore { get; set; }

        [DisplayName("האם קיים במלאי")]
        [NotMapped]
        public bool IsAvailable
        {
            get
            {
                return this.AmountInStore > 0;
            }
        }

        [NotMapped]
        public Dollar USD
        {
            get { return new Dollar(PriceInNIS / 4); /* TODO: MAKE SURE WE CONVERT IT */ }
        }

        [NotMapped]
        public Euro EUR
        {
            get { return new Euro(PriceInNIS / 5); }
        }

        [NotMapped]
        public Shekel NIS
        {
            get { return new Shekel(PriceInNIS); }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }

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