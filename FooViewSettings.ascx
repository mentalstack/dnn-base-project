<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FooViewSettings.ascx.cs" Inherits="DNNBase.FooViewSettings" %>

<%--Import--%>
<%@ Import Namespace="DNNBase" %>
<%--Import--%>
<%@ Import Namespace="DotNetNuke.Services" %>
<%--Import--%>
<%@ Import Namespace="DotNetNuke.Services.Localization" %>
<%--Import--%>
<%@ Import Namespace="System.Data" %>

<%--Register--%>
<%@ Register TagPrefix="wth" Assembly="DNNBase" Namespace="DNNBase.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="tlr" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/Controls/LabelControl.ascx" %>

<div class="dnnForm dnnClear bseFooSettings bseScope">
	<div class="dnnFormItem">
        <%--Dnn Label--%>
		<dnn:Label ResourceKey="DefaultMessage.Label" runat="server" />
        <%--DropDownList--%>
        <asp:DropDownList runat="server" ID="drpList">
            <asp:ListItem Text="Default" Value="Default" />
            <asp:ListItem Text="Toxic" Value="Toxic" />
        </asp:DropDownList>
        <br />
        <%--Label--%>
        <asp:Label ID="lblOut" runat="server" ></asp:Label>	
	</div>
</div>