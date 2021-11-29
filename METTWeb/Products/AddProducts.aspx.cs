using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MELib.Security;
using Singular.Web;

namespace MEWeb.Products
{
    public partial class AddProducts : MEPageBase<AddProductsVM>
    {
       
    }


    public class AddProductsVM : MEStatelessViewModel<AddProductsVM>
    {
        public MELib.Products.ProductList ProductList { get; set; }
        public MELib.Categories.CategoryList Categories { get; set; }

        public AddProductsVM()
        {

        }

        protected override void Setup()
        {
           base.Setup();
            ProductList = MELib.Products.ProductList.GetProductList( );
            Categories = MELib.Categories.CategoryList.GetCategoryList(true);
        }

        [WebCallable]
        public Result SaveProductList(MELib.Products.ProductList ProductList)
        {
            Result sr = new Result();
            if (ProductList.IsValid)
            {
                var SaveResult = ProductList.TrySave();
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
    }
}