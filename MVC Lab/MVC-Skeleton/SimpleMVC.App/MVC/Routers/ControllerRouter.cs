using SimpleHttpServer.Models;
using SimpleMVC.App.MVC.Interfaces;
using System.Collections.Generic;
using System;
using System.Net;
using SimpleMVC.App.MVC.Extentions;

namespace SimpleMVC.App.MVC.Routers
{
    public class ControllerRouter : IHandleable
    {
        private IDictionary<string, string> getParams;
        private IDictionary<string, string> postParams;
        private string requestMethod;
        private string controllerName;
        private string actionName;
        private object[] methodParams;
        private string[] controllerActionParams;
        private string[] controllerAction;
        public HttpResponse Handle(HttpRequest request)
        {
            //Parse input from the request
            ParseInput(request);
        }

        private void ParseInput(HttpRequest request)
        {
            string uri = WebUtility.UrlDecode(request.Url);
            string query = string.Empty;
            if (request.Url.Contains("?"))
            {
                query = request.Url.Split('?')[1];
            }
            this.controllerActionParams = uri.Split('?');
            this.controllerAction = controllerActionParams[0].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            this.controllerActionParams = query.Split('&');

            if(controllerActionParams.Length >= 1)
            {
                RetrieveGetParameters(controllerActionParams);
            }

            string postParameters = request.Content;
            if(postParameters != null)
            {
                RetrievePostParams(postParameters);
            }

            this.requestMethod = request.Method.ToString();
            this.controllerName = this.controllerAction[this.controllerAction.Length - 2].ToUpperFirst() + MvcContext.Current.ControllersSuffix;
            this.actionName = this.controllerAction[this.controllerAction.Length - 1].ToUpperFirst();
        }

        private IDictionary<string, string> RetrievePostParams(string postParameters)
        {
            postParameters = WebUtility.UrlDecode(postParameters);
            string[] paramPairs = postParameters.Split('&');
            foreach (var pair in paramPairs)
            {                
                 string[] keyValue = pair.Split('=');
                 this.postParams.Add(keyValue[0], keyValue[1]);                
            }
            return postParams;
        }

        private IDictionary<string, string> RetrieveGetParameters(string[] controllerActionParams)
        {
            foreach (var param in controllerActionParams)
            {
                if (param.Contains("="))
                {
                    string[] keyValue = param.Split('=');
                    this.getParams.Add(keyValue[0], keyValue[1]);
                }
            }
            return this.getParams;
        }
    }
}
