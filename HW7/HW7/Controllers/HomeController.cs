using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW7.Models;

namespace HW7.Controllers
{
    public class ListOfRepo
    {
        public string reposName = "";
        public string Owner = "";
        public string HtmlUrl = "";
        public string FullName = "";
        public string OwnerPic = "";
        public string LastModified = "";
    }
    public class ListOfCommits
    {
        public string Sha = "";
        public string TimeStamp = "";
        public string Committer = "";
        public string CommitMessage = "";
        public string HtmlUrl = "";
    }
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult UserProfile()
        {

            string uri = "https://api.github.com/user";
            string myUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["username"];
            string creds = System.Web.Configuration.WebConfigurationManager.AppSettings["credentials"];



            string data1 = SendRequest(uri, creds, myUserName);
            JObject obj = JObject.Parse(data1);

            var data = new
            {
                myPic = (string)obj["avatar_url"],
                fullName = (string)obj["name"],
                html_url = (string)obj["html_url"],
                userName = (string)obj["login"],
                email = (string)obj["email"],
                company = (string)obj["company"],
                Location = (string)obj["location"],

            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult UserRepo()
        {
            
            string myUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["username"]; 
            string creds = System.Web.Configuration.WebConfigurationManager.AppSettings["credentials"];
            string url = "https://api.github.com/user/repos";



            string data1 = SendRequest(url, creds, myUserName);
            JArray jarr = JArray.Parse(data1);

            int count = (jarr).Count();
            Debug.WriteLine(data1);

            List<ListOfRepo> output = new List<ListOfRepo>();
            

            for (int i = 0; i < count; i++)
            {
                
                ListOfRepo listof = new ListOfRepo();
                listof.reposName = (string)jarr[i]["name"];
                listof.Owner = (string)jarr[i]["owner"]["login"];
                listof.HtmlUrl = (string)jarr[i]["html_url"];
                listof.FullName = (string)jarr[i]["full_name"];
                listof.OwnerPic = (string)jarr[i]["owner"]["avatar_url"];
                listof.LastModified = (string)jarr[i]["updated_at"];
                output.Add(listof);
                


            }
            //string jsonString = JsonConvert.SerializeObject(output, Formatting.Indented);

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Commits(string user, string repo)
        {

            string myUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["username"];
            string creds = System.Web.Configuration.WebConfigurationManager.AppSettings["credentials"];
            string url = "https://api.github.com/repos/" + user + "/" + repo +"/commits" ;
            string data1 = SendRequest(url, creds, myUserName);
            JArray jarr = JArray.Parse(data1);

            int count = (jarr).Count();

            List<ListOfCommits> output = new List<ListOfCommits>();

          
            for (int i = 0; i < count; i++)
            {
                ListOfCommits listof = new ListOfCommits();
                listof.Sha = (string)jarr[i]["sha"];
                listof.TimeStamp = (string)jarr[i]["commit"]["committer"]["date"];
                listof.Committer = (string)jarr[i]["commit"]["committer"]["name"];
                listof.CommitMessage = (string)jarr[i]["commit"]["message"];
                listof.HtmlUrl = (string)jarr[i]["html_url"];
                output.Add(listof);



            }


            return Json(output, JsonRequestBehavior.AllowGet);
        }



        private string SendRequest(string uri, string credentials, string username)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Authorization", "token " + credentials);
            request.UserAgent = username;       // Required, see: https://developer.github.com/v3/#user-agent-required
            request.Accept = "application/json";

            string jsonString = null;
            // TODO: You should handle exceptions here
            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            return jsonString;
        }
    }
}