<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FooView.ascx.cs" Inherits="DNNBase.FooView" %>

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
	<div class="dnnForm dnnClear bseFooView bseScope">
		<asp:PlaceHolder ID="phTemplate" runat="server">
			<%= EvaluateItem() %>
		</asp:PlaceHolder>
	</div>
</tlr:RadAjaxPanel>
