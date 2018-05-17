<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="ProyectSARS.Logon" UICulture="es" Culture="es-CL"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/styles.css" rel="stylesheet" />
    <title>SARS - Sistema de administración y reserva de salas de reunión</title>
</head>
<body>
    <form id="Login" method="post" runat="server">
        <div id="login">


            <a href="#" id="logo">
                <span id="sars">SARS</span>
                <span id="texto">Sistema de administración y
                    <br />
                    reserva de salas de reunión</span>
            </a>

            <div class="clear"></div>
            <div id="log">
                <asp:Login ID="Login1" runat="server" OnLoginError="Login1_LoginError">
                    <LayoutTemplate>
                        <asp:TextBox ID="UserName" runat="server" CssClass="inp log1" TextMode="SingleLine" placeholder="nombre.apellido"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio." ValidationGroup="Login1" CssClass="text-danger">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="inp log2" placeholder="Contraseña"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria." ValidationGroup="Login1" CssClass="text-danger">*</asp:RequiredFieldValidator>
                        <div class="clear"></div>
                        <asp:CheckBox ID="RememberMe" runat="server" Text="Mantener la sesión iniciada." />
                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Inicio de sesión" ValidationGroup="Login1" CssClass="boton ingresar" onMouseOver="this.className='boton2 ingresar'" onMouseOut="this.className='boton ingresar'" /><br />
                        <span class="text-danger"><asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></span>
                    </LayoutTemplate>
                </asp:Login>
            </div>

	<div id="footer">
		<img id="logo-footer" src="img/logo-footer.png" alt=""/>
		<p>Ministerio de las Culturas, las Artes y el Patrimonio</a> <br/>
<a href="*">Gobierno de Chile</a> <br/>
Contáctanos: <a href="*">Formulario de atención ciudadana</a> <br/>
<a href="* ">Política de Privacidad</a></p> 
	</div>
        </div>
    </form>
</body>
</html>
