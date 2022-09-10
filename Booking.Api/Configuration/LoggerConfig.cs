using KissLog;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Api.Configuration
{
    public class LoggerConfig
    {
        public void RegisterKissLogListeners(IConfiguration configuration) 
        {
            KissLogConfiguration.Listeners.Add(new RequestLogsApiListener(new Application(
                    configuration["KissLog.OrganizationId"],
                    configuration["KissLog.ApplicationId"])
                )
            {
                ApiUrl = configuration["KissLog.ApiUrl"]
            });
        }
    }
}
