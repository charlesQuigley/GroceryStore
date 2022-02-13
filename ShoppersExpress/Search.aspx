<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppersExpress.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ShoppersExpress.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        
        <!-- If the Session object containing the user's search query does not exist, then 
            the user never typed anything into the search bar. If that is the case,
            then EmptySearch_Label becomes visible -->
        <asp:Label ID="EmptySearch_Label" runat="server" Text="Please enter a product name into the search bar." Visible="false"></asp:Label>


        <!-- Using DataList to show all Product table entries whose product name matches the user's search query -->
        <asp:DataList ID ="SearchList" runat="server" OnItemCommand ="SearchList_GetProductID"  DataKeyFields="PRODUCT_ID" RepeatDirection="Horizontal" RepeatColumns="3">
              <ItemTemplate>
                <table>
                    <tr>
                        <td class ="SearchCell">
                            <asp:LinkButton class="ProductPageButton" CommandName="GetProductID" runat="server" >
                                    <img class="Product_Image_Search" src="<%#Eval("PRODUCT_IMAGE") %>" />
                             </asp:LinkButton>
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

</asp:Content>
