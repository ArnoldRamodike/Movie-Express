using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;
using Singular;
using MELib;

namespace MEWeb.Category
{
    public partial class CategoryManager : MEPageBase<CategoryManagerVM>
    {
 
    }
    public class CategoryManagerVM : MEStatelessViewModel<CategoryManagerVM>
    {
       
        public MELib.Categories.CategoryList CategoryList { get; set; }

        public CategoryManagerVM()
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