using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Lufuno.Utilities
{
    public class Logger : Interfaces.ILogger
    {
        public Logger()
        {
            SetupLog();
        }

        private void SetupLog()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("MachineName", System.Environment.MachineName)
                .Enrich.WithProperty("SystemUser", System.Environment.MachineName)
                .Enrich.WithProperty("RuntimeVersion", System.Environment.Version)
                .CreateLogger();
        }
        public void LogCriticalError(Exception ex)
        {
            LogEvent(ex, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "", EventLogEntryType.Error);
        }

        public void LogCriticalError(Exception ex, string applicationName)
        {
            LogEvent(ex, applicationName, "", EventLogEntryType.Error);
        }

        public void LogWarning(Exception ex)
        {
            LogEvent(ex, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, string.Empty, EventLogEntryType.Warning);
        }

        public void LogWarning(Exception ex, string applicationName)
        {
            LogEvent(ex, applicationName, string.Empty, EventLogEntryType.Warning);
        }

        public void LogInfo(string payload, string message, params object[] args)
        {
            try
            {
                string formatedMessage = (args != null && args.Any())
              ? string.Format(message, args)
              : message;

                Exception exx = new Exception(formatedMessage);

                LogEvent(exx, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, payload, EventLogEntryType.Information);
            }
            catch (Exception)
            {
                //suppress wrong number of args
            }

        }

        public void LogEvent(Exception ex, string applicationName, string extraDetails, EventLogEntryType eventLogEntryType = EventLogEntryType.Error)
        {
            //try log to serilog
            try
            {                 
                switch (eventLogEntryType)
                {
                    case EventLogEntryType.Error:
                        SetupLog();
                        Log.Error(ex, "{Message} {Assembly} {BaseExceptionSource} {BaseExceptionMessage} {@AdditionalDetail}",
                            ex.Message,
                            applicationName,
                            ex.GetBaseException().Source,
                            ex.GetBaseException().Message,
                            extraDetails);
                        Log.CloseAndFlush();
                        break;
                    case EventLogEntryType.Warning:
                        SetupLog();
                        Log.Warning(ex, "{Message} {Assembly} {BaseExceptionSource} {BaseExceptionMessage} {@AdditionalDetail}",
                            ex.Message,
                            applicationName,
                            ex.GetBaseException().Source,
                            ex.GetBaseException().Message,
                            extraDetails);
                        Log.CloseAndFlush();
                        break;
                    case EventLogEntryType.Information:
                    case EventLogEntryType.SuccessAudit:
                    case EventLogEntryType.FailureAudit:
                        SetupLog();
                        Log.Information(ex, "{Message} {Assembly} {BaseExceptionSource} {BaseExceptionMessage} {@AdditionalDetail}",
                            ex.Message,
                            applicationName,
                            ex.GetBaseException().Source,
                            ex.GetBaseException().Message,
                            extraDetails);
                        Log.CloseAndFlush();
                        break;
                    default:
                        SetupLog();
                        Log.Error(ex, "{Message} {Assembly} {BaseExceptionSource} {BaseExceptionMessage} {@AdditionalDetail}",
                            ex.Message,
                            applicationName,
                            ex.GetBaseException().Source,
                            ex.GetBaseException().Message,
                            extraDetails);
                        Log.CloseAndFlush();
                        break;
                }
            }
            catch (Exception exB)
            {
                var path = ConfigurationManager.AppSettings["LogPath"].ToString();

                Log.Logger = new LoggerConfiguration()

                    .WriteTo.RollingFile(path + "log -{Date}.txt",
                        retainedFileCountLimit: 100,
                        fileSizeLimitBytes: 20971520,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                    .CreateLogger();
            }
            //Log the trace 
            System.Diagnostics.Trace.TraceError(GetEmailBody(ex, ""));
        }

        private static string GetEmailBody(Exception ex, string extraDetails)
        {
            StringBuilder body = new StringBuilder();
            body.AppendFormat("Assembly: {0}\n", ex.GetBaseException().Source);
            body.AppendFormat("Machine Name: {0}\n", System.Environment.MachineName);
            body.AppendFormat("System user: {0}\n", System.Environment.UserDomainName);
            body.AppendFormat("Runtime Version: {0}\n\n", System.Environment.Version);
            body.Append(ex.ToString());
            body.Append("Additional Detail:\n\n");
            body.Append(extraDetails);
            return body.ToString();
        }

        private string FormattedString(string message, params object[] args)
        {
            if (args != null && args.Any())
            {
                return string.Format(message, args);
            }
            else
            {
                return message;
            }
        }
    }
}