using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace MyHeartbeatService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
          var exitCode = HostFactory.Run(x=>
          {
              x.Service<Heartbeat>(s =>
              {
                  s.ConstructUsing(heartbeat => new Heartbeat());
                  s.WhenStarted(heartbeat => heartbeat.Start());
                  s.WhenStopped(heartbeat => heartbeat.Stop());
              });

              x.RunAsLocalSystem();

              x.SetServiceName("HeartbeatService");

              x.SetDisplayName("Heartbeat Service");

              x.SetDescription("Am I alive?");

            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
