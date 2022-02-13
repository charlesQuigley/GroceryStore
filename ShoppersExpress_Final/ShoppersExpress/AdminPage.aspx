<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppersExpress.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="ShoppersExpress.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <!-- Show Product and USER tables via Gridviews -->
    <!-- Admins can add and delete rows from these tables on this page -->
    <asp:Label ID="AdminProductLabel" CssClass="AdminTableLabel" runat="server" Text="Product Table"></asp:Label>
    <br />
    <!-- Gridviews are binded to Database data here -->
    <asp:SqlDataSource ID="SqlProductDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:StoreConnection %>" SelectCommand="SELECT * FROM [USER]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlUserDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:StoreConnection %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>
    
    <asp:GridView ID="ProductGridView" runat="server" DataSourceID="SqlUserDataSource" AllowPaging="True">
    </asp:GridView>
    <br />
    <div class="AddOrRemoveRows">
        <div class="AddRows">
            <!-- Stuff for adding a row to the Product table -->
            <!-- Required Field Vlidation has been implemented -->
            <asp:Label CssClass="AddDeleteRowLabel" runat="server" Text="Add A Row"></asp:Label>
            <table>
                <tr>
                    <td class="firstColumn">
                        <asp:TextBox ID="AddRow_ProductID" PlaceHolder="Product ID" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Product_Table_Add" ControlToValidate="AddRow_ProductID" runat="server" ErrorMessage="**Please Input The Product ID.">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                    <td class="secondColumn">
                        <asp:TextBox ID="AddRow_ProductName" PlaceHolder="Product Name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Product_Table_Add" ControlToValidate="AddRow_ProductName" runat="server" ErrorMessage="**Please Input The Product Name.">&nbsp;</asp:RequiredFieldValidator>

                   </td>
                </tr>
                <tr>
                    <td class="firstColumn">
                        <asp:TextBox ID="AddRow_ProductDescription"  PlaceHolder="Product Description" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Product_Table_Add" ControlToValidate="AddRow_ProductDescription" runat="server" ErrorMessage="**Please Input The Product Description.">&nbsp;</asp:RequiredFieldValidator>

                    </td>
                    <td class="secondColumn">
                        <asp:TextBox ID="AddRow_ProductImage" PlaceHolder="Product Image" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Product_Table_Add" ControlToValidate="AddRow_ProductImage" runat="server" ErrorMessage="**Please Input The Product Image.">&nbsp;</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td>
                       <asp:TextBox ID="AddRow_ProductPrice" PlaceHolder="Product Price" runat="server"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Product_Table_Add" ControlToValidate="AddRow_ProductPrice" runat="server" ErrorMessage="**Please Input The Product Price.">&nbsp;</asp:RequiredFieldValidator>

                    </td>
                    <td  class="secondColumn">

                    </td>
                </tr>
                <tr>
                    <td>
                      <asp:Button class="AddRow_Button" ValidationGroup="Product_Table_Add" runat="server" Text="Add Row" OnClick="AddRowProduct_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Product_Table_Add"  CssClass ="Admin_ValidationSummary" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="AddRow_NoExist_Product" class="AddRow_NoExist" runat="server" Visible="false"  Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class ="AdminVerticalLine"></div>
        <!-- Stuff for deleting a row from the Product table. -->
        <!-- Required field validation has been implemented -->
        <div class="RemoveRows">
            <asp:Label CssClass="AddDeleteRowLabel" runat="server" Text="Delete A Row"></asp:Label>
            <table>
                <tr>
                    <td>
                        <asp:Label class="DeleteRow_Label" runat="server" Text="Please enter the Product ID of the row you want to delete"></asp:Label>
                        <asp:TextBox ID="DeleteRow_ProductID" PlaceHolder="Product ID" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Product_Table_Delete" ControlToValidate="DeleteRow_ProductID" runat="server" ErrorMessage="**Please Input The Product ID.">&nbsp;</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button class="DeleteRow_Button"  ValidationGroup="Product_Table_Delete"  runat="server" Text="Delete Row" OnClick="DeleteRowProduct_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="Product_Table_Delete" CssClass ="Admin_ValidationSummary" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                       <asp:Label ID="DeleteRow_NoExist_Product" class="DeleteRow_NoExist" runat="server" Visible="false"  Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div id="AdminPage_HorizontalLine">
       <hr />
    </div>

   
    <br />
    <asp:Label ID="AdminMemberLabel"  CssClass="AdminTableLabel" runat="server" Text="Member Table"></asp:Label>
    <br />
    <asp:GridView ID="MemberGridView" runat="server" DataSourceID="SqlProductDataSource" AllowPaging="True">
    </asp:GridView>
    <br />
    <div class="AddOrRemoveRows">
        <!-- Stuff for adding a row to the USER table (Table containing the members) -->
        <!-- Required Field Validators and Regular Expression Validators have been implemented -->
        <div class="AddRows">
            <asp:Label CssClass="AddDeleteRowLabel" runat="server" Text="Add A Row"></asp:Label>
            <table>
                <tr>
                    <td class="firstColumn">
                        <asp:TextBox ID="AddRow_MemberID" PlaceHolder="Member ID" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="User_Table_Add" ControlToValidate="AddRow_MemberID" runat="server" ErrorMessage="**Please Input The Member ID.">&nbsp;</asp:RequiredFieldValidator>

                    </td>
                    <td class="secondColumn">
                        <asp:TextBox ID="AddRow_MemberEmail" PlaceHolder="Member Email" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="User_Table_Add" ControlToValidate="AddRow_MemberEmail" runat="server" ErrorMessage="**Please Input The Member Email.">&nbsp;</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="User_Table_Add" ControlToValidate="AddRow_MemberEmail" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  ErrorMessage="**Please Input A Valid Email.">&nbsp</asp:RegularExpressionValidator>

                   </td>
                </tr>
                <tr>
                    <td class="firstColumn">
                        <asp:TextBox ID="AddRow_MemberPassword"  PlaceHolder="Member Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="User_Table_Add" ControlToValidate="AddRow_MemberPassword" runat="server" ErrorMessage="**Please Input The Member Password.">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                    <td class="secondColumn">
                        <asp:TextBox ID="AddRow_MemberCardNumber" PlaceHolder="Member Card Number" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="User_Table_Add" ControlToValidate="AddRow_MemberCardNumber" runat="server" ErrorMessage="**Please Input The Member Card Number.">&nbsp;</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="User_Table_Add"  ControlToValidate="AddRow_MemberCardNumber" runat="server" ErrorMessage="**Please Input The 16 Digit Card Number." ValidationExpression="\d{16}">&nbsp;</asp:RegularExpressionValidator> 
                    </td>
                </tr>
                 <tr>
                    <td class="firstColumn">
                        <asp:TextBox ID="AddRow_MemberCardExpirationDate"  PlaceHolder="Card Expiration Date" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="User_Table_Add" ControlToValidate="AddRow_MemberCardExpirationDate" runat="server" ErrorMessage="**Please Input The Member Card Expiration Date.">&nbsp;</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="User_Table_Add" ControlToValidate="AddRow_MemberCardExpirationDate" runat="server" ErrorMessage="**Card Expiration must be in the form MM/YY and must expire in 2022-2029." ValidationExpression="^(0[1-9]|1[0-2])\/([2-2][2-9])$">&nbsp;</asp:RegularExpressionValidator>
                    </td>
                    <td class="secondColumn">
                        <asp:TextBox ID="AddRow_MemberCardSecurityCode" PlaceHolder="Card Security Code" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="User_Table_Add" ControlToValidate="AddRow_MemberCardSecurityCode" runat="server" ErrorMessage="**Please Input The Member Card Security Code.">&nbsp;</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="User_Table_Add" ControlToValidate="AddRow_MemberCardSecurityCode" runat="server" ErrorMessage="**Card Security Code must be a 3 digit number." ValidationExpression="\d{3}">&nbsp;</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                      <asp:Button class="AddRow_Button"  ValidationGroup="User_Table_Add"  runat="server" Text="Add Row" OnClick="AddRowMember_Click" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ValidationSummary ID="ValidationSummary3" ValidationGroup="User_Table_Add"  CssClass ="Admin_ValidationSummary" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="AddRow_NoExist_Member" class="AddRow_NoExist" runat="server" Visible="false"  Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class ="AdminVerticalLine"></div>
        <!-- Stuff for deleting a row from the USER table (the table containing Shopper's Express' members) -->
        <!-- Required Field validators and Expression Validators have been implemented -->
        <div class="RemoveRows">
            <asp:Label CssClass="AddDeleteRowLabel" runat="server" Text="Delete A Row"></asp:Label>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="DeleteRow_Label_Member" class="DeleteRow_Label" runat="server" Text="Please enter the Member ID of the row you want to delete"></asp:Label>
                        <asp:TextBox ID="DeleteRow_MemberID" PlaceHolder="Member ID" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="User_Table_Delete" ControlToValidate="DeleteRow_MemberID" runat="server" ErrorMessage="**Please Input The Member ID.">&nbsp;</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button class="DeleteRow_Button" ValidationGroup="User_Table_Delete"  runat="server" Text="Delete Row" OnClick="DeleteRowMember_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ValidationSummary ID="ValidationSummary4" ValidationGroup="User_Table_Delete" CssClass ="Admin_ValidationSummary" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                       <asp:Label id="DeleteRow_NoExist_Member"  class="DeleteRow_NoExist" runat="server" Visible="false"  Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
