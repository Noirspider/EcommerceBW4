<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="EcommerceBW4.AdminPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container">
            <div class="row">
                <div class="col-4">
                    <div class="form-group">
                        <asp:Label ID="LabelDropDownProdotto" runat="server" AssociatedControlID="DropDownProdotto" Text="Seleziona un Prodotto"></asp:Label>
                        <asp:DropDownList ID="DropDownProdotto" runat="server" CssClass="form-control mb-3 ms-2" AutoPostBack="true"  OnSelectedIndexChanged="DropDownProdotto_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text="Nome Prodotto" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" Text="ImgURL" CssClass="control-label"></asp:Label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" Text="Prezzo" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="Create" runat="server" Text="Crea Item" OnClick="InsertItem" CssClass="btn btn-secondary me-2" />
                        <asp:Button ID="Modify" runat="server" Text="Modifica"  OnClick="ModificaItem" CssClass="btn btn-secondary" />
                        <asp:Button ID="Delete" runat="server" Text="Cancella"  OnClick="DeleteItem" CssClass="btn btn-danger ms-2" />
                    </div>
                </div>
                <div class="col-4">
                    <div id="Card" runat="server" class="card" visible="false" style="box-shadow: 0 9px 50px hsla(20, 67%, 75%, 0.31);">
                        <asp:Image ID="ImgCarrello" runat="server" ImageUrl="~/path/to/default/image.jpg" CssClass="card-img-top d-inline-flex" alt="Card Image" Style="object-fit: cover;" />
                        <div class="card-body">
                            <asp:Label ID="LblNome" runat="server" class="card-text text-black"></asp:Label>
                            <asp:Label ID="LblPrezzo" runat="server" class="card-text text-black"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-4">
                    <div id="Card1" runat="server" class="card" visible="false" style="box-shadow: 0 9px 50px hsla(20, 67%, 75%, 0.31);">
                        <div class="card-body">
                            <asp:Label ID="Label1" runat="server" class="card-title text-black"></asp:Label>
                            <asp:Label ID="Label3" runat="server" class="card-text text-black"></asp:Label>
                        </div>
                    </div>
                </div>
                <div>
                <asp:DropDownList ID="DropDownStats" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownStats_SelectedIndexChanged">
    <asp:ListItem Text="Seleziona una statistica" Value=""></asp:ListItem>
    <asp:ListItem Text="Totale ordini effettuati" Value="TotalOrders"></asp:ListItem>
    <asp:ListItem Text="Totale prodotti venduti" Value="TotalProductsSold"></asp:ListItem>
    <asp:ListItem Text="Incasso totale" Value="TotalRevenue"></asp:ListItem>
    <asp:ListItem Text="Ordini per utente" Value="OrdersPerUser"></asp:ListItem>
</asp:DropDownList>

            </div>
                <div>
                 <asp:Label ID="LblResult" runat="server" ></asp:Label>
<asp:GridView ID="GridViewResults" runat="server"  AutoGenerateColumns="True">
</asp:GridView>

                </div>
                </div>
        </div>
    </main>
</asp:Content>
