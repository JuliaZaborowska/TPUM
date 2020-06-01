﻿using System.Net;
using Newtonsoft.Json;

namespace WebsocketServer.Resolver
{
    public class Message
    {
        public string Action { get; set; }
        public string Params { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
