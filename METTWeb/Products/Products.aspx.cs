using System;
using Singular.Web;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.UI;
using MELib.RO;
using MELib.Security;
using System.Web.UI.WebControls;
using Singular;
using MELib.Categories;
using MELib.Cart;
using MELib.Products;
//using MELib.Users;
using System.Linq;

namespace MEWeb.Products
{
    public partial class Products : MEPageBase<ProductsVM>
    {

    }
    public class ProductsVM : MEStatelessViewModel<ProductsVM>
    {

        public ProductList ProductList { get; set; }
        public CartList CartList { get; set; }
        public MELib.Security.UserList UserList { get; set; }

        //filter Criteria
        public DateTime Created { get; set; }
        public string ProductName { get; set; }

        [Singular.DataAnnotations.DropDownWeb(typeof(CategoryList), UnselectedText = "Select", ValueMember = "CategoryID", DisplayMember = "CategoryName")]
        [Display(Name = "Cartegory")]
        public int? CategoryID { get; set; }
        public int? ProductID { get; set; }
        public ProductList Select { get; set; }
        public int QuantityList { get; set; }
        public decimal TotalSum { get; set; }
        public decimal TotalProducts { get; set; }
        public int? counP { get; set; }
        public ProductsVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            ProductList = ProductList.GetProductList(null,true);
            CartList = CartList.GetCartList();
          
            TotalSum = MELib.Cart.CartList.GetCartList().Sum(x => x.CartBalance);
           // counP = CartList.GetCartList().Select(x => x.ProductID).FirstOrDefault();
            // TotalProducts = CartList.GetCartList().Count(x => x.ProductID);
          // QuantityList =  ProductList.FirstOrDefault().Quantity;

          MELib.Products.ProductList.GetProductList(null);

        }

        [WebCallable]
        public Result AddToCart(int productID, ProductList productList)
        {
            Result sr = new Result();

           try
           {
                if (productID != 0)
                {
                    MELib.Cart.Cart cart = new MELib.Cart.Cart();
                    CartList temp = new CartList();

                    Product product = productList.GetItem(productID);

                    var ProductCount = MELib.Products.ProductList.GetProductList(null);
                    

                    cart.ProductID = (int)productID;
                    cart.IsActiveInd = true;
                    cart.UserID = Settings.CurrentUser.UserID;
                    cart.Quantity = product.UserQuantity;
                    product.UserQuantity = 0;
                    //product.Quantity = QuantityList;

                    var count = cart.ProductID.Value;
                    

                    if ( cart.Quantity >= 1 )
                    {

                        if (product.Quantity >= 1)
                        {
                           
                            if (product.Quantity >= cart.Quantity)
                            {
                                cart.CartBalance = product.Price * cart.Quantity;
                            }
                            else
                            {
                                sr.ErrorText = "Sorry Only  " + product.Quantity.ToString() + " " + "Left In Stock";
                                // temp.TrySave();
                                sr.Success = false;
                                return sr;
                            }
                        }

                        else
                        {
                            sr.ErrorText = "Sorry Out of Stock....Try Other Products.";
                            // temp.TrySave();
                            sr.Success = false;
                            return sr;
                        }

                        // product.Quantity = product.Quantity - cart.Quantity;

                        product.TrySave(typeof(MELib.Products.ProductList));
                       temp.Add(cart);

                        if (temp.IsValid)
                        {

                            var SaveResult = temp.TrySave();
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
                        }
                        else
                        {
                            sr.ErrorText = "Oops! Something Went Wrong.Try Again";
                            sr.Success = false;
                        }
                    }
                    else
                    {
                        sr.ErrorText = "Enter Product Quantity ...!";
                       // temp.TrySave();
                        sr.Success = false;
                    }


                }

            }
            catch (Exception e)
            {
                sr.ErrorText = "Can't add to Basket!";
                sr.Success = false;
             
            }
            return sr;
         }
    
        [WebCallable]
        public Result FilterProducts(int CategoryID, int ResetInd)
        {
            Result sr = new Result();
            try
            {
                if (ResetInd == 0)
                {
                    MELib.Products.ProductList ProductList = MELib.Products.ProductList.GetProductList(CategoryID,true);
                    sr.Data = ProductList;
                }
                else
                {
                    MELib.Products.ProductList ProductList = MELib.Products.ProductList.GetProductList(null,true);
                    sr.Data = ProductList;
                }
                sr.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: Products.aspx | Method: FilterProducts", $"(int CategoryId, ({CategoryID})");
                sr.Data = e.InnerException;
                sr.ErrorText = "Could not filter Products by category.";
                sr.Success = false;
            }
            return sr;
        }
    }
}

