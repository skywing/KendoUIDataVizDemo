using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Esk.Blog.KendoUI.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Esk.Blog.KendoUI.Services
{
	/// <summary>
	/// SignalR service dashboard hub.
	/// </summary>
	[HubName("serviceDashboard")]
	public class ServiceDashboardHub : Hub
	{
		private readonly ServiceDashboard svcDashboard;

		public ServiceDashboardHub() : this(ServiceDashboard.Instance) { }

		public ServiceDashboardHub(ServiceDashboard svcDashboard)
		{
			this.svcDashboard = svcDashboard;
		}

		/// <summary>
		/// return all services statuses.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ServiceStatus> GetAllServiceStatuses()
		{
			return svcDashboard.GetAllServiceStatuses();
		}
	}
}