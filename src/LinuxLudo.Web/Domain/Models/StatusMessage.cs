using System;
using System.Collections.Generic;
using System.Linq;

namespace LinuxLudo.Web.Domain.Models
{
    public class StatusMessage
    {
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime EndTime { get; set; }
        public int statusMessageUpdateIntervalMs = 750;

        public StatusMessage(string message) => Message = message;
        public StatusMessage(string message, List<StatusMessage> messages)
        {
            Message = message;
            CreatedTime = DateTime.Now;

            // Sets the end time to the selected interval ms into the future
            if (messages.Count > 0)
            {
                EndTime = messages.LastOrDefault().CreatedTime.AddMilliseconds(statusMessageUpdateIntervalMs);
            }
            else
            {
                EndTime = DateTime.Now.AddMilliseconds(statusMessageUpdateIntervalMs);
            }
        }
    }
}