#r "Newtonsoft.Json"

using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Collections.Generic;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ILogger log)
{
    var statusMsg = "@channel \n";
    
    //Push message through Slack
    using (var client = new HttpClient()) 
    { 
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("User-Agent", "AzureFunctions");  

        List<Attachment> attachmentList = new List<Attachment>();
        Attachment attachment = new Attachment()
        {
            color = "#ff0000", 
            text = String.Format("404 code occurred in your web app !"), 
            ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds.ToString(),
            fallback = "404 code occurred in your web app !"
        };

        attachmentList.Add(attachment);

        var uri = "slackurl goes here";
        var msg = new SlackHook {text = statusMsg, parse = "full", attachments = attachmentList };
        
        StringContent SlackMsg = new StringContent(JsonConvert.SerializeObject(msg));
        HttpResponseMessage response = client.PostAsync(uri, SlackMsg).Result; 
        var responseString = response.Content.ReadAsStringAsync().Result;

        return response;
    }
}

public class SlackHook
{
    public string text { get; set; }
    public List<Attachment> attachments { get; set; }
    public string parse { get;set; }
}

public class Attachment
{
    public string color { get; set; }
    public string text { get; set; }
    public string ts { get; set; }
    public string fallback { get; set; }
}