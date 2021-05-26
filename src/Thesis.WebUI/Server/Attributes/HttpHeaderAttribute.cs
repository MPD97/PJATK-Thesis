using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thesis.WebUI.Server.Attributes
{
    public class HttpHeaderAttribute : Attribute, IActionConstraint
    {
        public string Header { get; set; }
        public string Value { get; set; }

        public HttpHeaderAttribute(string header, string value)
        {
            Header = header;
            Value = value;
        }

        public bool Accept(ActionConstraintContext context)
        {
            if (context.RouteContext.HttpContext.Request.Headers.TryGetValue(Header, out var value))
            {
                return value[0] == Value;
            }

            return false;
        }

        public int Order => 0;
    }
}
