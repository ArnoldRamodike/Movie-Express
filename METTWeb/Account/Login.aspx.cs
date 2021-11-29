using System.Web;
using Singular.Web;
using Singular.Web.Security;
using Singular.Web.Data;
using System.ComponentModel.DataAnnotations;
using System;
using MELib.Security;
using System.Linq;


namespace MEWeb.Account
{
    public partial class Login: MEPageBase<LoginVM>
  {
  }

  public class LoginVM: MEStatelessViewModel<LoginVM>
  {
    /// <summary>
    /// The login details
    /// </summary>
    public LoginDetails LoginDetails { get; set; }
    public MELib.Security.User EditingUser { get; set; }

        /// <summary>
        /// The location to redirect to after login
        /// </summary>
        public string RedirectLocation { get; set; }
		public bool ShowForgotPasswordInd { get; set; }

		[Display(Name = "Enter your email address", Description = "")]
		public string ForgotEmail { get; set; }
    /// <summary>
    /// Setup the Login ViewModel
    /// </summary>
    protected override void Setup()
    {
      base.Setup();

      this.LoginDetails = new LoginDetails();
			this.ShowForgotPasswordInd = false;
      this.ValidationMode = ValidationMode.OnSubmit;
      this.ValidationDisplayMode = ValidationDisplayMode.Controls;

      this.RedirectLocation = VirtualPathUtility.ToAbsolute(Security.GetSafeRedirectUrl(Page.Request.QueryString["ReturnUrl"], "~/default.aspx"));
    }

		/// <summary>
		/// Check the login details
		/// </summary>
		/// <param name="loginDetails">Login details</param>
		/// <returns>True if the login details are valid</returns>
		[WebCallable(LoggedInOnly = false)]
		public Result Login(LoginDetails loginDetails)
		{
			Result ret = new Result();

			try
			{
				MEIdentity.Login(loginDetails);
				ret.Success = true;
				if(MEWebSecurity.CurrentIdentity().FirstTimeLogin)
				{
					ret.Data = "ChangePassword.aspx";
				}
			}
			catch
			{
				ret.ErrorText = "";
				ret.Success = false;
			}

			return ret;
		}

		[WebCallable(LoggedInOnly = false)]
		public Result ResetPassword(string Email)
		{
			Result ret = new Result();
			try
			{
				MELib.Security.User.ResetPassword(Email);
				ret.Success = true;
			}
			catch (Exception ex)
			{
				ret.Success = false;
				ret.ErrorText = ex.Message;
			}
			return ret;
		}
        // added stuff
        [WebCallable]
        public static MELib.Security.User GetUser(int userId)
        {
            return MELib.Security.UserList.GetUserList(userId).First();
        }

        [WebCallable(Roles = new string[] { "Security.Manage Users" })]
        public static Result SaveUser(MELib.Security.User user)
        {
            if (user.SecurityGroupUserList.Count == 0)
            {
                //add a default security group of General User
                Singular.Security.SecurityGroupUser securityGroupUser = Singular.Security.SecurityGroupUser.NewSecurityGroupUser();
                securityGroupUser.SecurityGroupID = ROSecurityGroupList.GetROSecurityGroupList(true).FirstOrDefault(c => c.SecurityGroup == "General User")?.SecurityGroupID;
                user.SecurityGroupUserList.Add(securityGroupUser);
            }

            user.LoginName = user.EmailAddress;

            Result results = new Singular.Web.Result();
            Result Saveresults = user.SaveUser(user);
            User SavedUser = (User)Saveresults.Data;

            if (SavedUser != null)
            {
                results.Success = true;
                results.Data = SavedUser;
            }
            else
            {
                results.Success = false;
                results.ErrorText = " New User Not Saved Please Try Again !";//Saveresults.ErrorText;
            }
            return results;
        }

        //[Browsable(false)]
        //public SecurityGroupList SecurityGroupList
        //{
        //    get
        //    {
        //        var ROSecurityRoles = MELib.Security.ROSecurityGroupList.GetROSecurityGroupList(true);

        //        var SecurityRoles = SecurityGroupList.GetSecurityGroupList();
        //        var clonedList = SecurityRoles.Clone();

        //        foreach (var item in SecurityRoles)
        //        {
        //            if ((!ROSecurityRoles.Any(c => c.SecurityGroupID == item.SecurityGroupID)) || (item.SecurityGroup == "Administrator"))
        //            {
        //                clonedList.Remove(item);
        //            }
        //        }

        //        SecurityRoles = clonedList;

        //        return SecurityRoles;
        //    }
        //}

    }
}
