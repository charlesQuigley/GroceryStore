<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppersExpress.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="ShoppersExpress.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div id ="Login_Page">
        <asp:Label ID="Signin_Label" runat="server" Text="Sign in to your account"></asp:Label>
        <table id ="Siginin_Table">
            <!-- All textboxes are validated. Email has Required Field Validator and Expression Validator -->
            <!-- Password just has required Field Validator -->
            <tr>
                <td>
                    <asp:TextBox ID="Email_Login_Txt" CssClass="Login_Txt" placeholder="Email..." runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="Email_FieldValidator" CssClass="FieldValidator" runat="server" ValidationGroup="SignIn" ControlToValidate="Email_Login_Txt" ErrorMessage="**Please Enter an Email**" ForeColor="Red">&nbsp;</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="Email_ExpressionValidator" CssClass="FieldValidator" runat="server" ValidationGroup="SignIn" ControlToValidate="Email_Login_Txt"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="**Please Enter a Valid Email Address**">&nbsp;</asp:RegularExpressionValidator>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="Password_Login_Txt" CssClass="Login_Txt" placeholder="Password..." runat="server" TextMode="Password"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="Password_FieldValidator" CssClass="FieldValidator" runat ="server" ValidationGroup="SignIn" ControlToValidate="Password_Login_Txt" ErrorMessage="**Please Enter a Password**" ForeColor="Red">&nbsp;</asp:RequiredFieldValidator>
                    <br />
                    <asp:ValidationSummary ID="SignIn_ValidationSummary" ValidationGroup="SignIn" runat="server" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="AccountExists" CssClass="FieldValidator" runat="server" Text="">&nbsp</asp:Label>
                    <br />
                    <asp:Button ID="SignIn_Button" CssClass="Login_Button" ValidationGroup="SignIn"  runat="server" Text="Sign In"  OnClick="SignIn_Button_Click"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="SignUp_Button" CssClass="Login_Button" runat="server" Text="Not a member? &nbsp; Register Here." OnClick="SignUp" />
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>
