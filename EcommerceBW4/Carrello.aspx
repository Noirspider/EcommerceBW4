<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrello.aspx.cs" Inherits="EcommerceBW4.Carrello" %>

<!DOCTYPE html>
<html lang="it">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Il tuo carrello</title>
    <!-- Riferimento a Bootstrap CSS -->
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link rel="stylesheet" href="./Content/Assets/css/Carrello.css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <!-- Altri CSS se necessario -->
</head>
<body>
    <form runat="server">
        <main>
            <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark" style="background-image: linear-gradient(-220deg, #5cdb95 0%, #000000 60%);">
                <div class="container">
                    <a class="navbar-brand" runat="server" href="~/">Nome applicazione</a>
                    <button type="button" class="navbar-toggler" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" title="Attiva/Disattiva spostamento" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse d-sm-inline-flex justify-content-between">
                        <ul class="navbar-nav flex-grow-1">
                            <li class="nav-item"><a class="nav-link" runat="server" href="~/">Home</a></li>
                            <!-- <li class="nav-item"><a class="nav-link" runat="server" href="~/About">Login</a></li> -->
                            <li class="nav-item"><a class="nav-link" runat="server" href="~/Carrello">Carrello</a></li>
                        </ul>
                    </div>
                    <div class="d-flex align-items-center gap-2">
                        <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
                        <!-- Da integrare funzionalità bottone cerca--->
                        <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                    </div>
                </div>
            </nav>
            <div class="container mt-3">
                <asp:Repeater ID="carrelloRepeater" runat="server" OnItemCommand="CarrelloRepeater_ItemCommand">
                    <HeaderTemplate>
                        <div class="row">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="col-2 mb-4">
                            <div class="card h-100 w-100 bg-dark">
                                <img src='<%# Eval("ImmagineURL") %>' alt="Product Image" class="card-img-top" style="object-fit: cover" />
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Nome") %></h5>
                                    <p class="card-text">Quantità: <%# Eval("Quantita") %></p>
                                    <p class="card-text">Prezzo unitario: <%# Eval("Prezzo") %>€</p>
                                    <p class="card-text">Prezzo totale: <%# Eval("Totale") %>€</p>
                                    <div class="d-flex align-items-center justify-content-around">
                                        <asp:LinkButton ID="BtnRemoveOne" runat="server" CommandArgument='<%# Eval("ProdottoID") %>' CommandName="RemoveOne" Text="- uno" CssClass="btn btn-sm btn-danger" />
                                        <asp:LinkButton ID="BtnAddOne" runat="server" CommandArgument='<%# Eval("ProdottoID") %>' CommandName="AddOne" Text="+ uno" CssClass="btn btn-sm btn-success" />
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
        </main>
    </form>
    <!-- Riferimento a Bootstrap JS e dipendenze -->
    <div class="container body-content">
        <footer>
            <p>&copy; <%: DateTime.Now.Year %> - Applicazione ASP.NET by Team Ⅵ</p>
        </footer>
    </div>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
    </asp:PlaceHolder>
</body>
</html>
