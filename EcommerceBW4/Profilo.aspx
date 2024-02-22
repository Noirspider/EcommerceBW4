<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profilo.aspx.cs" Inherits="EcommerceBW4.Profilo" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
            <h1>Profilo</h1>
    <asp:Repeater ID="RepeaterOrders" runat="server">
  <ItemTemplate>
<div class="col-4">

    <div class="card bg-transparent border mb-3">
      <div class="card-header">
        Ordine #<%# Eval("OrdineID") %>
      </div>
      <div class="card-body bg-transparent">
        <p>Data Ordine: <%# Eval("DataOrdine", "{0:d}") %></p>
        <p>Totale Ordine: €<%# Eval("TotaleOrdine") %></p>
        <p>Stato: <%# Eval("StatoOrdine") %></p>
      </div>
    </div>
    </div>

  </ItemTemplate>
</asp:Repeater>
        </div>

</asp:Content>
