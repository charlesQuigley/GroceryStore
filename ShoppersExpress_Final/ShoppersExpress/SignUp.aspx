<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppersExpress.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="ShoppersExpress.SignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
    
    <!--JavaScript is defined in the HTML file because we needed to call JavaScript functions 
        from the C# code behind, and couldn't figure out how to do it with external JavaScript files.
        So, we just defined them in here instead.
    -->
    <script type="text/javascript">
        function alertBox()
        {
             alert("You're Account has been created! You were charged $50.");
             location.href = 'UserLogin.aspx';
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
   
    <div id ="SignUpContent">
        <asp:Label ID="SignUp_Label" runat="server" Text="Create an account"></asp:Label>
        <asp:Label ID="Charged" runat="server" Text="Membership costs $50 per year."></asp:Label>

        <table>
            <!--Most Textboxes have Expression validators. All Textboxes have Required Field validators.
                Password textbox has a compare validator because user must confirm his/her password -->
            <tr>
                <td>
                    <asp:TextBox CssClass="Login_Txt" ID="SignUp_Email" PlaceHolder="Email" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="SignUp_Table"  ControlToValidate="SignUp_Email" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="**Please enter a valid Email.">&nbsp;</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="SignUp_Table"  ControlToValidate="SignUp_Email" runat="server" ErrorMessage="**Please enter an Email.">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                 <td>
                    <asp:TextBox CssClass="Login_Txt SignUp_RightTextBox" ID="SignUp_CardNumber" PlaceHolder="16 Digit Card Number" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="SignUp_Table"  ControlToValidate="SignUp_CardNumber" runat="server" ValidationExpression="\d{16}" ErrorMessage="**Please enter a 16 digit Card Number.">&nbsp;</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="SignUp_Table"  ControlToValidate="SignUp_CardNumber" runat="server" ErrorMessage="**Please enter a Card Number.">&nbsp;</asp:RequiredFieldValidator>
                 </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox CssClass="Login_Txt" ID="SignUp_CardExpiration"  PlaceHolder="Card Expiration Date" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="SignUp_Table"  ControlToValidate="SignUp_CardExpiration" runat="server" ValidationExpression="^(0[1-9]|1[0-2])\/([2-2][2-9])$" ErrorMessage="**Card Expiration must be in the form MM/YY and must expire in 2022-2029.">&nbsp;</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="SignUp_Table"  ControlToValidate="SignUp_CardExpiration" runat="server" ErrorMessage="**Please enter a Card Expiration Date.">&nbsp;</asp:RequiredFieldValidator>
                </td>
                 <td>
                    <asp:TextBox CssClass="Login_Txt SignUp_RightTextBox" ID="SignUp_CardSecurity" PlaceHolder="Card Security Code" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="SignUp_Table"  ControlToValidate="SignUp_CardSecurity" runat="server" ValidationExpression="\d{3}" ErrorMessage="**Card Security Code must be a 3 digit number.">&nbsp;</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="SignUp_Table"  ControlToValidate="SignUp_CardSecurity" runat="server" ErrorMessage="**Please enter a Card Security Code.">&nbsp;</asp:RequiredFieldValidator>
                </td>
           
            </tr>
            <tr>
                <td>
                    <asp:TextBox CssClass="Login_Txt" ID="SignUp_Password" PlaceHolder="Password" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="SignUp_Table"  ControlToValidate="SignUp_Password" runat="server" ErrorMessage="**Please enter a Password.">&nbsp;</asp:RequiredFieldValidator>
                </td>
                 <td>
                    <asp:TextBox CssClass="Login_Txt SignUp_RightTextBox" ID="SignUp_ConfirmPassword"  PlaceHolder="Confirm Password" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="SignUp_Table"  ControlToValidate="SignUp_ConfirmPassword" runat="server" ErrorMessage="**Please confirm your Password.">&nbsp;</asp:RequiredFieldValidator>
                     <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="**The Passwords do not match." ValidationGroup="SignUp_Table" ControlToCompare="SignUp_Password" ControlToValidate="SignUp_ConfirmPassword">&nbsp;</asp:CompareValidator>
                 </td>
            </tr>
        </table>
        <asp:Button ID="SignUp_Confirm"  ValidationGroup="SignUp_Table" runat="server" Text="Sign Up"  OnClick="RegisterMember"/>

        <br />
        <asp:ValidationSummary ID="ValidationSummary_SignUp" CssClass="SignUp_ValidationSummary" ValidationGroup="SignUp_Table" runat="server" />
        <br />
        <asp:Label ID="EmailAlreadyExists" runat="server" Text="**Sorry, that email is already taken. Please enter a different one." Visible="false"></asp:Label>

    </div>
</asp:Content>
