<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="MEWeb.Orders.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
  <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <%
      using (var h = this.Helpers)
      {
          var MainContent = h.DivC("row pad-top-10");
          {
              var MainContainer = MainContent.Helpers.DivC("col-md-12 p-n-lr ");
              {
                  var PageContainer = MainContainer.Helpers.DivC("tabs-container");
                  {
                      var PageTab = PageContainer.Helpers.TabControl();
                      {
                          PageTab.Style.ClearBoth();
                          PageTab.AddClass("nav nav-tabs");
                          var ContainerTab = PageTab.AddTab("Orders History");
                          {
                              var RowContentDiv = ContainerTab.Helpers.DivC("row");
                              {

                                  #region Left Column / Data
                                  var LeftColRight = RowContentDiv.Helpers.DivC("col-md-12 text-Centre ");
                                  {

                                      var AnotherCardDiv = LeftColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                      {
                                          var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                          {
                                              CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                              CardTitleDiv.Helpers.HTML().Heading5("Orders List");
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
                                                      ColNoContentDiv.AddBinding(Singular.Web.KnockoutBindingString.visible, c => ViewModel.Orders.Count() == 0);
                                                      ColNoContentDiv.Helpers.HTML("<p> No Orders, Go to Products Section </p>");
                                                  }
                                                  var ColContentDiv = RowContentDiv2.Helpers.DivC("col-md-12 text-center");
                                                  {
                                                      var OrderSection = ColContentDiv.Helpers.BootstrapTableFor<MELib.Orders.Order>((c) => ViewModel.Orders , false, false, "");
                                                      {

                                                          var OrderListRow = OrderSection.FirstRow;
                                                          {
                                                              var OrderTitle = OrderListRow.AddColumn("Order ID");
                                                              {
                                                                  var order = OrderTitle.Helpers.Span(c => c.OrderID);
                                                              }
                                                              var ProductName = OrderListRow.AddColumn("Product Name");
                                                              {
                                                                  var Product = ProductName.Helpers.Span(c => c.ProductName);
                                                              }
                                                              var PQuantity = OrderListRow.AddColumn("Quantity");
                                                              {
                                                                  var Quantity = PQuantity.Helpers.Span(c => c.ProductQuantity);
                                                              }
                                                              var OrderDescription = OrderListRow.AddColumn("Delivery Options");
                                                              {
                                                                  var OrderDescriptionText = OrderDescription.Helpers.Span(c => c.DeliveryType);
                                                              }

                                                              var OrderPrice = OrderListRow.AddColumn("Total Amount");
                                                              {
                                                                  var Price = OrderPrice.Helpers.Span(c => "R " + c.Amount);
                                                              }
                                                              // var OrderDate = OrderListRow.AddColumn("Order Date");
                                                              //{
                                                              //    var Date = OrderPrice.Helpers.Span(c =>  c.);
                                                              //}

                                                              var CartAction = OrderListRow.AddColumn("");
                                                              {
                                                                  CartAction.Style.Width = "200px";
                                                                  var btnConfirm = CartAction.Helpers.Button("Confirmed Order", Singular.Web.ButtonMainStyle.Success, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.checkCircle);
                                                                  {
                                                                      // CartAction.Style.Width = "50px";
                                                                      btnConfirm.AddClass("btn btn-primary btn-outline");
                                                                      btnConfirm.AddBinding(Singular.Web.KnockoutBindingString.click, "Order($data)");
                                                                  }
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

      var Order = function (obj) {

          ViewModel.CallServerMethod('Order', { OrderID: obj.OrderID(),  ShowLoadingBar: true }, function (result) {
              if (result.Success) {

                  MEHelpers.Notification("Product Removed From Cart", 'Centre ', 'Success', 100000);
                  window.location = '../Orders/Orders.aspx';

              }
              else {
                  MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
              }
          })
      };
      var Home = function ()
      {
          window.location = '../Account/Home.aspx';
      };

  </script>
</asp:Content>
