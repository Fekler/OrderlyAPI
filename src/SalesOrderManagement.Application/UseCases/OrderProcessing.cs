using SalesOrderManagement.Application.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderManagement.Application.UseCases
{
    public class OrderProcessing
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly IProductBusiness _productBusiness;
        private readonly IOrderItemBusiness _orderItemBusiness;
    }
}
