<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoryManager.aspx.cs" Inherits="MEWeb.Category.CategoryManager" %>

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
                          var HomeContainerTab = AssessmentsTab.AddTab("Category Table");
                          {
                              var Row = HomeContainerTab.Helpers.DivC("row margin0");
                              {
                                  var RowCol = Row.Helpers.DivC("col-md-12");
                                  {
                                      RowCol.Helpers.HTML().Heading2("Category Management");



                                      var CategoryList = RowCol.Helpers.TableFor<MELib.Categories.Category>((c) => c.CategoryList, true, true);
                                      {
                                          CategoryList.AddClass("table table-striped table-bordered table-hover");
                                          var ProductListRow = CategoryList.FirstRow;
                                          {
                                              var CategoryName = ProductListRow.AddColumn(c => c.CategoryName);
                                              {
                                                  CategoryName.Style.Width = "700";
                                              }
                             
                                              var CategoryActive = ProductListRow.AddColumn(c => c.IsActiveInd);
                                              {
                                                  CategoryActive.Style.Width = "300px";  
                                              }
                                          }
                                      }

                                      var SaveList = RowCol.Helpers.DivC("col-md-12 text-right");
                                      {
                                          var SaveBtn = SaveList.Helpers.Button("Save", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.save);
                                          {
                                              SaveBtn.AddClass("btn-primary btn btn btn-primary");
                                              SaveBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "SaveCategory($data)");
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

      var SaveCategory = function (obj) {
          ViewModel.CallServerMethod('SaveCategoryList', { CategoryList: ViewModel.CategoryList.Serialise(), ShowLoadingBar: true }, function (result) {
        if (result.Success) {
          MEHelpers.Notification("Successfully Saved", 'center', 'success', 3000);
        }
        else {
          MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
        }
      });
      }
      </script>
</asp:Content>
