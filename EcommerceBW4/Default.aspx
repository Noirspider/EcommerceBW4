<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EcommerceBW4._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <!-- Card e tutte cose -->
        <div class="container mt-5">
            <div class="row">
                <asp:Repeater ID="prodottiRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col mb-4">
                            <div class="card text-white bg-dark mb-3" style="width: 18rem; box-shadow: 0 9px 50px hsla(20, 67%, 75%, 0.31);">
                                <div style="max-width: 100%; max-height: 10rem; overflow: hidden;">
                                   <img src='<%# Eval("ImmagineURL") %>' class="card-img-top w-100 h-100" alt="ProductImg">
                                </div>
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Nome") %></h5>
                                    <p>Prezzo: <i><%#Eval("Prezzo") %>&euro;</i></p>
                                    <!-- Da reintegrare -->
                                    
                                    <asp:LinkButton ID="ToDetail" runat="server" Text="Dettagli" CssClass="btn btn-dark" Style="background-color: #5cdb95;"  />
                                    
                                    <asp:LinkButton ID="ToCart" runat="server" Text="Aggiungi al carrello" CssClass="btn btn-dark" Style="background-color: #5cdb95;"  />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </main>

</asp:Content>
