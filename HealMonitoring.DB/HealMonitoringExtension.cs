using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealMonitoring.DB
{
    public static class HealMonitoringExtension
	{
		public static void AddHealMonitoringStore<TContext>(this IServiceCollection services)
		where TContext : DbContext
		{
			Type storeType = typeof(TContext);

			//services.TryAddScoped<InformationManager>();
		}
	}
}
