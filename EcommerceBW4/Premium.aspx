<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Premium.aspx.cs" Inherits="EcommerceBW4.Premium"  MasterPageFile="~/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="main flow">
        <h1 class="main__heading">Pricing</h1>
        <div class="main__cards cards">
            <div class="cards__inner">
                <div class="card">
                    <h2 class="card__heading">Basic</h2>
                    <p class="card__price">&#8377;99.99</p>
                    <ul role="list" class="card__bullets flow">
                        <li>Piano base per spie basilari</li>
                        <li>Supporto Email</li>
                    </ul>
                    <a href="#basic" class="card__cta cta">Abbonati</a>
                </div>

                <div class="cards__card card">
                    <h2 class="card__heading">Pro</h2>
                    <p class="card__price">&#8377;399.99</p>
                    <ul role="list" class="card__bullets flow">
                        <li>Piano avanzato per veri 007.</li>
                        <li>Prioritá nel supporto email</li>
                        <li>Accesso esclusivo alle sessioni live di Q&A </li>
                    </ul>
                    <a href="#pro" class="card__cta cta">Passa a Pro</a>
                </div>

                <div class="cards__card card">
                    <h2 class="card__heading">Ultimate</h2>
                    <p class="card__price">&#8377;999.99</p>
                    <ul role="list" class="card__bullets flow">
                        <li>Solo per veri Jhon Wick</li>
                        <li>24/7 Supporto Prioritario</li>
                        <li>1-on-1 virtual coaching sessione mensile</li>
                        <li>Contenuti esclusivi e accesso anticipato sulle nuove uscite</li>
                    </ul>
                    <a href="#ultimate" class="card__cta cta">Go Ultimate</a>
                </div>
            </div>

            <div class="overlay cards__inner"></div>
        </div>
    </main>
</asp:Content>
