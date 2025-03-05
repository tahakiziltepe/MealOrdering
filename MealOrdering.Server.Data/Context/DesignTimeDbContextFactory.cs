using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrdering.Server.Data.Context
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MealOrderingDbContext>
	{
		public MealOrderingDbContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<MealOrderingDbContext>();

			//String connectionString = "Server=(localdb)\\mssqllocaldb;Database=mealordering;Trusted_Connection=True;MultipleActiveResultSets=true";
			//builder.UseSqlServer(connectionString);

			//String connectionString = "postgresql://postgres:example@localhost:5432/mealordering";
			String connectionString = "Host=localhost;Port=5432;Database=mealordering;Username=postgres;Password=example;";
			builder.UseNpgsql(connectionString);


			return new MealOrderingDbContext(builder.Options);
		}
	}
}
