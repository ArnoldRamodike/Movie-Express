using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MELib.Orders;
using Singular.Web;

namespace MEWeb.Orders
{
    public partial class Orders : MEPageBase<OrdersVM>
    {

    }
    public class OrdersVM : MEStatelessViewModel<OrdersVM>
    {
        public OrderList Orders { get; set; }

        public OrdersVM()
        {

        }

        protected override void Setup()
        {
            base.Setup();
            Orders = OrderList.GetOrderList();
        }

       
        public Result Order(int OrderID)
        {
            Result sr = new Result();
            try
            {
                if (OrderID != 0)
                {
                    Orders = OrderList.GetOrderList(OrderID);
                    var temp = Orders.Single(x => x.OrderID == OrderID);
                    temp.IsActiveInd = false;
                    Orders.Add(temp);

                    Orders.TrySave();
                    sr.Success = true;
                }
            }

            catch (Exception e)
            {
                sr.Data = e.InnerException;
                sr.ErrorText = "The Item has not Arrived";
                sr.Success = true;

            }
            return sr;
        }

    }

}