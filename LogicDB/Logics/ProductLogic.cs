using LogicDB.BindingModels;
using LogicDB.Storages;
using LogicDB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDB.Logics
{
    public class ProductLogic
    {
        private readonly ProductStorage _storage;

        public ProductLogic()
        {
            _storage = new ProductStorage();
        }

        public void CreateOrUpdate(ProductBindingModel model)
        {
            var element = _storage.GetElement(new ProductBindingModel
            {
                Name = model.Name
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть товар с таким названием");
            }

            if (model.Id.HasValue)
            {
                _storage.Update(model);
            }
            else
            {
                _storage.Insert(model);
            }
        }

        public void Delete(ProductBindingModel model)
        {
            var element = _storage.GetElement(new ProductBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _storage.Delete(model);
        }

        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            if (model == null)
            {
                return _storage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<ProductViewModel>
                {
                    _storage.GetElement(model)
                };
            }

            return _storage.GetFilteredList(model);
        }
    }
}
