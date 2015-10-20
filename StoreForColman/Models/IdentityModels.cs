using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StoreForColman.Helpers;
using System.Collections.Generic;
using System;

namespace StoreForColman.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [DefaultValue(false)]
        [Required]
        [DisplayName("האם מנהל")]
        public bool IsAdmin { get; set; }

        [DisplayName("שם מלא")]
        [Required]
        public string FullName { get; set; }

        [DisplayName("סיסמה")]
        public override string PasswordHash
        {
            get
            {
                return base.PasswordHash;
            }

            set
            {
                base.PasswordHash = value;
            }
        }

        [DisplayName("שם משתמש")]
        public override string UserName
        {
            get
            {
                return base.UserName;
            }

            set
            {
                base.UserName = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Order
    {
        public int ID { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<OrderedProduct> Products { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class OrderedProduct
    {
        public int ID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double CurrentPriceInNIS { get; set; }
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

        [NotMapped]
        public bool IsAvailable
        {
            get
            {
                return AmountInStore > 0;
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
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedProduct> OrderedProduct { get; set; }

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