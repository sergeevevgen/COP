using LogicDB.BindingModels;
using LogicDB.Models;
using LogicDB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDB.Storages
{
    public class OrderStorage
    {
        public void Delete(OrderBindingModel model)
        {
            using var context = new InternetShopDatabase();
            var element = context.Orders
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new InternetShopDatabase();
            var order = context.Orders
                .FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new InternetShopDatabase();
            return context.Orders
                .Where(rec => rec.Id.Equals(model.Id))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<OrderViewModel> GetFullList()
        {
            using var context = new InternetShopDatabase();
            return context.Orders
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(OrderBindingModel model)
        {
            using var context = new InternetShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Orders.Add(CreateModel(model, new Order()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(OrderBindingModel model)
        {
            using var context = new InternetShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Orders
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.CustomerFIO = model.CustomerFIO;
            order.Image = model.Image;
            order.Product = model.Product;
            order.Mail = model.Mail;
            return order;
        }

        private static OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                CustomerFIO = order.CustomerFIO,
                Image = order.Image,
                Product = order.Product,
                Mail = order.Mail
            };
        }
    }
}
