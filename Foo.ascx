﻿<%--Control--%>
<%@ Control Language="C#" CodeBehind="Foo.ascx.cs" Inherits="DNNBase.Foo" %>

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
    <div class="bseFoo bseScope">
        <tlr:RadCodeBlock runat="server">
            <h2 class="bseWelcome"><%= LocalizeString("Welcome.Title") %></h2>
        </tlr:RadCodeBlock>
        <asp:Label ID="lblMessage" Text="" runat="server" />
    </div>
    <%-- GridView --%>
    <dnn:DnnGrid ID="grdFoo"
        CssClass="dnnGrid"
        AllowSorting="true"
        OnItemCommand="grdFoo_ItemCommand"
        OnNeedDataSource="grdFoo_NeedDataSource"
        OnItemDataBound="grdFoo_ItemDataBound"
        AllowPaging="true"
        AutoGenerateColumns="false"
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
                <dnn:DnnGridTemplateColumn HeaderText="Img">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink ID="hpLink" runat="server">
                            <dnn:DnnImage IconKey="Required" runat="server" />
                        </asp:HyperLink>
                    </ItemTemplate>
                </dnn:DnnGridTemplateColumn>
                <dnn:DnnGridTemplateColumn HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink ID="hlEdit" runat="server">
                            <dnn:DnnImage IconKey="Edit" runat="server" />
                        </asp:HyperLink>
                    </ItemTemplate>
                </dnn:DnnGridTemplateColumn>
                <dnn:DnnGridTemplateColumn HeaderText="Add">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink ID="hlAdd" runat="server">
                            <dnn:DnnImage IconKey="Add" runat="server" />
                        </asp:HyperLink>
                    </ItemTemplate>
                </dnn:DnnGridTemplateColumn>
                <dnn:DnnGridTemplateColumn HeaderText="Command">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton
                            ID="lbtnDelete"
                            CommandName="Delete"
                            CommandArgument='<%# Eval("FooId").ToString() %>'
                            EnableViewState="true" runat="server">
                            <dnn:DnnImage IconKey="Delete" runat="server" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </dnn:DnnGridTemplateColumn>
            </Columns>
        </MasterTableView>
    </dnn:DnnGrid>
    <br />
    <%-- Redirect to EditFoo control --%>
    <asp:HyperLink ID="hplAdd" Text="Add(+)" CssClass="dnnPrimaryAction" runat="server"/>
</tlr:RadAjaxPanel>
