using SimpleHttpServer.Models;
using SimpleMVC.App.MVC.Interfaces;
using System.Collections.Generic;
using System;
using System.Net;
using SimpleMVC.App.MVC.Extentions;
using System.Reflection;
using System.Linq;
using SimpleMVC.App.MVC.Attributes.Methods;
using SimpleMVC.App.MVC.Controllers;
using SimpleHttpServer.Enums;

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

        private HttpRequest request;
        private HttpResponse response;

        public ControllerRouter()
        {
            this.getParams = new Dictionary<string, string>();
            this.postParams = new Dictionary<string, string>();
            this.request = new HttpRequest();
            this.response = new HttpResponse();
        }

        public HttpResponse Handle(HttpRequest request)
        {
            this.request = request;
            this.response = new HttpResponse();
            //Parse input from the request
            ParseInput();
            //TODO 
            var method = this.GetMethod();
            var controller = this.GetController();
            IInvokable result = (IInvokable)method.Invoke(controller, this.methodParams);

            string content = result.Invoke();
            var response = new HttpResponse()
            {
                ContentAsUTF8 = content,
                StatusCode = ResponseStatusCode.Ok
            };

            this.ClearRequestParameters();
            return response;
        }

        private void ClearRequestParameters()
        {
            this.postParams = new Dictionary<string, string>();
            this.getParams = new Dictionary<string, string>();
        }

        private void ParseInput()
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

            MethodInfo method = this.GetMethod();
            if (method == null)
            {
                throw new NotSupportedException("No such method");
            }

            IEnumerable<ParameterInfo> parameters = method.GetParameters();
            this.methodParams = new object[parameters.Count()];

            int index = 0;
            foreach (ParameterInfo param in parameters)
            {
                if (param.ParameterType.IsPrimitive)
                {
                    object value = this.getParams[param.Name];
                    this.methodParams[index] = Convert.ChangeType(value, param.ParameterType);
                    index++;
                }
                else if(param.ParameterType == typeof(HttpRequest))
                {
                    this.methodParams[index] = this.request;
                    index++;
                }
                else if(param.ParameterType == typeof(HttpSession))
                {
                    this.methodParams[index] = this.request.Session;
                    index++;
                }
                else if (param.ParameterType == typeof(HttpResponse))
                {
                    this.methodParams[index] = this.response;
                    index++;
                }
                else
                {
                    Type bindingModelType = param.ParameterType;
                    object bindingModel = Activator.CreateInstance(bindingModelType);
                    IEnumerable<PropertyInfo> properties = bindingModelType.GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        property.SetValue(bindingModel, Convert.ChangeType(postParams[property.Name], property.PropertyType));
                    }

                    this.methodParams[index] = Convert.ChangeType(bindingModel, bindingModelType);
                    index++;
                }
            }
        }

        private MethodInfo GetMethod()
        {
            MethodInfo method = null;
            foreach (MethodInfo methodInfo in this.GetSuitableMethods())
            {
                IEnumerable<Attribute> attributes = methodInfo.GetCustomAttributes().Where(a => a is HttpMethodAttribute);
                if (!attributes.Any())
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(this.requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }
            return method;
        }

        private IEnumerable<object> GetSuitableMethods()
        {
            return this.GetController()
                .GetType()
                .GetMethods()
                .Where(m => m.Name == this.actionName);
        }

        private Controller GetController()
        {
            var controllerType = string.Format(
                "{0}.{1}.{2}",
                MvcContext.Current.AssemblyName,
                MvcContext.Current.ControllersFolder,
                this.controllerName);

            var controller =
                (Controller)Activator.CreateInstance(Type.GetType(controllerType));
            return controller;
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
