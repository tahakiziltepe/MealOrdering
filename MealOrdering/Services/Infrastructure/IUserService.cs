using MealOrdering.Shared.DTO;

namespace MealOrdering.Services.Infrastructure
{
	public interface IUserService
	{
		public Task<UserDTO> GetUserById(Guid id);
		public Task<List<UserDTO>> GetUsers();
		public Task<UserDTO> CreateUser(UserDTO user);
		public Task<UserDTO> UpdateUser(UserDTO user);
		public Task<bool> DeleteUserById(Guid id);



	}
}
