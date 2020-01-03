using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LiteDB;
using ToDo.BackEnd.Models;

namespace ToDo.BackEnd.Controllers
{
	public class UsersController : ApiController
	{
		[HttpGet]
		public List<User> GetAllUsers()
		{
			using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
			{
				var userCollection = db.GetCollection<User>("Users");

				return userCollection.FindAll().ToList();
			}

		}

		[HttpPost]
		public User CreateNewUser(User newUser)
		{
			using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
			{
				var userCollection = db.GetCollection<User>("Users");

				newUser.Id = Guid.NewGuid();

				userCollection.Insert(newUser);

				return newUser;
			}
		}

		[HttpDelete]
		[Route("api/users/{userId:Guid}")]
		public void DeleteUser(Guid userId)
		{
			using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
			{
				var userCollection = db.GetCollection<User>("Users");

				userCollection.Delete(userId);

			}
		}

		[HttpGet]
		[Route("api/users/{userId:Guid}")]
		public IHttpActionResult GetSingleUser(Guid userId)
		{
			using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
			{
				var userCollection = db.GetCollection<User>("Users");

				var foundUser = userCollection.FindById(userId);

				if (foundUser == null)
				{
					return NotFound();
				}
				else
				{
					return Ok(foundUser);
				}
			}

		}

		[HttpPut]
		[Route("api/users/{userId:Guid}")]
		public IHttpActionResult UpdateUser(Guid userId, User updatingUser)
		{
			using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
			{
				var userCollection = db.GetCollection<User>("Users");

				var foundUser = userCollection.FindById(userId);

				if (foundUser == null)
				{
					return NotFound();
				}

				if (!string.IsNullOrWhiteSpace(updatingUser.Name))
				{
					foundUser.Name = updatingUser.Name;
				}

				if (!string.IsNullOrWhiteSpace(updatingUser.UserName))
				{
					foundUser.Name = updatingUser.UserName;
				}

				userCollection.Update(foundUser);

				return Ok(foundUser);
			}

		}
	}
}
