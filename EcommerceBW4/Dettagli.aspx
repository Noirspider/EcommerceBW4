<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewState="true" CodeBehind="Dettagli.aspx.cs" Inherits="EcommerceBW4.Dettagli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card my-3 coloreSfondoDettagli">
        <div class="row g-0 d-flex">
            <div class="col-md-3 d-flex align-items-center justify-content-center">
                <asp:Image ID="ImgUrl" runat="server" CssClass="img-fluid rounded m-0 imgDettagli"/>
            </div>
            <div class="col-md-9">
                <div class="card-body">
                    <h3><asp:Label ID="Nome" class="card-title" runat="server"></asp:Label></h3>
                    <p class="m-0"> <asp:Label ID="Descrizione" class="card-text" runat="server"></asp:Label></p>
                    <asp:Button ID="MostraAltroButton" runat="server" Text="altro..." OnClick="MostraDettagli" CssClass="btn text-primary p-0" style="background: none; border: none;" />
                    <div id="DescrizioneEstesaDiv" runat="server" style="display: none;">
                         <p class="m-0"><asp:Label ID="DescrizioneEstesa" runat="server"></asp:Label></p>
                    </div>
                    <asp:Button ID="NascondiButton" runat="server" Text="meno" OnClick="NascondiDettagli" CssClass="btn text-primary p-0" style="background: none; border: none;" Visible="false" />
                    <p class="mt-2">Quantità disponibile: <br /><asp:Label ID="QuantitaDisponibile" class="card-text" runat="server"></asp:Label></p>
                    <p><strong><asp:Label ID="Prezzo" runat="server"></asp:Label></strong></p>
                    <asp:TextBox ID="QuantitaTextBox" runat="server" Text="1"></asp:TextBox>
                    <asp:Button ID="AddCarrello" runat="server" Text="Aggiungi al Carrello" CssClass="btn text-dark btnDettagli" OnClick="AddCarrello_Click" />  
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