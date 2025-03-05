using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Services.Infrastructure;
using MealOrdering.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Services.Services
{
	public class UserService : IUserService
	{
		private readonly IMapper mapper;
		private readonly MealOrderingDbContext context;

		public UserService(IMapper Mapper,  MealOrderingDbContext Context)
		{
			mapper = Mapper;
			context = Context;
		}
        public async Task<UserDTO> CreateUser(UserDTO user)
		{
			var dbUser = await context.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();

			if (dbUser != null)
				throw new Exception("User ID already exists.");

			dbUser = mapper.Map<Server.Data.Models.Users>(user);

			await context.Users.AddAsync(dbUser);
			int result = await context.SaveChangesAsync();

			return mapper.Map<UserDTO>(dbUser);
		}

		public async Task<bool> DeleteUserById(Guid id)
		{
			var dbUser = await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

			if (dbUser == null)
				throw new Exception("User not found.");

			context.Users.Remove(dbUser);
			int result = await context.SaveChangesAsync();

			return result > 0;
		}

		public async Task<UserDTO> GetUserById(Guid id)
		{
			return await context.Users
							.Where(x => x.Id == id)
							.ProjectTo<UserDTO>(mapper.ConfigurationProvider)
							.FirstOrDefaultAsync();
		}

		public async Task<List<UserDTO>> GetUsers()
		{
			return await context.Users
							.Where(x => x.IsActive)
							.ProjectTo<UserDTO>(mapper.ConfigurationProvider)
							.ToListAsync();
		}

		public async Task<UserDTO> UpdateUser(UserDTO user)
		{
			var dbUser = await context.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();

			if (dbUser == null)
				throw new Exception("User not found.");

			// Detects only changed columns to update on databae
			mapper.Map(user, dbUser);

			int result = await context.SaveChangesAsync();

			return mapper.Map<UserDTO>(dbUser);
		}
	}
}
