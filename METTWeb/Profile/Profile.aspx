<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="MEWeb.Profile.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Add page specific styles and JavaScript classes below -->
    <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
    <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
    <!-- Placeholder not used in this example -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%
        using (var h = this.Helpers)
        {
            var MainContent = h.DivC("row pad-top-10");
            {
                var MainContainer = MainContent.Helpers.DivC("col-md-12 p-n-lr");
                {
                    var PageContainer = MainContainer.Helpers.DivC("tabs-container");
                    {
                        var PageTab = PageContainer.Helpers.TabControl();
                        {
                            PageTab.Style.ClearBoth();
                            PageTab.AddClass("nav nav-tabs");
                            var ContainerTab = PageTab.AddTab("Account Information");
                            {
                                var RowContentDiv = ContainerTab.Helpers.DivC("row margin0");
                                {

                                    #region Left Column / Data
                                    var LeftColRight = RowContentDiv.Helpers.DivC("col-md-8");
                                    {

                                        var AnotherCardDiv = LeftColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                        {
                                            var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                            {
                                                CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                                CardTitleDiv.Helpers.HTML().Heading5("Profile");
                                            }
                                            var CardTitleToolsDiv = CardTitleDiv.Helpers.DivC("ibox-tools");
                                            {
                                                var aToolsTag = CardTitleToolsDiv.Helpers.HTMLTag("a");
                                                aToolsTag.AddClass("collapse-link");
                                                {
                                                    var iToolsTag = aToolsTag.Helpers.HTMLTag("i");
                                                    iToolsTag.AddClass("fa fa-chevron-up");
                                                }
                                            }
                                            var ContentDiv = AnotherCardDiv.Helpers.DivC("ibox-content");
                                            {
                                                var RowContentDiv2 = ContentDiv.Helpers.DivC("row");
                                                {
                                                    var ColContentDiv = RowContentDiv2.Helpers.DivC("col-md-12");
                                                    {

                                                        var ProfileList = ColContentDiv.Helpers.BootstrapTableFor<MELib.RO.ROUser>((c) => ViewModel.UsersList, false, false, "");
                                                        {
                                                            var ProfileListRow = ProfileList.FirstRow;
                                                           
                                                            {
                                                                var MovieTitle = ProfileListRow.AddColumn("User ID");
                                                                {
                                                                    MovieTitle.Style.Width = "200px";
                                                                    var MovieTitleText = MovieTitle.Helpers.Span(c => c.UserID);
                                                                   MovieTitleText.Style.FontSize = "15px";
                                                                }
                                                                var MovieGenre = ProfileListRow.AddColumn("Full Name");
                                                                {
                                                                    var MovieGenreText = MovieGenre.Helpers.Span(c => c.FirstName);
                                                                    MovieGenreText.Style.FontSize = "15px";
                                                                }
                                                                var LastName = ProfileListRow.AddColumn("Last Name");
                                                                {
                                                                    var last = LastName.Helpers.Span(c => c.LastName);
                                                                    last.Style.FontSize = "15px";
                                                                }
                                                                var MovieDescription = ProfileListRow.AddColumn("Email Address");
                                                                {
                                                                    var MovieDescriptionText = MovieDescription.Helpers.Span(c => c.EmailAddress);
                                                                    MovieDescriptionText.Style.FontSize = "15px";
                                                                }
                                                                var JobDescription = ProfileListRow.AddColumn("User Name");
                                                                {
                                                                    var JobDescriptionText = JobDescription.Helpers.Span(c => c.UserName);
                                                                    JobDescriptionText.Style.FontSize = "15px";
                                                                }

                                                                //var Actions = ProfileListRow.AddColumn("Actions");
                                                                //{
                                                                //    Actions.Style.Width = "75px";
                                                                //    // Add Buttons
                                                                //    var btnView = Actions.Helpers.Button("Edit", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                                //    {
                                                                //        btnView.AddClass("btn btn-primary btn-outline");
                                                                //        btnView.AddBinding(Singular.Web.KnockoutBindingString.click, "GenerateInterventionRpt($data)");
                                                                //    }
                                                                //    //var btnDelete = Actions.Helpers.Button("Save", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                                //    //{
                                                                //    //    btnDelete.AddClass("btn btn-primary btn-outline");
                                                                //    //    btnDelete.AddBinding(Singular.Web.KnockoutBindingString.click, "GenerateInterventionRpt($data)");
                                                                //    //}
                                                                //}
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Right Column / Filters
                                    var RowColRight = RowContentDiv.Helpers.DivC("col-md-4");
                                    {

                                        var AnotherCardDiv = RowColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                        {
                                            var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                            {
                                                CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                                CardTitleDiv.Helpers.HTML().Heading5("Account Information");
                                            }
                                            var CardTitleToolsDiv = CardTitleDiv.Helpers.DivC("ibox-tools");
                                            {
                                                var aToolsTag = CardTitleToolsDiv.Helpers.HTMLTag("a");
                                                aToolsTag.AddClass("collapse-link");
                                                {
                                                    var iToolsTag = aToolsTag.Helpers.HTMLTag("i");
                                                    iToolsTag.AddClass("fa fa-chevron-up");
                                                }
                                            }
                                            var ContentDiv = AnotherCardDiv.Helpers.DivC("ibox-content");
                                            {
                                                var LeftRowContentDiv = ContentDiv.Helpers.DivC("row");
                                                {
                                                    var LeftColContentDiv = LeftRowContentDiv.Helpers.DivC("col-md-12 text-center");
                                                    {

                                                        var ProfileDiv = LeftColContentDiv.Helpers.With<MELib.Accounts.Account>(c => c.UserAccount);
                                                        {

                                                            //   var Profile = ProfileDiv.Helpers.HTML("<div class='circlecenter'><div class='circlecontaineruser circlecenter'><span class='fa fa-user fa-lg fa-fw' style='font-size:64px;'></span></div></div>");

                                                            var AccountNo = ProfileDiv.Helpers.Span(c => "Account NO : " + c.AccountID);
                                                            AccountNo.Style.FontSize = "20px";
                                                            ProfileDiv.Helpers.HTMLTag("br");
                                                            var Balance = ProfileDiv.Helpers.Span(c => "Balance      : R " + c.Balance);
                                                            Balance.Style.FontSize = "20px";
                                                            ProfileDiv.Helpers.HTMLTag("br");
                                                            var UserName = ProfileDiv.Helpers.Span(c => "User Name   : " + ViewModel.LoggedInUserName);
                                                            UserName.Style.FontSize = "20px";
                                                            ProfileDiv.Helpers.HTMLTag("br");
                                                            var accType = ProfileDiv.Helpers.Span(c => "Account Type : " + c.AccountTypeName);
                                                            ProfileDiv.Helpers.HTMLTag("br");
                                                            accType.Style.FontSize = "20px";
                                                        }
                                                    }
                                                    var RightColContentDiv = LeftRowContentDiv.Helpers.DivC("col-md-12 text-center");
                                                    {
                                                        // Fund Account Button
                                                        var FundAccountBtn = RightColContentDiv.Helpers.Button("Deposit Funds", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                        {
                                                            FundAccountBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "DepositFunds()");
                                                            FundAccountBtn.AddClass("btn btn-primary btn-outline");
                                                        }
                                                        // Edit Profile
                                                        //var EditProfileBtn = RightColContentDiv.Helpers.Button("Edit Profile", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                        //{
                                                        //    EditProfileBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "EditProfile()");
                                                        //    EditProfileBtn.AddClass("btn btn-primary btn-outline");
                                                        //}
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                }
            }
        }
    %>
    <script type="text/javascript">
        // Place page specific JavaScript here or in a JS file and include in the HeadContent section
        Singular.OnPageLoad(function () {
            $("#menuItem1").addClass('active');
            $("#menuItem1 > ul").addClass('in');
        });

        var DepositFunds = function () {
            window.location = '../Profile/DepositFunds.aspx';
        };

    </script>
</asp:Content>
