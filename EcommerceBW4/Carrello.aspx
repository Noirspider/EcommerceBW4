<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrello.aspx.cs" Inherits="EcommerceBW4.Carrello" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-3">
        <asp:Repeater ID="carrelloRepeater" runat="server">
            <HeaderTemplate>
                <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-md-2 mb-4">
                    <div class="card h-100 bg-dark">
                        <img src='<%# Eval("ImmagineURL") %>' alt="Product Image" class="card-img-top" />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nome") %></h5>
                            <p class="card-text">Quantità: <%# Eval("Quantita") %></p>
                            <p class="card-text">Prezzo unitario: <%# Eval("Prezzo") %>€</p>
                            <p class="card-text">Prezzo totale: <%# Eval("Totale") %>€</p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
