<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="EcommerceBW4.AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container">
            <div class="row">
                <div class="col-4">
                    <div class="form-group">
                        <asp:Label ID="LabelDropDownProdotto" runat="server" AssociatedControlID="DropDownProdotto" Text="Seleziona un Prodotto"></asp:Label>
                        <asp:DropDownList ID="DropDownProdotto" runat="server" CssClass="form-control mb-3" AutoPostBack="true"  OnSelectedIndexChanged= "DropDownProdottoBoth"></asp:DropDownList>
                    </div>

                </div>
                <div class="col-4">
                    <div ID="Card" runat="server" class="card" visible="false" Style="box-shadow: 0 9px 50px hsla(20, 67%, 75%, 0.31)">
                        <asp:Image ID="ImgCarrello" runat="server" ImageUrl="~/path/to/default/image.jpg" CssClass="card-img-top d-inline-flex" alt="Card Image" Style="object-fit: cover;" />
                        <div class="card-body">
                            <asp:Label ID="LblNome" runat="server" class="card-text text-black"></asp:Label>
                            <asp:Label ID="LblPrezzo" runat="server" class="card-text text-black"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-4">
                    <div ID="Card1" runat="server" class="card" visible="false" Style="box-shadow: 0 9px 50px hsla(20, 67%, 75%, 0.31)">
                        <div class="card-body">
                            <asp:Label ID="Label1" runat="server" class="card-title text-black"></asp:Label>
                            <asp:Label ID="Label3" runat="server" class="card-text text-black"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
