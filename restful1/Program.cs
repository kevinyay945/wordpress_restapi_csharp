using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restful1
{
    class Program
    {
        static void Main(string[] args)
        {
            var web_address = "http://127.0.0.1/testTable/ver2/";
            var user = "admin";
            var password = "admin";
            var boundary = "----WebKitFormBoundary7MA4YWxkTrZu0gW";
            var article_title = "output_table_2";
            var article_content = "[mytable]\n\nyear,lose,Model,Length\n\n1997,Ford,E350,2.34\n\n2000,Mercury,Cougar,2.38\n\n[/mytable]";
            var article_status = "publish";
            var client = new RestClient(web_address+"wp-json/wp/v2/posts") { Authenticator = new HttpBasicAuthenticator(user, password) };
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("content-type", "multipart/form-data; boundary="+ boundary);
            request.AddParameter("multipart/form-data; boundary="+boundary+"", "--"+boundary+"\r\nContent-Disposition: form-data; name=\"title\"\r\n\r\n"+ article_title + "\r\n--"+boundary+"\r\nContent-Disposition: form-data; name=\"content\"\r\n\r\n"+article_content+"\r\n--"+boundary+"\r\nContent-Disposition: form-data; name=\"status\"\r\n\r\n"+article_status+"\r\n--"+boundary+"--", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            dynamic content_decode = JValue.Parse(response.Content);
            Console.WriteLine(content_decode.guid.rendered);
            Console.ReadLine();
        }
    }
}
