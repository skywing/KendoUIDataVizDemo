using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Esk.Blog.KendoUI.Models;

namespace Esk.Blog.KendoUI.Services
{
	/// <summary>
	/// Service dashboard to keep track of current service status.
	/// </summary>
	public class ServiceDashboard
	{
		private readonly static Lazy<ServiceDashboard> instance = 
			new Lazy<ServiceDashboard>(() => new ServiceDashboard(GlobalHost.ConnectionManager.GetHubContext<ServiceDashboardHub>().Clients));
		private readonly object updateServiceStatusLock = new object();
		private readonly ConcurrentDictionary<string, ServiceStatus> svcStatues = new ConcurrentDictionary<string, ServiceStatus>();
		private readonly Timer timer0, timer1, timer2, timer3, timer4, timer5;
		private volatile bool updatingServiceStatuses = false;
		private Random random = new Random(DateTime.Now.Day);
		private ServiceDashboard(IHubConnectionContext clients)
		{
			Clients = clients;
			var serviceStatus = new List<ServiceStatus>
			{
				new ServiceStatus { Title = "Service Type A",  Current = 0, Error = 0 },
				new ServiceStatus { Title = "Service Type B",  Current = 0, Error = 0 },
				new ServiceStatus { Title = "Service Type C",  Current = 0, Error = 0 },
				new ServiceStatus { Title = "Service Type D",  Current = 0, Error = 0 },
				new ServiceStatus { Title = "Service Type E",  Current = 0, Error = 0 },
				new ServiceStatus { Title = "Service Type F",  Current = 0, Error = 0 }
			};
			serviceStatus.ForEach(stat => svcStatues.TryAdd(stat.Title, stat));
			// create timers with random update interval to simulate service status updated in different time.
			timer0 = new Timer(UpdateServiceStatus, serviceStatus[0], TimeSpan.FromMilliseconds(300), TimeSpan.FromMilliseconds(300));
			timer1 = new Timer(UpdateServiceStatus, serviceStatus[1], TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));
			timer2 = new Timer(UpdateServiceStatus, serviceStatus[2], TimeSpan.FromMilliseconds(700), TimeSpan.FromMilliseconds(700));
			timer3 = new Timer(UpdateServiceStatus, serviceStatus[3], TimeSpan.FromMilliseconds(400), TimeSpan.FromMilliseconds(400));
			timer4 = new Timer(UpdateServiceStatus, serviceStatus[4], TimeSpan.FromMilliseconds(800), TimeSpan.FromMilliseconds(800));
			timer5 = new Timer(UpdateServiceStatus, serviceStatus[5], TimeSpan.FromMilliseconds(600), TimeSpan.FromMilliseconds(600));
		}

		#region Private methods
		private IHubConnectionContext Clients { get; set; }

		/// <summary>
		/// Method call by the timer to update the current service status.
		/// </summary>
		/// <param name="state"></param>
		private void UpdateServiceStatus(object state)
		{
			lock(updateServiceStatusLock)
			{
				if (!updatingServiceStatuses)
				{
					updatingServiceStatuses = true;
					//foreach (var status in _svcStatues.Values)
					//{
						if (TryUpdateServiceStatus((ServiceStatus)state))
						{
							BroadcastServiceStatus((ServiceStatus)state);
						}
					//}
					updatingServiceStatuses = false;
				}
			}
		}
 
  
		/// <summary>
		/// Update service status.
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		private bool TryUpdateServiceStatus(ServiceStatus status)
		{
			if (status.Total >= 100)
			{
				status.Current = 0;
				status.Error = 0;
			}
			else
			{
				if (status.Current > 0 && status.Current % 10 == 0)
				{
					status.Error += random.Next(1, 5);
				}
				if (status.Total < 100)
					status.Current += 1;
			}
			return true;
		}

		/// <summary>
		/// Broadchast the service status back to all clients.
		/// </summary>
		/// <param name="status"></param>
		private void BroadcastServiceStatus(ServiceStatus status)
		{
			Clients.All.updateServiceStatus(status);
		}

		#endregion Private methods

		#region Public methods
		public static ServiceDashboard Instance { get { return instance.Value;  } }

		public IEnumerable<ServiceStatus> GetAllServiceStatuses() { return svcStatues.Values; }
		#endregion Public methods

	}
}