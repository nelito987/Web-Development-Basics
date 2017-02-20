using SimpleHttpServer;
using System;
using System.IO;
using System.Reflection;

namespace SimpleMVC.App.MVC
{
    public static class MvcEngine
    {
        public static void Run(HttpServer server)
        {
            RegisterAssemblyName();
            RegisterControllers();
            RegisterViews();
            RegisterModels();

            try
            {
                server.Listen();
            }
            catch(Exception e)
            {
                File.WriteAllText("log.txt", e.Message + Environment.NewLine);
                Console.WriteLine(e.Message);
            }
        }

        private static void RegisterModels()
        {
            MvcContext.Current.ModelsFolder = "Models";
        }

        private static void RegisterViews()
        {
            MvcContext.Current.ViewsFolder = "Views";            
        }

        private static void RegisterControllers()
        {
            MvcContext.Current.ControllersFolder = "Controllers";
            MvcContext.Current.ControllersSuffix = "Controller";
        }

        private static void RegisterAssemblyName()
        {
            MvcContext.Current.AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        }
    }
}
