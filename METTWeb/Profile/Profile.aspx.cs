using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;
using MELib.RO;
using MELib.Accounts;

namespace MEWeb.Profile
{
  public partial class Profile : MEPageBase<ProfileVM>
  {

  }
  public class ProfileVM : MEStatelessViewModel<ProfileVM>
  {
        public MELib.Accounts.AccountList AccountList { get; set; }   
        public MELib.RO.ROUserList UsersList { get; set; }
        public MELib.Accounts.Account UserAccount { get; set; }
        public int UserName { get; set; }
        public string LoggedInUserName { get; set; }

   public ProfileVM()
    {

    }
        protected override void Setup()
        {
            base.Setup();
            AccountList = MELib.Accounts.AccountList.GetAccountList();
            UsersList = ROUserList.GetROUserList();
            UserAccount = AccountList.FirstOrDefault();

            // userName = UsersList.FirstOrDefault().UserName;

            LoggedInUserName = Singular.Security.Security.CurrentIdentity.UserName;
        }
  }
}

