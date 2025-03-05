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
			//String connectionString = "postgresql://postgres:example@localhost:5432/mealordering";
			String connectionString = "Server=(localdb)\\mssqllocaldb;Database=mealordering;Trusted_Connection=True;MultipleActiveResultSets=true";

			var builder = new DbContextOptionsBuilder<MealOrderingDbContext>();
			builder.UseSqlServer(connectionString);


			//builder.UseNpgsql(connectionString);


			return new MealOrderingDbContext(builder.Options);
		}
	}
}
