using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo.BackEnd.Models
{
	public class Comment
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid TaskId { get; set; }
		public string CommentBody { get; set; }
		public DateTime CreatedDate { get; set; }
	}
	
}