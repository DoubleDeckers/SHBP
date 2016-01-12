using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace SecondHandBusinessPlatform.Models
{
    public class SecondHandBusinessPlatformDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}