<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Historial.aspx.cs" Inherits="ProyectSARS.Usuario.Historial" Culture="es-CL" %>

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
                <h2>Historial de reservas</h2>

                <div class="clear"></div>

                <div class="clear sep"></div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <asp:GridView ID="GridView1" runat="server" CssClass="hor-minimalist-b" AutoGenerateColumns="False" DataKeyNames="idReserva" AllowPaging="True" PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="idReserva" HeaderText="N° de Reserva" />
                                <asp:BoundField DataField="sala" HeaderText="Sala" />
                                <asp:BoundField DataField="fecha" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha" />
                                <asp:BoundField DataField="horaInicio" DataFormatString="{0:HH:mm}" HeaderText="Hora Inicio" />
                                <asp:BoundField DataField="horaTermino" DataFormatString="{0:HH:mm}" HeaderText="Hora Término" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" Visible="true"/>
                                <asp:TemplateField HeaderText="Activo" SortExpression="ACTIVO">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label7" runat="server" Text="Activo" ToolTip="Activo o Borrado"></asp:Label>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("activo") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <%# (Boolean.Parse(Eval("activo").ToString())) ? "Si" : "No" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False" HeaderText="Opciones">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Elegir"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#66CCFF" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListaReservasUsuario" TypeName="ProyectSARS.BLL.ReservaBLL">

                            <SelectParameters>
                                <asp:ControlParameter ControlID="lbUsuario" Name="id_usuario" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <ul id="bots">
                    <li>
                        <asp:Button CssClass="boton" NavigateUrl="~/default.aspx" runat="server" Text="Borrar" ID="btnBorrar" OnClick="btnBorrar_Click" OnClientClick="Confirm();"></asp:Button>
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
                    if (confirm("¿Desea borrar la sala seleccionada?")) {
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
