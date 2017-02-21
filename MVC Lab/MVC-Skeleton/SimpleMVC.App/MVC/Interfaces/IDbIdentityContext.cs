﻿using SimpleMVC.App.Models;
using System.Data.Entity;

namespace SimpleMVC.App.MVC.Interfaces
{
    public interface IDbIdentityContext
    {
        DbSet<Login> Logins { get; }
        DbSet<User> Users { get; }
        void SaveChanges();
    }
}
