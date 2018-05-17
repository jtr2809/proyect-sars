<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarSala.aspx.cs" Inherits="ProyectSARS.Admin.EditarSala" UICulture="es" Culture="es-CL"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SARS - Sistema de administración y reserva de salas de reunión</title>
    <link href="../css/styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="contenedor">

            <asp:HiddenField ID="hdfIDSala" runat="server" />

            <a href="../default.aspx" id="logo">
                <span id="sars">SARS</span>
                <span id="texto">Sistema de administración y
                    <br />
                    reserva de salas de reunión</span>
            </a>

            <div id="user">
                <span id="usuario">Bienvenido: <span>
                    <asp:Label ID="lbUsuario" runat="server"></asp:Label></span></span>
                <asp:LinkButton runat="server" Text="Cerrar Sesión" ID="btnCerrarSesion" OnClick="btnCerrarSesion_Click" CssClass="cerrar"></asp:LinkButton>
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
                <h2>Salas</h2>


                <div class="sep"></div>


                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="f1">
                            <label class="lab1" for="">Salas disponibles</label>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="inp inp2" AutoPostBack="True" DataSourceID="ObjectDataSource1" DataTextField="DESCRIPCION" DataValueField="ID_UBICACION"></asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtenerUbicacion" TypeName="ProyectSARS.BLL.UbicacionBLL"></asp:ObjectDataSource>
                        </div>

                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2" CssClass="hor-minimalist-b" DataKeyNames="idSala" AllowPaging="True" PageSize="7" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
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
                                <asp:TemplateField HeaderText="Equipamiento" SortExpression="EQUIPAMIENTO">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("equipamiento") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("equipamiento") %>'></asp:Label>
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
                                        <asp:Label ID="Label7" runat="server" Text="Mntr" ToolTip="Monitor o Pantalla"></asp:Label>
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
                                <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar"/>
                            </Columns>
                            <SelectedRowStyle BackColor="#99CCFF" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtenerSalasPorUbicacion" TypeName="ProyectSARS.BLL.SalaBLL">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList1" Name="idUbicacion" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <div class="clear"></div>



                        <div class="sep"></div>


                        <h2>Editar Salas</h2>
                        <div class="form1">

                            <div class="f3">

                                <label class="lab1" for="date">Nombre sala</label>
                                <asp:TextBox CssClass="inp inp3" TextMode="SingleLine" ID="txtNombreSala" runat="server" />
                            </div>

                            <div class="f3">
                                <label class="lab1" for="date">Coordinador</label>

                                <asp:TextBox CssClass="inp inp3" TextMode="SingleLine" ID="txtCoordinador" runat="server" AutoPostBack="true" OnTextChanged="txtCoordinador_TextChanged" placeholder="nombre.apellido"/>
                                <asp:Label ID="lbOk" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="clear"></div>

                            <div class="f3">
                                <label class="lab1" for="">Ubicaciones</label>
                                <asp:DropDownList ID="ddlUbicaciones" CssClass="inp inp2" runat="server" DataSourceID="ObjectDataSource1" DataTextField="DESCRIPCION" DataValueField="ID_UBICACION"></asp:DropDownList>
                            </div>


                            <div class="f3">
                                <label class="lab1" for="date">Piso</label>
                                <asp:TextBox CssClass="inp inp3" TextMode="Number" ID="txtPiso" runat="server" />
                            </div>

                            <div class="clear"></div>

                            <div class="f3">
                                <label class="lab1" for="time">Estado</label>
                                <asp:DropDownList CssClass="inp inp3" runat="server" ID="ddlEstado">
                                    <asp:ListItem Selected="True" Value="1">Habilitada</asp:ListItem>
                                    <asp:ListItem Value="0">Deshabilitada</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="clear"></div>

                            <div class="f3">
                                <label class="lab1" for="time">Equipamiento</label>
                                <asp:TextBox CssClass="inp inp1" runat="server" TextMode="MultiLine" ID="txtEquipamiento"></asp:TextBox>
                            </div>

                            <div class="f3">
                                <label class="lab1" for="">Capacidad</label>
                                <asp:TextBox ID="txtCapacidad" TextMode="Number" runat="server" CssClass="inp inp3"></asp:TextBox>
                            </div>
                            <div class="clear"></div>

                            <div class="f3">
                                <span class="sep2">
                                    <asp:CheckBox ID="chkNote" runat="server" Text="Notebook - PC" /></span>
                                <span class="sep2">
                                    <asp:CheckBox ID="chkVC" runat="server" Text="Videoconferencia" /></span>
                                <span class="sep2">
                                    <asp:CheckBox ID="chkMNTRPantalla" runat="server" Text="Pantalla - Monitor" /></span>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <ul id="bots" style="margin-right: 40px;">
                    <li>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnEditar_Click" OnClientClick="Confirm()" CssClass="boton" onMouseOver="this.className='boton2'" onMouseOut="this.className='boton'"/>
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
