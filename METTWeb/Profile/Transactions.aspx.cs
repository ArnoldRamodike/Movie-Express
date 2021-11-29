using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;
using MELib.Transactions;
using System.ComponentModel.DataAnnotations;

namespace MEWeb.Profile
{
  public partial class Transactions : MEPageBase<TransactionsVM>
  {
  }
    public class TransactionsVM : MEStatelessViewModel<TransactionsVM>
    {
        public MELib.Transactions.TransactionList Transactions { get; set; }
        public MELib.Transactions.TransactionTypeList TransactionTypeList { get; set; }


        [Singular.DataAnnotations.DropDownWeb(typeof(TransactionTypeList), UnselectedText = "Select", ValueMember = "TransactionTypeID", DisplayMember = "TransactionTypeName")]
        [Display(Name = "Transactions")]

        public int? TransactionTypeID  { set; get; }


   public TransactionsVM()
    {

    }
    protected override void Setup()
    {
      base.Setup();
      Transactions = MELib.Transactions.TransactionList.GetTransactionList();
    }

        [WebCallable]
        public Result FilterTransations(int TransactionTypeID, int ResetInd)
        {
            Result sr = new Result();
            try
            {
                if (ResetInd == 0)
                {
                    MELib.Transactions.TransactionList Transactions = MELib.Transactions.TransactionList.GetTransactionList(TransactionTypeID);
                    sr.Data = Transactions;
                }
                else
                {
                    MELib.Transactions.TransactionList Transactions = MELib.Transactions.TransactionList.GetTransactionList(null);
                    sr.Data = Transactions;
                }
                sr.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: Products.aspx | Method: FilterProducts", $"(int CategoryId, ({TransactionTypeID})");
                sr.Data = e.InnerException;
                sr.ErrorText = "Could not filter Products by category.";
                sr.Success = false;
            }
            return sr;
        }


    }
}

