<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppersExpress.Master" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs" Inherits="ShoppersExpress.Receipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Label ID="ErrorLabelReceipt" runat="server" Text="" Visible ="false"></asp:Label>

    <div id="ReceiptContent" runat="server">
        <asp:Label ID="ReceiptTitle" runat="server" Text="Receipt"></asp:Label>

        <div class="horizontalLineReceipt">
        </div>
        <!-- The 'ReceiptLabel' label will hold the names of all items purchased as well as the quantity
            purchased of each item and the prices of those items-->
        <asp:Label ID="ReceiptLabel" runat="server" Text=""></asp:Label>
        
        <div class="horizontalLineReceipt">
        </div>

        <br />
        <asp:Label ID="OrderPickupWhen" runat="server" Text="Your order should be ready for pickup on "></asp:Label>
        <br />
        <br />
        <asp:Label ID="PleaseCall" runat="server" Text="Please call 361-255-2279 if you have any questions."></asp:Label>
        <br />
        <br />
        <asp:Label ID="PurchaseThankyou" runat="server" Text="Thank You!"></asp:Label>
    </div>

</asp:Content>
