<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dettagli.aspx.cs" Inherits="EcommerceBW4.Dettagli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mb-3">
        <div class="row g-0 d-flex align-items-center">
            <div class="col-md-4">
                <asp:Image ID="ImgUrl" runat="server" CssClass="img-fluid rounded" />
            </div>
            <div class="col-md-8">
                <div class="card-body">
                    <h3><asp:Label ID="Nome" class="card-title" runat="server"></asp:Label></h3>
                    <p> <asp:Label ID="Descrizione" class="card-text" runat="server"></asp:Label></p>
                    <p> <asp:Label ID="DescrizioneEstesa" class="card-text" runat="server"></asp:Label></p>
                    <p> <asp:Label ID="QuantitaDisponibile" class="card-text" runat="server"></asp:Label></p>
                    <p><strong><asp:Label ID="Prezzo" runat="server"></asp:Label></strong></p>
                    <asp:TextBox ID="QuantitaTextBox" runat="server" Text="1"></asp:TextBox>
                    <asp:Button ID="Incrementa" runat="server" Text="+" OnClick="IncrementaQuantita" />
                    <asp:Button ID="Decrementa" runat="server" Text="-" OnClick="DecrementaQuantita" />
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
</asp:Content>  