<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="MEWeb.Profile.Transactions" %>

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
                          var ContainerTab = PageTab.AddTab("Transaction History");
                          {
                              var RowContentDiv = ContainerTab.Helpers.DivC("row");
                              {

                                  #region Left Column / Data
                                  var LeftColRight = RowContentDiv.Helpers.DivC("col-md-9");
                                  {

                                      var AnotherCardDiv = LeftColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                      {
                                          var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                          {
                                              CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                              CardTitleDiv.Helpers.HTML().Heading5("Transactions");
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
                                                  var ColNoContentDiv = RowContentDiv2.Helpers.DivC("col-md-12 text-center");
                                                  {
                                                      ColNoContentDiv.AddBinding(Singular.Web.KnockoutBindingString.visible, c => ViewModel.Transactions.Count() == 0);
                                                      ColNoContentDiv.Helpers.HTML("<p>Could not find any Product based on your filter criteria.</p>");
                                                  }
                                                  var ColContentDiv = RowContentDiv2.Helpers.DivC("col-md-12");
                                                  {
                                                      var ProductSection = ColContentDiv.Helpers.BootstrapTableFor<MELib.Transactions.Transaction>((c) => ViewModel.Transactions, false, false, "");
                                                      {

                                                          var ProductListRow = ProductSection.FirstRow;
                                                          {
                                                              var TransactionID = ProductListRow.AddColumn("Transcaction ID");
                                                              {
                                                                  var TransID = TransactionID.Helpers.Span(c => c.TransactionID);
                                                              }

                                                              var ProductTitle = ProductListRow.AddColumn("Transaction Type");
                                                              {
                                                                  var productNameText = ProductTitle.Helpers.Span(c => c.TransactionTypeName);
                                                              }
                                                              var ProductDescription = ProductListRow.AddColumn("Description");
                                                              {
                                                                  var ProductDescriptionText = ProductDescription.Helpers.Span(c => c.Description);
                                                              }

                                                              var ProductPrice = ProductListRow.AddColumn("Price");
                                                              {
                                                                  var Price = ProductPrice.Helpers.Span(c => "R " + c.Amount);
                                                              }
                                                          }
                                                          var SaveList = RowContentDiv2.Helpers.DivC("col-md-12 text-Left");
                                                          {
                                                              var btnView = SaveList.Helpers.Button("Home", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.shoppingCart);
                                                              {
                                                                  btnView.AddClass("btn btn-primary btn-outline");
                                                                  btnView.AddBinding(Singular.Web.KnockoutBindingString.click, "Home($data)");
                                                              }
                                                          }
                                                      }
                                                  }
                                              }
                                          }
                                      }
                                  }
                                  #endregion

                                  #region Right Column / Filters
                                  var RowColRight = RowContentDiv.Helpers.DivC("col-md-3");
                                  {

                                      var AnotherCardDiv = RowColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                      {
                                          var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                          {
                                              CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                              CardTitleDiv.Helpers.HTML().Heading5("Filter Criteria");
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
                                                  var MovieGenreContentDiv = RowContentDiv2.Helpers.DivC("col-md-12");
                                                  {

                                                      MovieGenreContentDiv.Helpers.LabelFor(c => ViewModel.TransactionTypeID);
                                                      var ReleaseFromDateEditor = MovieGenreContentDiv.Helpers.EditorFor(c => ViewModel.TransactionTypeID);
                                                      ReleaseFromDateEditor.AddClass("form-control marginBottom20 ");

                                                      var FilterBtn = MovieGenreContentDiv.Helpers.Button("Filter", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.filter);
                                                      {
                                                          FilterBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterTransations($data)");
                                                          FilterBtn.AddClass("btn btn-primary btn-outline");
                                                      }
                                                      var ResetBtn = MovieGenreContentDiv.Helpers.Button("Reset", Singular.Web.ButtonMainStyle.Default, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.circle);
                                                      {
                                                          ResetBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterReset($data)");
                                                          ResetBtn.AddClass("btn btn-primary btn-outline");
                                                      }

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

      var Home = function ()
      {
          window.location = '../Account/Home.aspx';
      };

          var FilterTransations = function (obj) {
      // alert('Movie Genre ID ' + obj.MovieGenreID());

        ViewModel.CallServerMethod('FilterTransations', { TransactionTypeID: obj.TransactionTypeID(), ResetInd: 0, ShowLoadingBar: true }, function (result) {
            if (result.Success) {
            
            MEHelpers.Notification("Products filtered successfully.", 'center', 'info', 10000);
                ViewModel.ProductList.Set(result.Data);
               
        }
        else {
          MEHelpers.Notification(result.ErrorText, 'center', 'warning', 10000);
        }
      })
      };

    var FilterReset = function (obj) {
      ViewModel.CallServerMethod('FilterTransations', { TransactionTypeID: obj.TransactionTypeID(), ResetInd: 1, ShowLoadingBar: true }, function (result) {
        if (result.Success) {
          MEHelpers.Notification("Products reset successfully.", 'center', 'info', 1000);
          ViewModel.ProductList.Set(result.Data);
          // Set Drop Down to 'Select'
        }
        else {
          MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
        }
      })
    };

   

  </script>
</asp:Content>
