<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="EcommerceBW4.AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="form-group">
            <asp:Label ID="LabelDropDownProdotto" runat="server" AssociatedControlID="DropDownProdotto" Text="Seleziona un Prodotto"></asp:Label>
            <asp:DropDownList ID="DropDownProdotto" runat="server" CssClass="form-control mb-3" OnSelectedIndexChanged="DropDownProdotto_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Vendite per anno"></asp:Label>
            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control mb-3" AutoPostBack="true"></asp:DropDownList>
        </div>

        <div id="Card" runat="server" class="card" style="display: none;">
            <img src='<%# Eval ("ImmagineURL") %>' id="ImgCarrello" runat="server" class="card-img-top" alt="Card Image" width="200" height="150" />

            <div class="card-body">
                <h5 id="LblNome" runat="server" class="card-title"></h5>
                <p id="LblPrezzo" runat="server" class="card-text"></p>
                <a href="#" class="btn btn-primary">Learn More</a>
            </div>
        </div>
    </main>
</asp:Content>
