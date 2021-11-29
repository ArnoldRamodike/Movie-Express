using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.UI;
using System.Web.UI.WebControls;
using Singular;
using Singular.Web;
using MELib.Security;
using MELib;

namespace MEWeb.Category
{
    public partial class ManageCartegory : MEPageBase<ManageCartegoryVM>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

    public class ManageCartegoryVM : MEStatelessViewModel<ManageCartegoryVM>
    {
       
        public MELib.Categories.CategoryList CategoryList { get; set; }

        public ManageCartegoryVM()
        {

        }

        protected override void Setup()
        {
            base.Setup();
            CategoryList = MELib.Categories.CategoryList.GetCategoryList();
        }
        
        [WebCallable]
        public Result SaveCategoryList(MELib.Categories.CategoryList CategoryList)
        {
            Result sr = new Result();
            if (CategoryList.IsValid)
            {
                var SaveResult = CategoryList.TrySave();
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
                sr.ErrorText = CategoryList.GetErrorsAsHTMLString();
                return sr;
            }
        }
    }
}