using ManualHttpServer.Core;
using ManualHttpServer.Core.Attributes;
using Microsoft.VisualBasic;
using System;
using System.Net;
using System.Text;

namespace ManualHttpServer.Resources.Pages
{
    [ResourceRoute("home")]

    public class IndexPageResource : BaseResourceProvider
    {
        public override void Process(
            HttpListenerRequest request,
            HttpListenerResponse response)
        {
            // Console.WriteLine(CurrentAccount.Email);
            
            var currentMoment = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");

            var page = "<!DOCTYPE html><html><head><style>table{font-family: arial, sans-serif; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}tr:nth-child(even){background-color: #dddddd;}</style></head><body><h2>HTML Table DATE_TIME_INSERT</h2><table> <tr> <th>Company</th> <th>Contact</th> <th>Country</th> </tr><tr> <td>Alfreds Futterkiste</td><td>Maria Anders</td><td>Germany</td></tr><tr> <td>Centro comercial Moctezuma</td><td>Francisco Chang</td><td>Mexico</td></tr><tr> <td>Ernst Handel</td><td>Roland Mendel</td><td>Austria</td></tr><tr> <td>Island Trading</td><td>Helen Bennett</td><td>UK</td></tr><tr> <td>Laughing Bacchus Winecellars</td><td>Yoshi Tannamuri</td><td>Canada</td></tr><tr> <td>Magazzini Alimentari Riuniti</td><td>Giovanni Rovelli</td><td>Italy</td></tr></table></body></html>";
            page = page.Replace("DATE_TIME_INSERT", currentMoment);

            var pageBytes = Encoding.UTF8.GetBytes(page);
            response.ContentType = "text/html";
            response.StatusCode = 200;
            response.OutputStream.Write(pageBytes, 0, pageBytes.Length);
            response.Close();
        }
    }
}
