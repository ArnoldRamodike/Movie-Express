using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;
using Singular;
using MELib.Accounts;
using MELib.Transactions;

namespace MEWeb.Profile
{
  public partial class DepositFunds : MEPageBase<DepositFundsVM>
  {
  }
    public class DepositFundsVM : MEStatelessViewModel<DepositFundsVM>
    {
        public MELib.Accounts.AccountList DepositAccount { get; set; }
        public MELib.Accounts.Account Deposit { get; set; }
        public MELib.Transactions.TransactionList Transaction { get; set; }
        public int? AccountID { get; set; }

        public decimal Money { get; set; }

        public DepositFundsVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            DepositAccount = MELib.Accounts.AccountList.GetAccountList();
            Deposit = DepositAccount.FirstOrDefault();

            Transaction = TransactionList.GetTransactionList();
            Money = Deposit.Balance; //MELib.Accounts.AccountList.GetAccountList().FirstOrDefault().Balance;

        }
        [WebCallable]
        public Result DepositFunds(AccountList Account)
        {
            Result sr = new Result();
            var NewBalance = MELib.Accounts.AccountList.GetAccountList(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();
            NewBalance.UserID = Singular.Security.Security.CurrentIdentity.UserID;
            NewBalance.Balance += Account.FirstOrDefault().Balance;
            NewBalance.TrySave(typeof(AccountList));

            var bal = NewBalance.Balance;

            MELib.Transactions.Transaction transaction = new MELib.Transactions.Transaction();
            TransactionList temp = new TransactionList();
            transaction.UserID = Settings.CurrentUser.UserID;

            decimal Balance = Account.FirstOrDefault().Balance;
            string BalanceString = Account.FirstOrDefault().Balance.ToString();
            bool parseSuccess = decimal.TryParse(BalanceString, out Balance);


            if (Account.FirstOrDefault().Balance > 0 && parseSuccess)
            {
                transaction.Amount = Account.FirstOrDefault().Balance;
                transaction.TransactionTypeID = 2;
                transaction.Description = "Deposit Funds";
                transaction.TrySave(typeof(TransactionList));
                sr.Success = true;
                return new Singular.Web.Result() { Success = true };
            }
            else
            {
                sr.Success = false;
                sr.ErrorText = "Enter Numeric Values Only ..!!";
                return sr;
            }
        }


    }
}

