using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KursAuth.Models.Main
{
    class UserMain
    {
        public string UserName { get; set; }
        private WebRequest _request;
        private string _token;

        public async Task<int> MainAuthAsync(string log, string pass, bool flag)
        {
            if (flag)
            {
                _request = WebRequest.Create("http://saber011-001-site1.htempurl.com/api/Account/login");
            }
            else
            {
                _request = WebRequest.Create("http://saber011-001-site1.htempurl.com/api/Account/register");
            }

            _request.ContentType = "application/json";
            _request.Method = "POST";

            using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
            {
                string json = "{\"login\":\"" + log + "\",\"password\":\"" + pass + "\"}";
                await streamWriter.WriteAsync(json);
            }

            HttpWebResponse response = _request.GetResponse() as HttpWebResponse;
            string resp;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                resp = await reader.ReadToEndAsync();
            }

            JsonResp jResp = JsonConvert.DeserializeObject<JsonResp>(resp);
            var code = jResp.responseInfo.status;
            if (code == 0)
            {
                UserName = jResp.content.username;
                _token = jResp.content.access_token;
                return code;
            }
            else { return code; }
        }
    }
}