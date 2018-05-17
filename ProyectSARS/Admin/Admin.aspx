<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ProyectSARS.Admin.Admin" UICulture="es" Culture="es-CL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>SARS - Sistema de administración y reserva de salas de reunión</title>
    <link href="../css/styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        

        <div id="contenedor">



<a href="#" id="logo">
	<span id="sars">SARS</span>
	<span id="texto">Sistema de administración y <br>reserva de salas de reunión</span>
</a>

<div id="user">
	<span id="usuario">Bienvenido: <span><asp:Label ID="lbUsuario" runat="server" Text=""></asp:Label></span></span> <asp:LoginStatus ID="LoginStatus1" runat="server" CssClass="cerrar" />
    <%--<asp:LinkButton runat="server" Text="Cerrar Sesión" ID="btnCerrarSesion" OnClick="btnCerrarSesion_Click" CssClass="cerrar"></asp:LinkButton>--%>
    <%--<asp:HyperLink CssClass="lab1" runat="server" NavigateUrl="~/Admin/Admin.aspx">Administración</asp:HyperLink><asp:HyperLink CssClass="lab1" runat="server" NavigateUrl="~/Coord/Coordinador.aspx">Coordinador</asp:HyperLink>--%>
    <asp:TreeView ID="T1" DataSourceID="SiteMapDataSource1" runat="server" ImageSet="Arrows" EnableViewState="False">
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                </asp:TreeView>
                <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
</div>

<div class="clear"></div>

<div id="contenido">
<h2>Agregar Salas</h2>

<div class="sep"></div>


<div class="formleft">

<div class="f3">
		<label class="lab1" for="date">Nombre sala</label>
		
    <asp:TextBox ID="txtNombreSala" runat="server" CssClass="inp inp3" TextMode="SingleLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Campo Obligatorio" Display="Dynamic" ControlToValidate="txtNombreSala" ValidationGroup="nuevaSala" CssClass="text-danger"></asp:RequiredFieldValidator>
	</div>
	<div class="f3">
		<label class="lab1" for="">Capacidad</label>
        <asp:TextBox ID="txtCapacidad" runat="server" CssClass="inp inp4" TextMode="Number"></asp:TextBox>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="* Solo se aceptan valores entre 0 y 9999" MaximumValue="9999" MinimumValue="0" Type="Integer" ControlToValidate="txtCapacidad" CultureInvariantValues="False" ValidationGroup="nuevaSala" CssClass="text-danger" Display="Dynamic"></asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Campo Obligatorio" Display="Dynamic" ControlToValidate="txtCapacidad" ValidationGroup="nuevaSala" CssClass="text-danger"></asp:RequiredFieldValidator>

    </div>
    <div class="f3">
        <label class="lab1" for="date">Coordinador</label>
        <asp:TextBox CssClass="inp inp3" TextMode="SingleLine" ID="txtCoordinador" runat="server" AutoPostBack="true" OnTextChanged="txtCoordinador_TextChanged" placeholder="nombre.apellido"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Campo Obligatorio" Display="Dynamic" ControlToValidate="txtCoordinador" ValidationGroup="nuevaSala" CssClass="text-danger"></asp:RequiredFieldValidator>
        <asp:Label ID="lbError" runat="server" CssClass="text-danger"></asp:Label>
    </div>
    <div class="clear"></div>

    <div class="f3">
        <label class="lab1" for="">Ubicaciones</label>
        <asp:DropDownList ID="ddlUbicaciones" runat="server" DataSourceID="ObjectDataSource1" DataTextField="DESCRIPCION" DataValueField="ID_UBICACION" CssClass="inp inp2"></asp:DropDownList>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtenerUbicacion" TypeName="ProyectSARS.BLL.UbicacionBLL"></asp:ObjectDataSource>
    </div>


    <div class="f3">
		<label class="lab1" for="date">	Piso</label>
		<asp:TextBox CssClass="inp inp2" TextMode="Number" id="txtPiso" runat="server"/>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Campo Obligatorio" ControlToValidate="txtPiso" Display="Dynamic" ValidationGroup="nuevaSala" CssClass="text-danger"></asp:RequiredFieldValidator>
	</div>

	<div class="clear"></div>

	<div class="f3">
		<label class="lab1" for="time">Equipamiento</label>
		<asp:TextBox CssClass="inp inp5" TextMode="MultiLine" id="txtEquipamiento" runat="server" MaxLength="1024"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Campo Obligatorio" Display="Dynamic" ControlToValidate="txtEquipamiento" ValidationGroup="nuevaSala" CssClass="text-danger"></asp:RequiredFieldValidator>
	</div>

	<div class="f3" style="margin: 40px 0 0 0;">
        <asp:CheckBox ID="chkNotePC" runat="server" Text="PC / Notebook "/><br>
        <asp:CheckBox ID="chkMonitorPantalla" runat="server" Text="Monitor / Pantalla"/><br>
        <asp:CheckBox ID="chkVC" Text="Videoconferencia" runat="server" />
	</div>


</div>





<div class="clear"></div>




<ul id="bots">
	<li><asp:Button CssClass="boton" runat="server" Text="Agregar Sala" ID="txtAgregarSala" OnClick="txtAgregarSala_Click" OnClientClick="Confirm()" ValidationGroup="nuevaSala" onMouseOver="this.className='boton2'" onMouseOut="this.className='boton'"></asp:Button></li>
	<li><asp:Button CssClass="boton" runat="server" Text="Ver Salas" ID="txtEditar" OnClick="txtEditar_Click" onMouseOver="this.className='boton2'" onMouseOut="this.className='boton'"></asp:Button></li>
</ul>



</div>




<div class="clear"></div>

	<div id="footer">
		<img id="logo-footer" src="../img/logo-footer.png" alt=""/>
		<p>Ministerio de las Culturas, las Artes y el Patrimonio</a> <br/>
<a href="http://www.gob.cl">Gobierno de Chile</a> <br/>
Contáctanos: <a href="http://formulariosiac.cultura.gob.cl/">Formulario de atención ciudadana</a> <br/>
<a href="http://www.cultura.gob.cl/politica-de-privacidad/">Política de Privacidad</a></p> 
	</div>


	</div>
    </form>

    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("¿Desea guardar la nueva sala? Porfavor revise los campos antes de continuar")) {
                confirm_value.value = "Si";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</body>
</html>
