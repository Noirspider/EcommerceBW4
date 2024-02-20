<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Dettagli.aspx.cs" Inherits="EcommerceBW4.Dettagli" %>

<!DOCTYPE html>

<html lang="it">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title> Dettagli </title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="./Content/Assets/css/Carrello.css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

</head>
<body class="text-white" style="background-color:#000000">
    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
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
                <form class="form-inline">
                    <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
                    <!-- Da integrare funzionalità bottone cerca--->
                    <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                </form>
            </div>
        </nav>
        <div class="container body-content">
              <div class="card mb-3">
        <div class="row g-0 d-flex align-items-center">
            <div class="col-md-4">
                <asp:Image ID="ImgUrl" runat="server" CssClass="img-fluid rounded" />
            </div>
            <div class="col-md-8">
                <div class="card-body">
                    <h3><asp:Label ID="Nome" class="card-title text-dark" runat="server"></asp:Label></h3>
                    <p> <asp:Label ID="Descrizione" class="card-text text-dark" runat="server"></asp:Label></p>
                    <p> <asp:Label ID="DescrizioneEstesa" class="card-text text-dark" runat="server"></asp:Label></p>
                    <p> <asp:Label ID="QuantitaDisponibile" class="card-text text-dark" runat="server"></asp:Label></p>
                    <p><strong><asp:Label ID="Prezzo" class="text-dark" runat="server"></asp:Label></strong></p>
                    <asp:TextBox ID="QuantitaTextBox" runat="server" Text="1"></asp:TextBox>
                    <asp:Button ID="AddCarrello" runat="server" Text="Aggiungi al Carrello" CssClass="btn btn-danger" OnClick="AddCarrello_Click" />  
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Literal ID="PopupLiteral" runat="server"></asp:Literal>
        </ContentTemplate>
    </asp:UpdatePanel>
            <hr />
            <footer>
                <p>&copy; <%: DateTime.Now.Year %> - Applicazione ASP.NET by Team6</p>
            </footer>
        </div>
    </form>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
    </asp:PlaceHolder>
</body>
</html>
