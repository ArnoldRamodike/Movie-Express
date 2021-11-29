using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MELib;
using MELib.Cart;
using MELib.Products;
using MELib.Accounts;
using Singular.Web;
using Singular;
using System.ComponentModel.DataAnnotations;
using MELib.Transactions;
using MELib.Orders;


namespace MEWeb.Cart
{
    public partial class Cart : MEPageBase<CartVM>
    {

    }
 public class CartVM: MEStatelessViewModel<CartVM>
    {
        public CartList CartList { get; set; }
        public ProductList ProductList { get; set; }
        public AccountList AccounList { get; set; }
        public MELib.Accounts.Account Buy { get; set; }
        public MELib.Transactions.TransactionList Transactions { set; get; }
        public MELib.Orders.OrderList Orders { set; get; }
        public decimal TotalSum { get; set; }
        public decimal UserBalance { get; set; }
        public decimal  Walletlist { get; set; }
        public int Quantity { get; set; }
       // public int ProductID { get; set; }
        //public int CartID { get; set; }

        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.Orders.DeliveyList), UnselectedText ="Delivey Type", ValueMember = "DeliveryID", DisplayMember = "DeliveryType")]
        [Display(Name = "Delivery")]

        public int? DeliveryID { get; set; }
       // int? ProductID { get; set; }
        

        public CartVM()
        {
        }

        protected override void Setup()
        {
            base.Setup();
            CartList = CartList.GetCartList();
            ProductList = ProductList.GetProductList();
            AccounList = AccountList.GetAccountList();
            Transactions = TransactionList.GetTransactionList();
            Orders = OrderList.GetOrderList();

            Buy = AccounList.FirstOrDefault();
            TotalSum = CartList.GetCartList().Sum(x => x.CartBalance);

            Walletlist = CartList.GetCartList().Sum(x => x.Quantity);

        }

        [WebCallable]
        public Result Purchase( decimal TotalSum , int DeliveryID, CartList CartList,  ProductList ProductList )
        {
            Result sr = new Result();

            try
            {
               
                UserBalance = MELib.Accounts.AccountList.GetAccountList().Select(x => x.Balance).FirstOrDefault();


                //Check If Cart is Not Empty.
                if (CartList.Count == 0)
                {
                    sr.Success = false;
                    sr.ErrorText = "The Cart is Empty Go to products!";
                    return sr;
                }

                if (DeliveryID == 2)
                {

                    TotalSum = TotalSum + 20;
                }

                if (UserBalance >= TotalSum)
                {
                    // Delivery Options
                    if (DeliveryID == 0)
                    {
                        sr.Success = false;
                        sr.ErrorText = "Please Select Delivery Options!";
                        return sr;

                    }
                   

                    foreach (var cart in CartList)
                    {

                        foreach (var product in ProductList)
                        {
                            if (cart.ProductID == product.ProductID)
                            {
                                if (product.Quantity >= cart.Quantity)
                                {
                                    product.Quantity = product.Quantity - cart.Quantity;

                                    Order Orders = new Order();
                                    OrderList temps = new OrderList();

                                    Orders.UserID = Settings.CurrentUser.UserID;
                                    Orders.CartID = cart.CartID;   //CartList.GetCartList().Select(x => x.CartID).FirstOrDefault();
                                    Orders.Amount = cart.CartBalance;
                                    Orders.IsActiveInd = true;
                                    Orders.DeliveryID = DeliveryID;
                                    Orders.ProductName = cart.ProductName;
                                    Orders.ProductQuantity = cart.Quantity;
                                    temps.Add(Orders);
                                    temps.TrySave();
                                }
                                else
                                {
                                    sr.Success = false;
                                    sr.ErrorText = "Quantity is Greater than Products in Stock..! only: " + product.Quantity.ToString() +" "+ " Left in Stock !";
                                    return sr;
                                }

                            }
                        }
                    }
                    //Save Cart
                    CartList.ToList().ForEach(c => { c.IsActiveInd = false; });

                    CartList.TrySave();
                    ProductList.TrySave();


                    //Balance Reduced
                    var BalanceCheckout = UserBalance - TotalSum;
                    AccounList = AccountList.GetAccountList();
                    AccounList.ToList().ForEach(f => f.Balance = BalanceCheckout);
                    AccounList.TrySave();

                    //Oder Confirmation
                  
                    

                    //Transactions List
                    Transaction transaction = new Transaction();

                    transaction.UserID = Settings.CurrentUser.UserID;
                    transaction.Amount = TotalSum;
                    transaction.TransactionTypeID = 1;
                    transaction.Description = "Purchase";
                    transaction.TrySave(typeof(TransactionList));

                    sr.Success = true;
                }

                else
                {
                    sr.Success = false;
                    sr.ErrorText = "Insufficiant Funds Please Deposit  Money into your Account ..! ";
                }
               
            }
            catch (Exception e)
            {
                sr.Data = e.InnerException;
                sr.Success = false;
              
            }
           
            return sr;
        }

        [WebCallable]
        public Result DeleteCart(CartList CartList)
        {
            Result sr = new Result();
            try
            {

                CartList = MELib.Cart.CartList.GetCartList();
                CartList.Clear();
                CartList.TrySave();
                sr.Success = true;

            }

            catch (Exception e)
            {
                sr.Data = e.InnerException;
                sr.Success = true;

            }
            return sr;
        }
        [WebCallable]
        public Result UpdateCart(int ProductID, CartList CartList, ProductList productList)
        {
            Result sr = new Result();

           // Update Cart Quantity.
            
         
            Product product = productList.GetItem(ProductID);
  
            var Cart = CartList.Single(x => x.ProductID == ProductID);

            Cart.CartBalance = product.Price * Cart.Quantity;

            if (Cart.Quantity > product.Quantity)
            {
                sr.Success = false;
                sr.ErrorText = "Sorry only "+product.Quantity.ToString()  + " left In Stock";
                return sr;
            }

            if (CartList.IsValid)
            {
                var SaveResult = CartList.TrySave();
                if (SaveResult.Success)
                {
                    sr.Data = SaveResult.SavedObject;
                    sr.Success = true;
                }
                else
                {
                    sr.ErrorText = SaveResult.ErrorText;
                    sr.Success = false;
                }
                return sr;
            }
            else
            {
                sr.ErrorText = ProductList.GetErrorsAsHTMLString();
                return sr;
            }
        }
        public Result DeleteItem(int CartID)
        {
            Result sr = new Result();
            try
            {
                if (CartID != 0)
                {
                    CartList = CartList.GetCartList(CartID);
                    var temp = CartList.Single(x => x.CartID == CartID);
                    CartList.Remove(temp);

                    CartList.TrySave();
                    sr.Success = true;
                }
            }

            catch (Exception e)
            {
                sr.Data = e.InnerException;
                sr.Success = true;

            }
            return sr;
        }

    }
}