using SharpStore.BindingModels;
using SharpStore.Models;
using SharpStore.Services;
using SharpStore.ViewModels;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;
using System.Collections.Generic;

namespace SharpStore.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult<IEnumerable<ProductsViewModel>> Products(string Name)
        {
            ProductsService service = new ProductsService(Data.Data.Context);
            IEnumerable<ProductsViewModel> viewModels = service.GetProducts(Name);
            return this.View(viewModels);
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contacts(MessageBinding messageBindingModel)
        {
            if(string.IsNullOrEmpty(messageBindingModel.Email) == string.IsNullOrEmpty(messageBindingModel.Subject))
            {
                this.Redirect(new HttpResponse()
                {
                }, "/home/contacts");
            }

            MessagesService service = new MessagesService(Data.Data.Context);
            service.AddMessageFrombind(messageBindingModel);
            return this.View("Home", "Index");
        }

        [HttpGet]
        public IActionResult Buy()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Buy(SaleBindingModel model)
        {
            BuyService service = new BuyService(Data.Data.Context);
            service.AddPurchase(model);

            return this.View("Home", "Index");
        }
    }
}
