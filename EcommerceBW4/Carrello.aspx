﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrello.aspx.cs" Inherits="EcommerceBW4.Carrello" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-3">
        <asp:Repeater ID="carrelloRepeater" runat="server" OnItemCommand="CarrelloRepeater_ItemCommand">
            <HeaderTemplate>
                <div class="row row-cols-6">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col mb-4">
                    <div class="card h-100 w-100 bg-dark">
                        <img src='<%# Eval("ImmagineURL") %>' alt="Product Image" class="card-img-top" style="object-fit: cover" />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nome") %></h5>
                            <p class="card-text">Quantità: <%# Eval("Quantita") %></p>
                            <p class="card-text">Prezzo unitario: <%# Eval("Prezzo") %>€</p>
                            <p class="card-text">Prezzo totale: <%# Eval("Totale") %>€</p>
                            <div class="d-flex align-items-center justify-content-between">
                                <asp:LinkButton ID="btnRemoveOne" runat="server" CommandArgument='<%# Eval("ProdottoID") %>' CommandName="RemoveOne" Text="- uno" CssClass="btn btn-sm btn-danger" />
                                <asp:LinkButton ID="btnAddOne" runat="server" CommandArgument='<%# Eval("ProdottoID") %>' CommandName="AddOne" Text="+ uno" CssClass="btn btn-sm btn-success" />
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <asp:Button ID="BtnClearCart" runat="server" OnClick="BtnClearCart_Click" Text="Svuota carrello" CssClass="btn btn-warning" />
        <asp:Button ID="BtnCompleteOrder" runat="server" OnClick="BtnCompleteOrder_Click" Text="Completa ordine" CssClass="btn btn-primary" />

    </div>
</asp:Content>
