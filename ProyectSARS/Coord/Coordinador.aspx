<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Coordinador.aspx.cs" Inherits="ProyectSARS.Coord.Coordinador" UICulture="es" Culture="es-CL"%>

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


            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                <h2>Coordinador</h2>


                <div class="sep"></div>

                <label class="lab1" for="">Solicitudes</label>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvSolicitudes" runat="server" AutoGenerateColumns="False" CssClass="hor-minimalist-b" OnSelectedIndexChanged="GvSolicitudes_SelectedIndexChanged" DataKeyNames="IdReserva" OnPageIndexChanging="GvSolicitudes_PageIndexChanging" AllowPaging="True" PageSize="5" OnRowDataBound="gvSolicitudes_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="IdReserva" HeaderText="N° Reserva" />
                                <asp:BoundField DataField="idUsuario" HeaderText="Usuario" />
                                <asp:BoundField DataField="sala" HeaderText="Sala" />
                                <asp:BoundField DataField="fecha" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha" />
                                <asp:BoundField DataField="horaInicio" DataFormatString="{0:HH:mm}" HeaderText="Hora Inicio" />
                                <asp:BoundField DataField="horaTermino" DataFormatString="{0:HH:mm}" HeaderText="Hora Término" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Elegir"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#99CCFF" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListaReservasCoordinador" TypeName="ProyectSARS.BLL.ReservaBLL">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lbUsuario" Name="id_usuario" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>




                        <div class="clear"></div>


                        <div class="clear"></div>

                        <div class="sep"></div>


                        <h2>Datos Reserva</h2>

                        <div class="form1">

                            <div class="f3">
                                <label class="lab1" for="date">Nombre sala</label>
                                <asp:TextBox CssClass="inp inp3" TextMode="SingleLine" ID="txtNombreSala" runat="server" ReadOnly="True" />
                            </div>

                            <div class="f3">
                                <label class="lab1" for="date">Usuario</label>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox CssClass="inp inp3" TextMode="SingleLine" ID="txtUsuario" runat="server" ReadOnly="True" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <div class="clear"></div>


                            <div class="clear"></div>

                            <div class="f3">
                                <div class="f3">
                                    <asp:Label runat="server" ID="lbFecha" Text="Fecha: " CssClass="lab1"></asp:Label>
                                </div>
                                <div class="f3">
                                    <asp:Label runat="server" ID="lbHoraInicio" Text="Hora Inicio: " CssClass="lab1"></asp:Label>
                                </div>
                                <div class="f3">
                                    <asp:Label runat="server" ID="lbHoraTermino" Text="Hora Término: " CssClass="lab1"></asp:Label>
                                </div>
                            </div>

                            <div class="clear"></div>

                            <div class="f3">
                                <label class="lab1" for="time">Opciones</label>
                                <asp:DropDownList CssClass="inp inp3" runat="server" ID="ddlEstado" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" AutoPostBack="True" DataSourceID="ObjectDataSource2" DataTextField="NombreEstado" DataValueField="IdEstado">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtenerEstados" TypeName="ProyectSARS.BLL.ReservaBLL"></asp:ObjectDataSource>
                            </div>
                            <div class="f3">
                                <asp:label runat="server" cssclass="lab1" for="time" ID="lbMotivo" Visible="false">Motivo Rechazo</asp:label>
                                <asp:TextBox CssClass="inp inp1" runat="server" TextMode="MultiLine" ID="txtMotivoRechazo" Visible="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMotivoRechazo" CssClass="text-danger" Display="Dynamic" ErrorMessage="Campo Obligatorio" ValidationGroup="validate">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="clear"></div>
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

            <div class="clear"></div>

	<div id="footer">
		<img id="logo-footer" src="../img/logo-footer.png" alt=""/>
		<p>Ministerio de las Culturas, las Artes y el Patrimonio</a> <br/>
<a href="*">Gobierno de Chile</a> <br/>
Contáctanos: <a href="*">Formulario de atención ciudadana</a> <br/>
<a href="*">Política de Privacidad</a></p> 
	</div>


        </div>
    </form>
</body>
</html>
