using Newtonsoft.Json;
using StepChat.Common.CommonModels;
using StepChat.Common.ViewModels;
using StepChat.Server.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace StepChat.Server
{
    public class Programm
    { 
  
        static void Main(string[] args)
        {
            
           Task.Run(new Action(() =>
           {
               ChatHTTP_Server.StartServer();
           }));

            WebServer.StartServer();
           
          
        }
    }
}
