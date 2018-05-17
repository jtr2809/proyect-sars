<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ubicaciones.aspx.cs" Inherits="ProyectSARS.Admin.Ubicaciones" UICulture="es" Culture="es-CL"%>

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
	<span id="usuario">Bienvenido: <span><asp:Label ID="lbUsuario" runat="server"></asp:Label></span></span> <asp:LinkButton runat="server" Text="Cerrar Sesión" ID="btnCerrarSesion" OnClick="btnCerrarSesion_Click" CssClass="cerrar"></asp:LinkButton>
    
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
<h2>Ubicaciones</h2>


<div class="sep"></div>



<div id="izq">
	<label class="lab1" for="">Agregar nueva ubicación</label>
    <asp:TextBox CssClass="inp inp1" ID="txtDescripcion" runat="server" TextMode="MultiLine"></asp:TextBox>

	<div class="clear"></div><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Obligtorio" ControlToValidate="txtDescripcion" CssClass="text-danger" ValidationGroup="validate"></asp:RequiredFieldValidator>
	<asp:Button CssClass="boton" style="float: left;" runat="server" ID="btnAgregar" Text="Agregar Ubicación" OnClick="btnAgregar_Click" OnClientClick="Confirm()" onMouseOver="this.className='boton2'" onMouseOut="this.className='boton'" ValidationGroup="validate"></asp:Button>
</div>

<div id="der">
	<label class="lab1" for="">Ubicaciones</label>
    <asp:GridView ID="GridView1" runat="server" CssClass="hor-minimalist-b" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" DataKeyNames="ID_UBICACION">
        <Columns>
            <asp:BoundField DataField="ID_UBICACION" HeaderText="ID_UBICACION" SortExpression="ID_UBICACION" Visible="False" />
            <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" SortExpression="DESCRIPCION" />
            <asp:CommandField HeaderText="Opciones" ShowSelectButton="True" SelectText="Seleccionar"/>
        </Columns>
        <SelectedRowStyle BackColor="#3399FF" />
    </asp:GridView>
    <div class="clear"></div>
    <asp:Button ID="btnBorrar" runat="server" Text="Borrar Seleccionado" CssClass="boton" OnClick="btnBorrar_Click" OnClientClick="Confirm2()" onMouseOver="this.className='boton2'" onMouseOut="this.className='boton'"/>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtenerUbicacion" TypeName="ProyectSARS.BLL.UbicacionBLL"></asp:ObjectDataSource>
</div>




<div class="clear"></div>
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
            if (confirm("¿Desea guardar la nueva ubicación?")) {
                confirm_value.value = "Si";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function Confirm2() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("¿Desea borrar la ubicación seleccionada? Esto también borrará las salas de la ubicación, pero no las reservas.")) {
                confirm_value.value = "Si";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</body>
</html>
