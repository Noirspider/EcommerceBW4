<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="EcommerceBW4.AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="form-group">
            <asp:Label ID="LabelDropDownProdotto" runat="server" AssociatedControlID="DropDownProdotto" Text="Seleziona un Prodotto"></asp:Label>
            <asp:DropDownList ID="DropDownProdotto" runat="server" CssClass="form-control mb-3" AutoPostBack="true" OnSelectedIndexChanged="DropDownProdotto_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Vendite per anno"></asp:Label>
            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control mb-3" AutoPostBack="true"></asp:DropDownList>
        </div>

      <div id="Card" runat="server" class="card">
    <asp:Image ID="ImgCarrello" runat="server" ImageUrl="~/path/to/default/image.jpg" CssClass="card-img-top" alt="Card Image" Width="200" Height="150" />
    <div class="card-body">
        <asp:Label id="LblNome" runat="server" class="card-title"></asp:Label>
        <asp:Label id="LblPrezzo" runat="server" class="card-text"></asp:Label>
        <a href="#" class="btn btn-primary">Learn More</a>
    </div>
</div>

    </main>
</asp:Content>
