<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditFoo.ascx.cs" Inherits="DNNBase.EditFoo" %>

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
	<div class="dnnForm dnnClear bseFoo bseScope">
		<tlr:RadCodeBlock runat="server">
			<h2 class="bseWelcome"><%= LocalizeString("Welcome.Title") %></h2>
		</tlr:RadCodeBlock>
		<div class="dnnFormItem">
			<%-- Dnn Label --%>
			<dnn:Label AssociatedControlID="nameFoo" ResourceKey="Name.Label" runat="server" />
			<%-- TextBox Control --%>
			<asp:TextBox ID="txtName"
				ValidationGroup="grpMain" Visible="true"
				MaxLength="100" runat="server">
			</asp:TextBox>
			<%-- Validator --%>
			<asp:RequiredFieldValidator ID="rfvName"
				CssClass="dnnFormMessage dnnFormError" ValidationGroup="grpMain"
				Enabled="true" Display="Dynamic" ResourceKey="Name.Required" ControlToValidate="txtName"
				runat="server" />
		</div>
		<br />
		<div class="dnnFormItem">
			<dnn:Label AssociatedControl ResourceKey="Description.Label" runat="server" />
			<%-- TextBox Control --%>
			<asp:TextBox ID="txtDescription"
				ValidationGroup="grpMain" Visible="true"
				MaxLength="100" runat="server">
			</asp:TextBox>
			<%-- Validator --%>
			<asp:RequiredFieldValidator ID="rfvDescription"
				CssClass="dnnFormMessage dnnFormError" ValidationGroup="grpMain"
				Enabled="true" Display="Dynamic" ResourceKey="Description.Required" ControlToValidate="txtDescription"
				runat="server" />
		</div>
		<%-- ID hidden field --%>
		<asp:HiddenField ID="hfId" Value="-1" runat="server" />
		<%-- Actions--%>
		<ul class="dnnActions dnnClear">
			<li>
				<asp:LinkButton ID="btnPrimary" CssClass="dnnPrimaryAction"
					OnClick="btnPrimary_OnClick" ValidationGroup="grpMain" CausesValidation="true" CommandName="Save"
					ResourceKey="Save" runat="server" />
			</li>
			<li>
				<asp:HyperLink ID="btnCancel" CssClass="dnnSecondaryAction"
					NavigateUrl="javascript:void(0)" EnableViewState="true" ResourceKey="Cancel"
					runat="server" />
			</li>
		</ul>
	</div>
</tlr:RadAjaxPanel>
