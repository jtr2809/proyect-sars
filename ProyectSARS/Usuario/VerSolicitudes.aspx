<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerSolicitudes.aspx.cs" Inherits="ProyectSARS.Usuario.VerSolicitudes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SARS - Sistema de administración y reserva de salas de reunión</title>
    <link href="../css/styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="contenedor">


            <a href="../default.aspx" id="logo">
                <span id="sars">SARS</span>
                <span id="texto">Sistema de administración y
                    <br>
                    reserva de salas de reunión</span>
            </a>

            <div id="user">
                <span id="usuario">Bienvenido: <span>
                    <asp:Label ID="lbUsuario" runat="server"></asp:Label></span></span>
                <asp:LinkButton runat="server" Text="Cerrar Sesión" ID="btnCerrarSesion" OnClick="btnCerrarSesion_Click" CssClass="cerrar"></asp:LinkButton>
                <%--<a class="lab1" href="../Admin/Admin.aspx">Administración</a><a class="lab1" href="../Coord/Coordinador.aspx">Coordinador</a>--%>
                <asp:TreeView ID="T1" DataSourceID="SiteMapDataSource1" runat="server" ImageSet="Arrows" EnableViewState="False">
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                </asp:TreeView>
                <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
            </div>

            <div class="clear"></div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div id="contenido">
                <h2>Ver Solicitudes Activas</h2>

                <div class="clear"></div>

                <div class="clear sep"></div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <asp:GridView ID="GridView1" runat="server" CssClass="hor-minimalist-b" AutoGenerateColumns="False" DataKeyNames="idReserva" AllowPaging="True" PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="idReserva" HeaderText="N° de Reserva" />
                                <asp:BoundField DataField="sala" HeaderText="Sala" />
                                <asp:BoundField DataField="fecha" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha" />
                                <asp:BoundField DataField="horaInicio" DataFormatString="{0:HH:mm}" HeaderText="Hora Inicio" />
                                <asp:BoundField DataField="horaTermino" DataFormatString="{0:HH:mm}" HeaderText="Hora Término" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" />
                                <asp:TemplateField HeaderText="Opciones" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Editar"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#66CCFF" />
                        </asp:GridView>

                        <h2>Datos Reserva</h2>

                        <div class="form1">

                            <div class="f3">
                                <label class="lab1" for="date">Nombre Sala</label>
                                <asp:TextBox CssClass="inp inp3" TextMode="SingleLine" ID="txtNombreSala" runat="server" ReadOnly="True" />
                            </div>
                            <div class="clear"></div>
                <div class="f2">
                    <label class="lab1" for="date">Fecha</label>
                    <asp:TextBox CssClass="inp" TextMode="Date" ID="txtFecha" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Campo Obligatorio" Display="Dynamic" ControlToValidate="txtFecha" CssClass="text-danger" ValidationGroup="nuevaReserva"></asp:RequiredFieldValidator>
                </div>

                <div class="f2">
                    <label class="lab1" for="time">Hora Inicio</label>
                    <asp:TextBox CssClass="inp" TextMode="Time" ID="txtHoraInicio" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHoraInicio" CssClass="text-danger" Display="Dynamic" ErrorMessage="* Campo Obligatorio" ValidationGroup="nuevaReserva"></asp:RequiredFieldValidator>
                </div>

                <div class="f2">
                    <label class="lab1" for="time">Hora Término</label>
                    <asp:TextBox CssClass="inp" TextMode="Time" ID="txtHoraTermino" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtHoraTermino" CssClass="text-danger" Display="Dynamic" ErrorMessage="* Campo Obligatorio" ValidationGroup="nuevaReserva"></asp:RequiredFieldValidator>
                </div>

                            <div class="clear"></div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <ul id="bots" style="margin-right: 40px;">
                    <li>
                        <a><asp:Button CssClass="boton" runat="server" Text="Guardar Cambios" ID="btnEditar" OnClick="btnEditar_Click" OnClientClick="Confirm()" onMouseOver="this.className='boton2'" onMouseOut="this.className='boton'" ValidationGroup="validate"/></a>
                    </li>
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

                    <script type="text/javascript">
                function Confirm() {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_value";
                    if (confirm("¿Desea guardar los cambios?")) {
                        confirm_value.value = "Si";
                    } else {
                        confirm_value.value = "No";
                    }
                    document.forms[0].appendChild(confirm_value);
                }
            </script>

    </form>

</body>
</html>
