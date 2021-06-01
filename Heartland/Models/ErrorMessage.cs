using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heartland.Models
{
    public class ErrorMessage
    {
        public string resourceKey { get; set; }
        public string errorCode { get; set; }
        public string message { get; set; }
    }
}
