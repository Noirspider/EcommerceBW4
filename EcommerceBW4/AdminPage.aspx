<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="EcommerceBW4.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        
        <asp:Label for="DropDownProdotto" ID="DropDown1" runat="server" Text=""></asp:Label>
        <asp:DropDownList ID="DropDownProdotto" runat="server"></asp:DropDownList>

        <asp:Label ID="Label2" runat="server" Text="Vendite per anno"></asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>

        <asp:Label ID="Label3" runat="server" Text="Vendite per Regione"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
    </main>
</asp:Content>
