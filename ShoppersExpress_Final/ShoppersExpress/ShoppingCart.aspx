<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppersExpress.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="ShoppersExpress.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <link rel="stylesheet" type="text/css" href="StyleSheet.css" />

        <!--JavaScript is defined in the HTML file because we needed to call JavaScript functions 
        from the C# code behind, and couldn't figure out how to do it with external JavaScript files.
        So, we just defined them in here instead.
        -->
         <script type="text/javascript">
             function alertBox() {
                 alert("Thank You For Shopping With Us!");
                 location.href = 'Receipt.aspx';
             }

         </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <asp:Label ID="CartTitle" runat="server" Text="Your Cart"></asp:Label>
    <br />
    <div  id="HorizontalLine1" runat="server" class="CartHorizontalLine">
        <hr />
    </div>
      <asp:Label ID="EmptySearch_Label" runat="server" Text="" Visible="false"></asp:Label>

        <!-- DataList is used to render to the screen all of the products that the user has added to 
            his or her cart-->
        <asp:DataList ID ="SearchList" runat="server"  DataKeyFields="PRODUCT_ID" RepeatDirection="Horizontal" RepeatColumns="3">
              <ItemTemplate>
                <table>
                    <tr>
                        <td class ="SearchCell">
                             <img class="Product_Image_Search" src="<%#Eval("PRODUCT_IMAGE") %>" />
 
                             <br />
                             <asp:Label  CssClass="Product_Name_Search" runat="server" Text=""> <%#Eval("PRODUCT_NAME") %></asp:Label>
                             <br />
                             <asp:Label  CssClass="Product_Description_Search" runat="server" Text=""> <%#Eval("PRODUCT_DESCRIPTION") %></asp:Label> 
                             <br />
                             <asp:Label  CssClass="Product_Price_Search" runat="server" Text=""> $<%#Eval("PRODUCT_PRICE") %></asp:Label> 
                                               
                             </td>    
                                              
                     </tr>
                                        
                  </table>
                </ItemTemplate>
         </asp:DataList>

        <div id="HorizontalLine2" runat="server" class="CartHorizontalLine_Dashed">
        <hr />
        </div>
        <asp:Label ID="TotalPurchase_Label" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="TotalPrice_Label" runat="server" Text=""></asp:Label>

        <br />
        <br />

        <div id="HorizontalLine3" runat="server" class="CartHorizontalLine">
            <hr />
        </div>

        <div id="ShoppingCartButtons">
            <asp:Button ID="PurchaseCartButton" runat="server" Text="Buy" OnClick="PurchaseCart" />

            <asp:Button ID="DeleteCartButton" runat="server" Text="Delete Cart" OnClick="DeleteCart" />
        </div>

        

        <br />
        <br />

</asp:Content>
