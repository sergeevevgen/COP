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
    public class ProductStorage
    {
        public void Delete(ProductBindingModel model)
        {
            using var context = new InternetShopDatabase();
            var element = context.Products
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Products.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ProductViewModel GetElement(ProductBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new InternetShopDatabase();
            var element = context.Products
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return element != null ? CreateModel(element) : null;
        }

        public List<ProductViewModel> GetFilteredList(ProductBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new InternetShopDatabase();
            return context.Products
                .Where(rec => rec.Name == model.Name)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<ProductViewModel> GetFullList()
        {
            using var context = new InternetShopDatabase();
            return context.Products
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(ProductBindingModel model)
        {
            using var context = new InternetShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Products.Add(CreateModel(model, new Product()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(ProductBindingModel model)
        {
            using var context = new InternetShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Products
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

        private static Product CreateModel(ProductBindingModel model, Product product)
        {
            product.Name = model.Name;
            return product;
        }

        private static ProductViewModel CreateModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name
            };
        }
    }
}
