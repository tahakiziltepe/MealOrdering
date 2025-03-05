﻿using MealOrdering.Services.Infrastructure;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealOrdering.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService userService;

		public UserController(IUserService userService)
		{
			this.userService = userService;
		}

		[HttpGet("Users")]
		public async Task<ServiceResponse<List<UserDTO>>> GetUsers()
		{
			return new ServiceResponse<List<UserDTO>>
			{
				Value = await userService.GetUsers()
			};
		}
	}
}
