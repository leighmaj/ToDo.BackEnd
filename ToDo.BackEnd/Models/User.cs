using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo.BackEnd.Models
{
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string UserName { get; set; }
		public DateTime CreatedDate { get; set; }

	}
}