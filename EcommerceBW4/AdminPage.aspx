<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="EcommerceBW4.AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        
        <asp:Label for="DropDownProdotto" ID="DropDown1" runat="server" Text="Seleziona un Prodotto"></asp:Label>
        <asp:DropDownList ID="DropDownProdotto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownProdotto_SelectedIndexChanged"></asp:DropDownList>

        <asp:Label ID="Label2" runat="server" Text="Vendite per anno"></asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>

        <asp:Label ID="Label3" runat="server" Text="Vendite per Regione"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
    </main>
</asp:Content>
