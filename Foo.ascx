<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Foo.ascx.cs" Inherits="DNNBase.Foo" %>

<%--Register--%>
<%@ Register TagPrefix="gmf" Assembly="DNNBase" Namespace="DNNBase.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="tlr" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/Controls/LabelControl.ascx" %>

<%--Import--%>
<%@ Import Namespace="DNNBase" %>
<%--Import--%>
<%@ Import Namespace="DotNetNuke.Services" %>
<%@ Import Namespace="DotNetNuke.Services.Localization" %>
<%--Import--%>
<%@ Import Namespace="System.Data" %>

<asp:PlaceHolder ID="phAjaxManager" runat="server">
	<%-- Here will be ajax manager --%>
</asp:PlaceHolder>

<tlr:RadAjaxLoadingPanel ID="alpMain" Skin="Default" runat="server" />

<tlr:RadAjaxPanel ID="apMain" runat="server">
	<div class="bseFoo bseScope">
		<div class="dnnRight">
			<asp:HyperLink ID="btnAdd" CssClass="dnnPrimaryAction"
				ResourceKey="Add" runat="server" />
		</div>
		<div class="dnnClear">
			<dnn:DnnGrid ID="grdFoo" CssClass="dnnGrid" AllowSorting="true"
				OnItemCommand="grdFoo_ItemCommand" OnNeedDataSource="grdFoo_NeedDataSource" OnItemDataBound="grdFoo_ItemDataBound"
				AllowPaging="true" AutoGenerateColumns="false"
				runat="server">
				<MasterTableView>
					<HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
					<SortExpressions>
						<tlr:GridSortExpression FieldName="Name" SortOrder="Ascending" />
					</SortExpressions>
					<Columns>
						<dnn:DnnGridBoundColumn DataField="Name" HeaderText="FooName">
							<ItemStyle HorizontalAlign="Center" />
						</dnn:DnnGridBoundColumn>
						<dnn:DnnGridBoundColumn DataField="Description" HeaderText="FooDescription">
							<ItemStyle HorizontalAlign="Center" />
						</dnn:DnnGridBoundColumn>
						<dnn:DnnGridTemplateColumn HeaderText="Edit">
							<ItemStyle HorizontalAlign="Center" />
							<ItemTemplate>
								<asp:HyperLink ID="hlEdit" runat="server">
									<dnn:DnnImage IconKey="Edit" ResourceKey="Edit" runat="server" />
								</asp:HyperLink>
							</ItemTemplate>
						</dnn:DnnGridTemplateColumn>
						<dnn:DnnGridTemplateColumn HeaderText="Delete">
							<ItemStyle HorizontalAlign="Center" />
							<ItemTemplate>
								<asp:LinkButton ID="lbDelete"
									CommandName="Delete" CommandArgument='<%# Eval("FooId").ToString() %>'
									EnableViewState="true" runat="server">
										<dnn:DnnImage IconKey="Delete"  ResourceKey="Delete" runat="server" />
								</asp:LinkButton>
							</ItemTemplate>
						</dnn:DnnGridTemplateColumn>
					</Columns>
				</MasterTableView>
			</dnn:DnnGrid>
		</div>
	</div>
</tlr:RadAjaxPanel>
