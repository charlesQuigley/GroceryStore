<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppersExpress.Master" AutoEventWireup="true" CodeBehind="ProductDescriptionPage.aspx.cs" Inherits="ShoppersExpress.ProductDescriptionPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
     <link rel="stylesheet" type="text/css" href="StyleSheet.css" />

    <!--JavaScript is defined in the HTML file because we needed to call JavaScript functions 
        from the C# code behind, and couldn't figure out how to do it with external JavaScript files.
        So, we just defined them in here instead.
     -->
     <script type="text/javascript">
         function alertBox() {
             alert("The item has been added to your cart.");
         }

         function notMember() {
             alert("You must be logged in as a member to add to your cart.");
         }
     </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table>
        <tr>
            <td>
                <!--Using 2 DataList objects to show 1 product's Image, name, description, and price -->
                <!--The user can decide to add this item to his/her cart on this page -->
                <!--This page is used for every item in the database, we use Session Objects to
                    make this page dynamic. We just place a specific product's Product ID
                    into a Session object and send that object over to this page, where the code behind
                    uses the product ID to populate this page -->
                <asp:DataList ID ="ProductPageView_Image" runat="server">
                       <ItemTemplate>
                           <img class="ProductView_Image" src="<%#Eval("PRODUCT_IMAGE") %>" />
                      </ItemTemplate>
                </asp:DataList>
            </td>

            <td>
                <div id="Product_Page_Vertical_Line"></div>
            </td>
            
            <td id="ProductPage_Info_Column">
                <asp:DataList ID ="ProductPageView_Info" runat="server">
                       <ItemTemplate>
                           <asp:Label  CssClass="ProductView_Name" runat="server" Text=""> <%#Eval("PRODUCT_NAME") %></asp:Label>
                           <br />
                           <asp:Label  CssClass="ProductView_Description" runat="server" Text=""> <%#Eval("PRODUCT_DESCRIPTION") %></asp:Label> 
                           <br />
                           <asp:Label  CssClass="ProductView_Price" runat="server" Text="">$<%#Eval("PRODUCT_PRICE") %></asp:Label> 
                                              
                      </ItemTemplate>
                </asp:DataList>
                <br />
                <div id="ProductView_Button">
                <asp:Button runat="server" Text="Add to Cart" OnClick="AddToCart"/>
                </div>
            </td>
        </tr>
     </table>
</asp:Content>




