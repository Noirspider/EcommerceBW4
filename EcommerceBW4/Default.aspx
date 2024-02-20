<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EcommerceBW4._Default"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-10">
                <main>
                    <!-- Cards -->
                    <div class="row row-cols-xs-1 row-cols-md-2 row-cols-lg-5 row-cols-xl-6">
                        <asp:Repeater ID="prodottiRepeater" runat="server">
                            <ItemTemplate>
                                <div class="col mb-4">
                                    <div class="card text-white bg-black mb-3 h-100" style="box-shadow: 0 9px 50px hsla(20, 67%, 75%, 0.31);">
                                        <div style="max-width: 100%; max-height: 10rem; overflow: hidden;">
                                            <img src='<%# Eval("ImmagineURL") %>' class="card-img-top w-100 h-100" alt="ProductImg">
                                        </div>
                                        <div class="card-body">
                                            <h5 class="card-title"><%# Eval("Nome") %></h5>
                                            <p>Prezzo: <i><%#Eval("Prezzo") %>&euro;</i></p>
                                            <!-- Bottoni della card -->
                                            <asp:LinkButton ID="ToDetail" runat="server" CommandArgument='<%# Eval("ProdottoID") %>' OnCommand="ToDetail_Command" CssClass="btn btn-light" Style="background-color: #5cdb95;"> 
                                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-info-lg" viewBox="0 0 16 16">
                                                  <path d="m9.708 6.075-3.024.379-.108.502.595.108c.387.093.464.232.38.619l-.975 4.577c-.255 1.183.14 1.74 1.067 1.74.72 0 1.554-.332 1.933-.789l.116-.549c-.263.232-.65.325-.905.325-.363 0-.494-.255-.402-.704zm.091-2.755a1.32 1.32 0 1 1-2.64 0 1.32 1.32 0 0 1 2.64 0"/>
                                                </svg>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="AddCart" CommandArgument='<%# Eval("ProdottoID") %>' runat="server" OnCommand="AddCart_OnClickButton" CssClass="btn btn-light" Style="background-color: #5cdb95;">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-bag-plus-fill" viewBox="0 0 16 16">
                                                    <path fill-rule="evenodd" d="M10.5 3.5a2.5 2.5 0 0 0-5 0V4h5zm1 0V4H15v10a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2V4h3.5v-.5a3.5 3.5 0 1 1 7 0M8.5 8a.5.5 0 0 0-1 0v1.5H6a.5.5 0 0 0 0 1h1.5V12a.5.5 0 0 0 1 0v-1.5H10a.5.5 0 0 0 0-1H8.5z"/>
                                                </svg>
                                            </asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!-- Fine Card -->
                </main>
            </div> 
            <!-- Side per pubblicità -->
            <div class="col-md-2 d-flex justify-content-center sticky-top ">
                <div class="aligh-content-center" style="text-align:center">
                    <h6>Benvenuto <span style="color: #5cdb95" ID="helloUser" runat="server"><%# Session["Username"] %></span>,</h6>
                    <p>Dai un'occhiata ai nostri partner!</p>
                    <div>
                        <img src="Content/Assets/images/detectiveaddsquare.png" alt="Banner 1" class="img-fluid mb-3">
                    </div>
                </div>
            </div>
            <!-- Fine side -->
        </div>
    </div>

</asp:Content>
