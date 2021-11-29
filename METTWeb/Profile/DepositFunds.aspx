<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepositFunds.aspx.cs" Inherits="MEWeb.Profile.DepositFunds" Debug="true" %>

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
                          var ContainerTab = PageTab.AddTab("Deposit");
                          {
                              var RowContentDiv = ContainerTab.Helpers.DivC("row");
                              {

                                  #region Left Column / Data
                                  var LeftColRight = RowContentDiv.Helpers.DivC("col-md-4");
                                  {

                                  }
                                  #endregion

                                  #region Deposit Column / Filters
                                  var MiddleColRight = RowContentDiv.Helpers.DivC("col-md-4");
                                  {
                                      MiddleColRight.Helpers.HTML().Heading2(" Current Balance = R " + ViewModel.Money);
                                      var AnotherCardDiv = MiddleColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                      {
                                          var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                          {
                                              CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                              CardTitleDiv.Helpers.HTML().Heading5("Deposit Funds");
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
                                          var CategoryList = AnotherCardDiv.Helpers.TableFor<MELib.Accounts.Account>((c) => c.DepositAccount, false, false);
                                          {
                                              CategoryList.AddClass("table table-striped table-bordered table-hover");
                                              var DeposittRow = CategoryList.FirstRow;
                                              {
                                                  var BalanceDisplay = CategoryList.Helpers.Span("Deposit ...Funds");

                                                  var CategoryActive = DeposittRow.AddColumn(c => c.Balance);
                                                  {
                                                     
                                                      CategoryActive.Style.Width = "500px";
                                                      CategoryActive.Style.FontSize = "40px";
                                                  }
                                              }
                                          }

                                          var SaveList = AnotherCardDiv.Helpers.DivC("col-md-12 text-right");
                                          {
                                              var SaveBtn = SaveList.Helpers.Button("Deposit", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.money);
                                              {
                                                  SaveBtn.AddClass("btn-primary btn btn btn-primary");
                                                  SaveBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "DepositFunds($data)");
                                              }
                                              var Cancel = SaveList.Helpers.Button("Cancel", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.home);
                                              {
                                                  Cancel.AddClass(" btn-primary btn btn btn-primary");
                                                  Cancel.AddBinding(Singular.Web.KnockoutBindingString.click, "Cancel($data)");
                                              }
                                          }
                                      }
                                  }



                                  #endregion
                                  #region Right Column / Data
                                  var RowColRight = RowContentDiv.Helpers.DivC("col-md-4");
                                  {

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
//const { data } = require("jquery");

    // Place page specific JavaScript here or in a JS file and include in the HeadContent section
    Singular.OnPageLoad(function () {
      $("#menuItem1").addClass('active');
      $("#menuItem1 > ul").addClass('in');
    });

      var DepositFunds = function (data) {
          ViewModel.CallServerMethod('DepositFunds', { Account: data.DepositAccount.Serialise(), ShowLoadingBar: true }, function (result) {
              if (result.Success) {
                 // Singular.AddMessage(3, 'Save', 'Saved Successfully.').Fade(2000);
                  alert('Money Deposited Successfully');
                  window.location = "../Profile/DepositFunds.aspx";
                  
              }
              else {
                  Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                  // MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
              }
          });
      }
      var Cancel = function () {
          window.location = '../Account/Home.aspx';
      };

  </script>
</asp:Content>
