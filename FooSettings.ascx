<%--Import--%>
<%@ Import Namespace="DNNBase" %>
<%--Import--%>
<%@ Import Namespace="DotNetNuke.Services" %>
<%@ Import Namespace="DotNetNuke.Services.Localization" %>
<%--Import--%>
<%@ Import Namespace="System.Data" %>

<%--Control--%>
<%@ Control Language="C#" CodeBehind="FooSettings.ascx.cs" Inherits="DNNBase.FooSettings" %>

<%--Register--%>
<%@ Register TagPrefix="wth" Assembly="DNNBase" Namespace="DNNBase.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="tlr" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/Controls/LabelControl.ascx" %>

<div class="dnnForm dnnClear bseFooSettings bseScope">
	<div class="dnnFormItem">
		<dnn:Label ResourceKey="DefaultMessage.Label" runat="server" />
		<%-- Control --%>
		<asp:TextBox ID="txtDefaultMessage" Text="" TextMode="SingleLine" runat="server" />
		<%-- Validators --%>
		<asp:RequiredFieldValidator ID="rqvDefaultMessage" Display="Dynamic"
			ControlToValidate="txtDefaultMessage" CssClass="dnnFormMessage dnnFormError" ResourceKey="DefaultMessage.Required"
			runat="server" />
	</div>
</div>
