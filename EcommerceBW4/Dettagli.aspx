﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewState="true" CodeBehind="Dettagli.aspx.cs" Inherits="EcommerceBW4.Dettagli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                    <asp:Button ID="AddCarrello" runat="server" Text="Aggiungi al Carrello" CssClass="btn text-white" style="background-image: linear-gradient(-220deg, #5cdb95 0%, #000000 60%);" OnClick="AddCarrello_Click" />  
                </div>
            </div>
        </div>
    </div>
<div id="myModal" class="modal" runat="server" visible="false">
    <div class="modal-content">
        <asp:Label ID="ModalContent" runat="server" CssClass="text-white"/>
        <asp:Button ID="CloseButton" runat="server" Text="Chiudi" OnClick="CloseButton_Click" cssClass="bottoneModale" />
    </div>
</div>

</asp:Content>  