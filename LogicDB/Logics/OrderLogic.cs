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
    public class OrderLogic
    {
        private readonly OrderStorage _orderStorage;

        public OrderLogic()
        {
            _orderStorage = new OrderStorage();
        }

        public void CreateOrUpdate(OrderBindingModel model)
        {
            var element = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.Id
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть заказ с таким id");
            }

            if (model.Id.HasValue)
            {
                _orderStorage.Update(model);
            }
            else
            {
                _orderStorage.Insert(model);
            }
        }

        public void Delete(OrderBindingModel model)
        {
            var element = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _orderStorage.Delete(model);
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<OrderViewModel>()
                {
                    _orderStorage.GetElement(model)
                };
            }
            return _orderStorage.GetFilteredList(model);
        }
    }
}
