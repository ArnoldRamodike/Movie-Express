<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProducts.aspx.cs" Inherits="MEWeb.Products.AddProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript" src="../Scripts/JSLINQ.js"></script>
  <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
  <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <%
      using (var h = this.Helpers)
      {

          var MainHDiv = h.DivC("row pad-top-10");
          {
              var PanelContainer = MainHDiv.Helpers.DivC("col-md-12 p-n-lr");
              {
                  var HomeContainer = PanelContainer.Helpers.DivC("tabs-container");
                  {
                      var AssessmentsTab = HomeContainer.Helpers.TabControl();
                      {
                          AssessmentsTab.Style.ClearBoth();
                          AssessmentsTab.AddClass("nav nav-tabs");
                          var HomeContainerTab = AssessmentsTab.AddTab("Products Table");
                          {
                              var Row = HomeContainerTab.Helpers.DivC("row margin0");
                              {
                                  var RowCol = Row.Helpers.DivC("col-md-12");
                                  {
                                      RowCol.Helpers.HTML().Heading2("Products Management");



                                      var ProductList = RowCol.Helpers.TableFor<MELib.Products.Product>((c) => c.ProductList, true, true);
                                      {
                                          ProductList.AddClass("table table-striped table-bordered table-hover");
                                          var ProductListRow = ProductList.FirstRow;
                                          {
                                              var ProductName = ProductListRow.AddColumn(c => c.ProductName);
                                              {
                                                  ProductName.Style.Width = "300px";
                                              }
                                              var ProductDescription = ProductListRow.AddColumn(c => c.ProductDescription);
                                              {
                                                  ProductDescription.Style.Width = "500px";
                                              }
                                              var ProductPrice = ProductListRow.AddColumn(c => c.Price);
                                              {
                                                  ProductPrice.Style.Width = "80px";
                                              }
                                            
                                              var CartegoryID = ProductListRow.AddColumn(c => c.CartegoryID);

                                              {
                                                  CartegoryID.Style.Width = "175px";

                                              }
                                              var ProductQuantity = ProductListRow.AddColumn(c => c.Quantity);
                                              {
                                                  ProductQuantity.Style.Width = "50px";
                                              }

                                              var ProductActive = ProductListRow.AddColumn(c => c.IsActiveInd);
                                              {
                                                  ProductActive.Style.Width = "75px";

                                              }

                                          }
                                      }

                                      var SaveList = RowCol.Helpers.DivC("col-md-12 text-right");
                                      {
                                          var SaveBtn = SaveList.Helpers.Button("Save", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.save);
                                          {
                                              SaveBtn.AddClass("btn-primary btn btn btn-primary");
                                              SaveBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "SaveProduct($data)");
                                          }
                                      }

                                  }
                              }
                          }
                      }
                  }
              }
          }
      }

  %>

  <script type="text/javascript">
    Singular.OnPageLoad(function () {
      $("#menuItem2").addClass("active");
      $("#menuItem2 > ul").addClass("in");
    });

      var SaveProduct = function (obj) {
          ViewModel.CallServerMethod('SaveProductList', { ProductList: ViewModel.ProductList.Serialise(), ShowLoadingBar: true }, function (result) {
        if (result.Success) {
          MEHelpers.Notification("Successfully Saved", 'center', 'success', 3000);
            window.location = '../Products/AddProducts.aspx';
        }
        else {
          MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
        }
      });
    }

  </script>
</asp:Content>
