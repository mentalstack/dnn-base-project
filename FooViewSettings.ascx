<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FooViewSettings.ascx.cs" Inherits="DNNBase.FooViewSettings" %>

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

<div class="dnnForm dnnClear bseFooSettings bseScope">
	<div class="dnnFormItem">
		<dnn:Label ResourceKey="TemplateDirectory.Label" runat="server" />
		<%-- Control --%>
		<dnn:DnnComboBox ID="cbTemplateDirectory"
			CssClass="wthCombobox" DataValueField="Name" DataTextField="Name"
			runat="server" />
		<%-- Validators --%>
		<asp:RequiredFieldValidator ID="rqvTemplateDirectory"
			ControlToValidate="cbTemplateDirectory" ValidationGroup="grpMain" CssClass="dnnFormMessage dnnFormError"
			ResourceKey="TemplateDirectory.Required" Display="Dynamic" Enabled="true"
			runat="server" />
	</div>
</div>
