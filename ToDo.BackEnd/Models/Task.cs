using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo.BackEnd.Models
{
	public class Task
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string Title { get; set; }
		public string TaskBody { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool? IsComplete { get; set; }
		public DateTime? CompleteDate { get; set; }
	}
}