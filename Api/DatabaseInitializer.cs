using Api.Domain;
using Api.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Api
{
    public static class DatabaseInitializer
    {
        public static void Initialize(ViceContext dbContext)
        {
            Console.WriteLine("Initializing database with context that follows");
            if (dbContext == null)
            {
                throw new Exception("You must provide a dbContext.");
            }

            // Console.WriteLine(JsonSerializer.Serialize(dbContext));
            var isDirty = false;

            dbContext.Database.Migrate();

            if (!dbContext.Users.Any())
            {
                dbContext.Users.Add(new User
                {
                    Id = "auth0|5f4e280d397b70006749858f",
                    Name = "Demo User",
                });

                isDirty = true;
            }

            if (isDirty)
            {
                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}