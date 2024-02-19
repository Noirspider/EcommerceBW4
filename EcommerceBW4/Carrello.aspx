<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrello.aspx.cs" Inherits="EcommerceBW4.Carrello" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="carrelloRepeater" runat="server">
        <ItemTemplate>
            <div class="card">
                <img src='<%# Eval("ImmagineURL") %>' alt="Product Image" />
                <div class="card-body">
                    <h5 class="card-title"><%# Eval("Nome") %></h5>
                    <p class="card-text">Quantità: <%# Eval("Quantita") %></p>
                    <p class="card-text">Prezzo unitario: <%# Eval("Prezzo") %></p>
                    <p class="card-text">Prezzo totale: <%# Eval("Totale") %></p>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
