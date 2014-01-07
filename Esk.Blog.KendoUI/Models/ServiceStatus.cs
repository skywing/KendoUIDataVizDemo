using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esk.Blog.KendoUI.Models
{
	/// <summary>
	/// Service status model.
	/// </summary>
	public class ServiceStatus
	{
		public ServiceStatus()
		{
			this.Id = Guid.NewGuid().ToString();
		}
		public string Id { get; set; }
		public string Title { get; set; }
		public int Current { get; set; }
		public int Error { get; set; }

		public int Total
		{
			get { return Current + Error; }
		}
	}
}