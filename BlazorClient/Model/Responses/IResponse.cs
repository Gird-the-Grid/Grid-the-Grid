using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Model.Responses
{
    interface IResponse
    {
        bool Success { get; set; }
    }
}
