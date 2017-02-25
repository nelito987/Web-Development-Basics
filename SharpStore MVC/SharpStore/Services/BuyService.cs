using System;
using SharpStore.Data;
using SharpStore.BindingModels;
using SharpStore.Models;
using SharpStore.Enums;

namespace SharpStore.Services
{
    public class BuyService : Service
    {
        public BuyService(SharpStoreContext context) : base(context)
        {
        }

        public void AddPurchase(SaleBindingModel model)
        {
            Sale sale = new Sale()
            {
                Name = model.Name,
                Address = model.Address,
                DeliveryType = (DeliveryType) Enum.Parse(typeof(DeliveryType), model.DeliveryType.Trim()),
                PhoneNumber = model.PhoneNumber
            };

            this.context.Sales.Add(sale);
            this.context.SaveChanges();
        }
    }
}
