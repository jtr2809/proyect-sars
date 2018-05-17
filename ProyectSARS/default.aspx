<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProyectSARS._default" UICulture="es" Culture="es-CL"%>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/styles.css" rel="stylesheet" />
    <link href="css/white.css" rel="stylesheet" />
    <title>SARS - Sistema de administración y reserva de salas de reunión</title>

</head>
<body>
    <form id="form1" runat="server">
        <div id="contenedor">

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <a href="default.aspx" id="logo">
                <span id="sars">SARS</span>
                <span id="texto">Sistema de administración y<br />reserva de salas de reunión</span>
            </a>

            <div id="user">
                <span id="usuario">Bienvenido: <span>
                    <asp:Label ID="lbUsuario" runat="server" Text=""></asp:Label></span></span><asp:LinkButton runat="server" Text="Cerrar Sesión" ID="btnCerrarSesion" OnClick="btnCerrarSesion_Click" CssClass="cerrar"></asp:LinkButton>
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
                <h2>Formulario de solicitud de sala</h2>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="f2">
                            <label class="lab1" for="">Ubicaciones</label>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="inp inp1" AutoPostBack="True" DataSourceID="ObjectDataSource1" DataTextField="DESCRIPCION" DataValueField="ID_UBICACION" OnDataBound="MyListDataBound">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtenerUbicacion" TypeName="ProyectSARS.BLL.UbicacionBLL"></asp:ObjectDataSource>
                        </div>


                        <div class="f2">
                            <label class="lab1" for="date">Filtro por Salas</label>
                            <asp:TextBox CssClass="inp" ID="txtFiltroSalas" runat="server" AutoPostBack="true" />
                        </div>

                        <div class="clear"></div>


                        <label class="lab1" for="time">Salas disponibles</label>

                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="hor-minimalist-b" DataKeyNames="idSala" AllowPaging="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" DataSourceID="ObjectDataSource2">
                            <Columns>
                                <asp:TemplateField HeaderText="Nombre Sala" SortExpression="nombreSala">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("nombreSala") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("nombreSala") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coordinador" SortExpression="IDCOORD">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("idCoord") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("idCoord") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Capacidad" SortExpression="CAPACIDAD">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("capacidad") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("capacidad") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Piso" SortExpression="PISO">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("piso") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("piso") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PC" SortExpression="POSEE_NOTE">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label6" runat="server" Text="PC" ToolTip="PC o Notebook"></asp:Label>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("pcNote") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <%# (Boolean.Parse(Eval("pcNote").ToString())) ? "Si" : "No" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pantalla" SortExpression="POSEE_MONITOR">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label7" runat="server" Text="Pantalla" ToolTip="Monitor o Pantalla"></asp:Label>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("monitor") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <%# (Boolean.Parse(Eval("monitor").ToString())) ? "Si" : "No" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VC" SortExpression="POSEE_VC">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label6" runat="server" Text="VC" ToolTip="Videoconferencia"></asp:Label>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox3" runat="server" Checked='<%# Bind("vc") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <%# (Boolean.Parse(Eval("vc").ToString())) ? "Si" : "No" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="True" SelectText="Ver Disponibilidad" />
                            </Columns>
                            <SelectedRowStyle BackColor="#99CCFF" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtenerSalasPorUbicacion" TypeName="ProyectSARS.BLL.SalaBLL" FilterExpression="nombreSala LIKE '{0}%'">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList1" Name="idUbicacion" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                            <FilterParameters>
                                <asp:ControlParameter Name="nombreSala" ControlID="txtFiltroSalas" PropertyName="Text" />
                            </FilterParameters>
                        </asp:ObjectDataSource>
                        <div class="f2">
                            <asp:Label ID="lbGvError" runat="server" Text="" CssClass="text-danger"></asp:Label>
                        </div>
                        <div class="clear"></div>
                        <DayPilot:DayPilotCalendar ID="DayPilotCalendar1" runat="server" Theme="white" Days="9" DataStartField="horaInicio" DataEndField="horaTermino" DataIdField="idReserva" DataValueField="idReserva" DataTextField="usuario" CssClassPrefix="white" StartDate="2018-04-26" BackColor="#FFFFD5" BorderColor="#000000" DataSourceID="ObjectDataSource3" DayFontFamily="Tahoma" DayFontSize="10pt" DurationBarColor="Blue" EventBackColor="#FFFFFF" EventBorderColor="#000000" EventClickHandling="Disabled" EventFontFamily="Tahoma" EventFontSize="8pt" EventHoverColor="#DCDCDC" HourBorderColor="#EAD098" HourFontFamily="Tahoma" HourFontSize="16pt" HourHalfBorderColor="#F3E4B1" HourNameBackColor="#ECE9D8" HourNameBorderColor="#ACA899" HoverColor="#FFED95" NonBusinessBackColor="#FFF4BC" ScrollPositionHour="8" BusinessBeginsHour="8" BusinessEndsHour="20" HeightSpec="BusinessHoursNoScroll"/>
                        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListaReservasPorSala" TypeName="ProyectSARS.BLL.ReservaBLL">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GridView1" Name="idSala" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="clear"></div>

                <div class="f2">
                    <label class="lab1" for="date">Fecha</label>
                    <asp:TextBox CssClass="inp" TextMode="Date" ID="txtFecha" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Campo Obligatorio" Display="Dynamic" ControlToValidate="txtFecha" CssClass="text-danger" ValidationGroup="nuevaReserva"></asp:RequiredFieldValidator>
                </div>

                <div class="f2">
                    <label class="lab1" for="time">Hora inicio</label>
                    <asp:TextBox CssClass="inp" TextMode="Time" ID="txtHoraInicio" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHoraInicio" CssClass="text-danger" Display="Dynamic" ErrorMessage="* Campo Obligatorio" ValidationGroup="nuevaReserva"></asp:RequiredFieldValidator>
                </div>

                <div class="f2">
                    <label class="lab1" for="time">Hora termino</label>
                    <asp:TextBox CssClass="inp" TextMode="Time" ID="txtHoraTermino" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtHoraTermino" CssClass="text-danger" Display="Dynamic" ErrorMessage="* Campo Obligatorio" ValidationGroup="nuevaReserva"></asp:RequiredFieldValidator>
                </div>

                <div class=" clear sep"></div>

                <ul id="bots">
                    <li>
                        <a><asp:Button ValidationGroup="nuevaReserva" ID="btnCrear" runat="server" Text="Enviar solicitud" CssClass="boton" OnClick="btnCrear_Click" OnClientClick="Confirm()" onMouseOver="this.className='boton2'" onMouseOut="this.className='boton'"/></a>
                    </li>
                    <li>
                        <a><asp:Button ID="btnVerSolicitudes" runat="server" Text="Ver Solicitudes" CssClass="boton" OnClick="btnVerSolicitudes_Click" onMouseOver="this.className='boton2'" onMouseOut="this.className='boton'"/></a>
                    </li>
                </ul>

            </div>

            <div class="clear"></div>

	<div id="footer">
		<img id="logo-footer" src="img/logo-footer.png" alt=""/>
		<p>Ministerio de las Culturas, las Artes y el Patrimonio</a> <br/>
<a href="*">Gobierno de Chile</a> <br/>
Contáctanos: <a href="*">Formulario de atención ciudadana</a> <br/>
<a href="*">Política de Privacidad</a></p> 
	</div>

            <script type="text/javascript">
                function Confirm() {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_value";
                    if (confirm("¿Desea confirmar la nueva reserva?")) {
                        confirm_value.value = "Si";
                    } else {
                        confirm_value.value = "No";
                    }
                    document.forms[0].appendChild(confirm_value);
                }
            </script>

        </div>
    </form>
</body>
</html>
