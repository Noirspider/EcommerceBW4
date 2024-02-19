<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EcommerceBW4.Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="style.css" />
    <link rel="stylesheet" href="./Content/Assets/css/Login.css" />
    <title>Log-in Form 2023</title>
    <script src="https://kit.fontawesome.com/2b9cdc1c9a.js" crossorigin="anonymous"></script>
</head>
<body>
    <div class="overlay">
        <form id="form1" runat="server">
            <div class="con">
                <header class="head-form">
                    <h2>Log-In</h2>
                    <p>Log-in here using your username and password</p>
                    <p>Or Insert username and password and click Sign Up</p>
                </header>
                <br />
                <div class="field-set">
                    <asp:TextBox ID="txtEta" CssClass="form-input" TextMode="SingleLine" placeholder="Età" runat="server" />

                    <span class="input-item">
                        <i class="fa fa-user-circle"></i>
                    </span>
                    <input class="form-input" type="text" placeholder="@UserName" id="txtUsername" required runat="server" />

                    <br />
                    <span class="input-item">
                        <i class="fa fa-key"></i>
                    </span>
                    <input class="form-input" type="password" placeholder="Password" id="txtPassword" required autocomplete="on" runat="server" />           
                    <span>
                        <i class="fa fa-eye" aria-hidden="true" id="eye"></i>
                    </span>
                    <br />
                    <button class="log-in" runat="server" onserverclick="Login_Click">Log In</button>
                </div>
                <div class="other">
                    <button class="btn submits frgt-pass" runat="server" onserverclick="ForgotPassword_Click">Forgot Password</button>
                    <button class="btn submits sign-up" runat="server" onserverclick="SignUp_Click">
                        Sign Up
                        <i class="fa fa-user-plus" aria-hidden="true"></i>
                    </button>
                </div>
                <asp:Label Text="text" ID="lblError" runat="server" Visible="False" CssClass="error" />
            </div>
        </form>
    </div>

    <script src="Scripts\Login.js"></script>
</body>
</html>
