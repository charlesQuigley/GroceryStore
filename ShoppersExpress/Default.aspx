<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppersExpress.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShoppersExpress.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Create a table of DataList's that will create columns on the page containing the product information from the Product database -->         
    <table id ="ProductTable">  
                   <tr>
                       <td>
                                <!-- First Column ... Will display 3 product names, images, descriptions, and prices from database connection -->
                                <!-- We are not hardcoding these products on our web page. The products are being pulled from the database --> 
                                <asp:DataList ID ="ProductList1" runat="server" OnItemCommand ="ProductList_GetProductID"  DataKeyFields="PRODUCT_ID">
                                    <ItemTemplate>
                                        <div class ="Product_Column">
                                        <table>
                                            <tr>
                                                <td>
                                                     <asp:LinkButton class="ProductPageButton" CommandName="GetProductID_Column1" runat="server" >
                                                            <img class="Product_Image" src="<%#Eval("PRODUCT_IMAGE") %>" />
                                                     </asp:LinkButton>
                                                     <br />
                                                     <asp:Label  CssClass="Product_Name" runat="server" Text=""> <%#Eval("PRODUCT_NAME") %></asp:Label>
                                                     <br />
                                                     <asp:Label  CssClass="Product_Description" runat="server" Text=""> <%#Eval("PRODUCT_DESCRIPTION") %></asp:Label> 
                                                     <br />
                                                     <asp:Label  CssClass="Product_Price" runat="server" Text=""> $<%#Eval("PRODUCT_PRICE") %></asp:Label> 
                                               
                                                    </td>    
                                              
                                            </tr>
                                        
                                        </table>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                       </td>
                            
                       <td>
                           <!-- Second Column .. 3 more database items from the Product table -->
                            <asp:DataList ID ="ProductList2" runat="server" OnItemCommand ="ProductList_GetProductID"  DataKeyFields="PRODUCT_ID">
                                <ItemTemplate>
                                    <div class ="Product_Column">
                                    <table>
                                        <tr>
                                            <td>
                                                 <asp:LinkButton class="ProductPageButton" CommandName="GetProductID_Column2" runat="server"  >
                                                        <img class="Product_Image" src="<%#Eval("PRODUCT_IMAGE") %>" />
                                                 </asp:LinkButton>
                                    
                                                <br />
                                                <asp:Label  CssClass="Product_Name" runat="server" Text=""> <%#Eval("PRODUCT_NAME") %></asp:Label>
                                                <br />
                                                <asp:Label  CssClass="Product_Description" runat="server" Text=""> <%#Eval("PRODUCT_DESCRIPTION") %></asp:Label> 
                                                <br />
                                                 <asp:Label  CssClass="Product_Price" runat="server" Text="">$<%#Eval("PRODUCT_PRICE") %></asp:Label> 
                                               
                                            </td>          
                                        </tr>
                                    </table>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                       </td>

                       <td>
                           <!-- Third Column ... 3 more items from Product table -->
                            <asp:DataList ID ="ProductList3" runat="server" OnItemCommand ="ProductList_GetProductID"  DataKeyFields="PRODUCT_ID">
                                <ItemTemplate>
                                    <div class ="Product_Column">
                                    <table>
                                        <tr>
                                            <td>
                                                 <asp:LinkButton class="ProductPageButton" CommandName="GetProductID_Column3" runat="server"  >
                                                        <img class="Product_Image" src="<%#Eval("PRODUCT_IMAGE") %>" />
                                                 </asp:LinkButton>
                                                <br />
                                                <asp:Label  CssClass="Product_Name" runat="server" Text=""> <%#Eval("PRODUCT_NAME") %></asp:Label>
                                                <br />
                                                <asp:Label  CssClass="Product_Description" runat="server" Text=""> <%#Eval("PRODUCT_DESCRIPTION") %></asp:Label> 
                                                <br />
                                                 <asp:Label  CssClass="Product_Price" runat="server" Text="">$<%#Eval("PRODUCT_PRICE") %></asp:Label> 
                                               
                                            </td>          
                                        </tr>
                                    </table>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                       </td>
                       <td>

                       </td>
                   </tr>
               </table>
</asp:Content>


  


    
  






    
                   
               



        
      
   
              

    
                   
               




