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
	public class TasksController : ApiController
	{


		[HttpGet]
		public List<Task> GetAllTasks()
		{
			using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
			{
				var taskCollection = db.GetCollection<Task>("Tasks");

				return taskCollection.FindAll().ToList();
			}

		}

		[HttpPost]
		public IHttpActionResult CreateNewTask(Task newTask)
		{
			using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
			{
				var taskCollection = db.GetCollection<Task>("Tasks");

				var userCollection = db.GetCollection<User>("Users");

				var foundUser = userCollection.FindById(newTask.UserId);

				if (foundUser == null)
				{
					return BadRequest();
				}


				newTask.Id = Guid.NewGuid();

				newTask.CreatedDate = DateTime.Now;

				taskCollection.Insert(newTask);

				if (newTask.IsComplete == false)
				{
					newTask.CompleteDate = null;
				}
				else
				{
					newTask.CompleteDate = DateTime.Now;
				}

				return Ok(newTask);
			}
		}

		[HttpDelete]
		[Route("api/tasks/{Id:Guid}")]
		public void DeleteTask(Guid Id)
		{
			using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
			{
				var taskCollection = db.GetCollection<Task>("Tasks");

				taskCollection.Delete(Id);
			}
		}

		[HttpGet]
		[Route("api/tasks/{Id:Guid}")]
		public IHttpActionResult UpdateTask(Guid Id,Guid userId)
		{
			using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
			{
				var taskCollection = db.GetCollection<Task>("Tasks");

				var foundTask = taskCollection.FindById(Id);

				if (foundTask == null)
				{
					return NotFound();
				}
				else
				{
					return Ok(foundTask);
				}
			}
		}

		[HttpPut]
		[Route("api/tasks/{Id:Guid}")]
		public IHttpActionResult UpdateTask(Guid Id, Task updatingTask)
		{
			using (var db = new LiteDatabase(@"C:\ToDo\ToDo.db"))
			{
				var taskCollection = db.GetCollection<Task>("Tasks");

				var foundTask = taskCollection.FindById(Id);

				if (foundTask == null)
				{
					return BadRequest();
				}

				var userCollection = db.GetCollection<User>("Users");

				var foundUser = userCollection.FindById(updatingTask.UserId);

				if (foundUser == null)
				{
					return BadRequest();
				}

				if (!string.IsNullOrWhiteSpace(updatingTask.Title))
				{
					foundTask.Title = updatingTask.Title;
				}

				if (!string.IsNullOrWhiteSpace(updatingTask.TaskBody))
				{
					foundTask.TaskBody = updatingTask.TaskBody;
				}

				if (!string.IsNullOrWhiteSpace(updatingTask.Title))
				{
					foundTask.Title = updatingTask.Title;
				}

				if (updatingTask.UserId != default(Guid))
				{
					foundTask.UserId = updatingTask.UserId;
				}

				if (updatingTask.IsComplete.HasValue)
				{
					foundTask.IsComplete = updatingTask.IsComplete;
				}

				if (updatingTask.IsComplete == false)
				{
					updatingTask.CompleteDate = null;
				}
				else
				{
					updatingTask.CompleteDate = DateTime.Now;
				}



				taskCollection.Update(foundTask);

				return Ok(foundTask);
			}
		}

	}
}
