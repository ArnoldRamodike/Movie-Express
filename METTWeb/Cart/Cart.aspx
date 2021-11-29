<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="MEWeb.Cart.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
  <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
  <link href="../Theme/Singular/METTCustomCss/Maintenance/maintenance.css" rel="stylesheet" />
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
                        var HomeContainerTab = AssessmentsTab.AddTab("Cart");
                        {
                            var Row = HomeContainerTab.Helpers.DivC("row margin0");
                            {
                                var RowCol = Row.Helpers.DivC("col-md-12");
                                {
                                    RowCol.Helpers.HTML().Heading2(" Sub Total = R " + Math.Round(  ViewModel.TotalSum,2));

                                    var CartList = RowCol.Helpers.BootstrapTableFor<MELib.Cart.Cart>((c) => ViewModel.CartList, false, false, "");

                                    {
                                        var MovieListRow = CartList.FirstRow;
                                        {
                                            var MovieTitle = MovieListRow.AddColumn("Product Name");
                                            {
                                                MovieTitle.Style.Width = "500px";
                                                var MovieTitleText = MovieTitle.Helpers.Span(c => c.ProductName);
                                            }
                                            var MovieGenre = MovieListRow.AddColumn("Cart Quantity");
                                            {
                                                MovieGenre.Style.Width = "150px";
                                                // MovieGenre.Style.Display = 1;
                                                var Quantity = MovieGenre.Helpers.EditorFor(c => c.Quantity);
                                            }
                                            var MovieDescription = MovieListRow.AddColumn("Amount ");
                                            {
                                                MovieDescription.Style.Width = "200px";
                                                var MovieDescriptionText = MovieDescription.Helpers.Span(c => "R "+c.CartBalance);
                                            }
                                            var CartAction = MovieListRow.AddColumn("Actions");
                                            {
                                                CartAction.Style.Width = "300px";
                                                var btnDelete = CartAction.Helpers.Button("Delete", Singular.Web.ButtonMainStyle.Danger, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.remove);
                                                {
                                                    // CartAction.Style.Width = "50px";
                                                    btnDelete.AddClass("btn btn-primary btn-outline");
                                                    btnDelete.AddBinding(Singular.Web.KnockoutBindingString.click, "DeleteItem($data)");
                                                }
                                                var UpdateBtn = CartAction.Helpers.Button("Update", Singular.Web.ButtonMainStyle.Success, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.upload);
                                                {
                                                    // CartAction.Style.Width = "50px";
                                                    UpdateBtn.AddClass("btn btn-primary btn-outline");
                                                    UpdateBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "UpdateCart($data)");
                                                }
                                            }



                                            var DropContentDiv = RowCol.Helpers.DivC("col-md-3");
                                            {

                                                DropContentDiv.Helpers.HTML().Heading3("R20 for Delivery");
                                               // DropContentDiv.Helpers.HTML().Heading4("Collection is Free and delivery is 20");
                                                DropContentDiv.Helpers.LabelFor(c => ViewModel.DeliveryID);
                                                var carte = DropContentDiv.Helpers.EditorFor(c => ViewModel.DeliveryID);

                                                DropContentDiv.Helpers.HTMLTag("br");
                                                DropContentDiv.Helpers.HTMLTag("br");
                                              
                                                
                                                //if (ViewModel.DeliveryID == 2)
                                                //{
                                                //    carte.Helpers.BootstrapDialog.ent;
                                                //}


                                            }

                                            var SaveList = RowCol.Helpers.DivC("col-md-12 text-Centre");
                                            {
                                                var btnView = SaveList.Helpers.Button("Empty Cart", Singular.Web.ButtonMainStyle.Danger, Singular.Web.ButtonSize.Large, Singular.Web.FontAwesomeIcon.remove);
                                                {
                                                    btnView.AddClass("btn btn-primary btn-outline");
                                                    btnView.AddBinding(Singular.Web.KnockoutBindingString.click, "DeleteCart($data)");
                                                }

                                                var Purchase = SaveList.Helpers.Button("CheckOut", Singular.Web.ButtonMainStyle.Success, Singular.Web.ButtonSize.Large, Singular.Web.FontAwesomeIcon.shoppingCart);
                                                {
                                                    Purchase.AddClass("btn btn-primary btn-outline");
                                                    Purchase.AddBinding(Singular.Web.KnockoutBindingString.click, "Purchase($data)");
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
        }
    }
  %>

  <script type="text/javascript">
    Singular.OnPageLoad(function () {
      $("#menuItem5").addClass("active");
        $("#menuItem5 > ul").addClass("in");

        	$("#menuItem5ChildItem3").addClass("subActive");

    });

      var Purchase = function (obj) {

          ViewModel.CallServerMethod('Purchase', { TotalSum: obj.TotalSum(), DeliveryID: obj.DeliveryID(),  CartList: ViewModel.CartList.Serialise(), ProductList: ViewModel.ProductList.Serialise(), ShowLoadingBar: true }, function (result) {
              if (result.Success) {
                 
                  MEHelpers.Notification("Successfuly Purchased Product", 'Centre ', 'Success', 50000);
                   window.location = '../Orders/Orders.aspx';
                 
                  
              }
              else {
                  MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                 // window.location = '../Cart/Cart.aspx';
              }
          })
      };

         var DeleteCart = function (obj) {

             ViewModel.CallServerMethod('DeleteCart', { CartList: ViewModel.CartList.Serialise(),  ShowLoadingBar: true }, function (result) {
              if (result.Success) {
                  
                  MEHelpers.Notification("Products Cleared From Cart", 'Centre ', 'Success', 1000);
                   window.location = '../Cart/Cart.aspx';
                  
              }
              else {
                  MEHelpers.Notification(result.ErrorText, 'center', 'warning', 3000);
              }
          })
      };
          var DeleteItem = function (obj) {

             ViewModel.CallServerMethod('DeleteItem', { CartID: obj.CartID(),  ShowLoadingBar: true }, function (result) {
              if (result.Success) {
                  
                  MEHelpers.Notification("Product Removed From Cart", 'Centre ', 'Success', 1000);
                  window.location = '../Cart/Cart.aspx';
                  
              }
              else {
                  MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
              }
          })
      };

      var UpdateCart = function (obj) {
          ViewModel.CallServerMethod('UpdateCart', {ProductID: obj.ProductID(), CartList: ViewModel.CartList.Serialise(), productList: ViewModel.ProductList.Serialise(), ShowLoadingBar: true }, function (result) {
              if (result.Success) {
                 // MEHelpers.Notification("Successfully Saved", 'center', 'success', 3000);
                  alert('Successfully Updated');
                  window.location = '../Cart/Cart.aspx';
              }
              else {
                  MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
              }
          })
      };

      var Cancel = function () {
          window.location = '../Cart/Cart.aspx';
      };



  </script>
</asp:Content>

