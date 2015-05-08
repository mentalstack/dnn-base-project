<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditFoo.ascx.cs" Inherits="DNNBase.EditFoo" %>

<%--Register--%>
<%@ Register TagPrefix="tlr" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/Controls/LabelControl.ascx" %>

<%--Import--%>
<%@ Import Namespace="DNNBase" %>
<%--Import--%>
<%@ Import Namespace="DotNetNuke.Services" %>
<%--Import--%>
<%@ Import Namespace="DotNetNuke.Services.Localization" %>
<%--Import--%>
<%@ Import Namespace="System.Data" %>

<tlr:RadScriptBlock ID="sbInit" runat="server">
    <script type="text/javascript">
        <%-- Here will be Javascript code --%>
    </script>
</tlr:RadScriptBlock>

<tlr:RadAjaxLoadingPanel ID="alpMain" Skin="Default" runat="server" />

<asp:PlaceHolder ID="phAjaxManager" runat="server">
    <%-- Here will be ajax manager --%>
</asp:PlaceHolder>

<tlr:RadAjaxPanel ID="apMain" runat="server">
    <div class="dnnForm dnnClear bseFoo bseScope">
        <tlr:RadCodeBlock runat="server">
            <h2 class="bseWelcome"><%= LocalizeString("Welcome.Title") %></h2>
        </tlr:RadCodeBlock>
        <div class="dnnFormItem">
            <%-- Dnn Label --%>
            <dnn:Label AssociatedControlID="nameFoo" ResourceKey="Name.Label" runat="server" />
            <%-- TextBox Control --%>
            <asp:TextBox
                ID="txtName"
                ValidationGroup="txtGroup"
                Visible="true"
                MaxLength="100"
                runat="server">
            </asp:TextBox>
            <%-- Validator --%>
            <asp:RequiredFieldValidator
                ID="rfv_1"
                CssClass="dnnFormMessage dnnFormError"
                ValidationGroup="txtGroup"
                Enabled="true"
                Display="Dynamic"
                ResourceKey="Edit.Required"
                ControlToValidate="nameFoo"
                runat="server">
            </asp:RequiredFieldValidator>
        </div>
        <br />
        <div class="dnnFormItem">
            <dnn:Label AssociatedControl ResourceKey="Desc.Label" runat="server" />
            <%-- TextBox Control --%>
            <asp:TextBox
                ID="txtDescription"
                ValidationGroup="txtGroup"
                Visible="true"
                MaxLength="100"
                runat="server">
            </asp:TextBox>
            <%-- Validator --%>
            <asp:RequiredFieldValidator
                ID="rfv_2"
                CssClass="dnnFormMessage dnnFormError"
                ValidationGroup="txtGroup"
                Enabled="true"
                Display="Dynamic"
                ResourceKey="Edit.Required"
                ControlToValidate="descFoo"
                runat="server">
            </asp:RequiredFieldValidator>
        </div>
        <br />
        <%-- Control --%>
        <asp:LinkButton ID="btnAdd" CssClass="dnnPrimaryAction" Text="Add(+)" ValidationGroup="txtGroup" OnClick="btnAdd_Click" runat="server"/>
        <%-- HyperLink --%>
        <asp:HyperLink ID="rtrn" CssClass="dnnSecondaryAction" Text="Return"  runat="server"/>
    </div>
</tlr:RadAjaxPanel>
