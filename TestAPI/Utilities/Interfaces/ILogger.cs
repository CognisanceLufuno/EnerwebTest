using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lufuno.Utilities.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Log a critical message, with the relevant arguments.
        /// </summary>
        /// <param name="ex">The Exception.</param>        
        void LogCriticalError(Exception ex);

        /// <summary>
        /// Log a critical message, with the relevant arguments.
        /// </summary>
        /// <param name="ex">The Exception.</param>
        /// <param name="applicationName">The Name of the application where the exception occured.</param>
        void LogCriticalError(Exception ex, string applicationName);

        /// <summary>
        /// Log a warning message, with the relevant arguments.
        /// </summary>
        /// <param name="ex">The Exception.</param>        
        void LogWarning(Exception ex);

        /// <summary>
        /// Log an information message, with the relevant arguments.
        /// </summary>
        /// <param name="payload">The Payload</param>
        /// <param name="message">The format message.</param>
        /// <param name="args">The set of arguments.</param>
        void LogInfo(string payload, string message, params object[] args);

        /// <summary>
        /// Log an Event, with the relevant arguments.
        /// </summary>
        /// <param name="ex">The Exception.</param>  
        /// <param name="applicationName">The Name of the application where the exception occured.</param>
        /// <param name="extraDetails">The Extra details of the event.</param>
        /// <param name="eventLogEntryType">The type of Log Entry</param>
        /// <param name="message">The format message.</param>        
        void LogEvent(Exception ex, string applicationName, string extraDetails, System.Diagnostics.EventLogEntryType eventLogEntryType);
    }
}
