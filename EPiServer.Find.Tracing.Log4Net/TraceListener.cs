﻿using System.Text;
using System.Web;
using log4net;

namespace EPiServer.Find.Tracing.Log4Net
{
    public class TraceListener : ITraceListener
    {
        ILog log = LogManager.GetLogger("EPiServer.Find");

        public void Add(ITraceEvent traceEvent)
        {
            var message = new StringBuilder();
            if (HttpContext.Current != null)
            {
                message.Append("During HTTP request to: ");
                message.Append(HttpContext.Current.Request.Url);
                message.AppendLine();
            }

            message.Append("Instance ");
            message.Append(traceEvent.Source.TraceId);
            message.Append(" of type ");
            message.Append(traceEvent.Source.GetType());
            message.Append(" reported: ");
            message.Append(traceEvent.Message);
            message.AppendLine();

            if (traceEvent.IsError)
            {
                log.Error(message.ToString());
            }
            else
            {
                log.Debug(message.ToString());
            }
        }
    }
}
