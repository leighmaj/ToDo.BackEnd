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
	public class CommentsController : ApiController
	{

		[HttpPost]
		[Route("api/comments")]
		public IHttpActionResult CreateNewComment(Comment newComment)
		{
			using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
			{
				var commentCollection = db.GetCollection<Comment>("Comments");

				var taskCollection = db.GetCollection<Task>("Tasks");

				var userCollection = db.GetCollection<User>("Users");

				var foundUser = userCollection.FindById(newComment.UserId);

				if (foundUser == null)
				{
					return BadRequest();
				}

				var foundTask = taskCollection.FindById(newComment.TaskId);

				if (foundTask == null)
				{
					return BadRequest();
				}

				newComment.Id = Guid.NewGuid();

				newComment.CreatedDate = DateTime.Now;

				commentCollection.Insert(newComment);

				return Ok(newComment);
			}
		}
	}
}
