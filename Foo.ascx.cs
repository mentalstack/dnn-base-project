namespace DNNBase
{
    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using System;

    using System.Web.UI.WebControls;

    using Telerik.Web.UI;

    /// <summary>
    /// Foo module control.
    /// </summary>
    public partial class Foo : ModuleBase
    {
        #region Private Fields

        /// <summary>
        /// Rad AJAX manager.
        /// </summary>
        RadAjaxManager _ajaxManager = null;

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets url of module control.
        /// </summary>
        public static string GetControlUrl(string controlKey, params string[] parameters)
        {
            string url = DotNetNuke.Common.Globals.NavigateURL(controlKey, parameters);
            {
                return url; // return url of module control
            }
        }

        #endregion

        #region Protected Methods : Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/Foo.resx";

                ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/foo.js", FileOrder.Js.DefaultPriority + 1);
                {
                    ClientResourceManager.RegisterStyleSheet(Page, TemplateSourceDirectory + "/Css/foo.css", FileOrder.Css.DefaultPriority + 1);
                }

                base.OnInit(e);
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// Page_Load hadler.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try // try to handle Page_Load
            {
                _ajaxManager = null;

                if ((_ajaxManager = RadAjaxManager.GetCurrent(Page)) == null)
                {
                    _ajaxManager = new RadAjaxManager() { ID = "amMain", EnableAJAX = true };
                    {
                        phAjaxManager.Controls.Add(_ajaxManager);
                    }
                }

                var apMainSetting = new AjaxSetting("apMain");
                {
                    apMainSetting.UpdatedControls.Add(new AjaxUpdatedControl("apMain", "alpMain"));
                    {
                        _ajaxManager.AjaxSettings.Add(apMainSetting);
                    }
                }

                if (IsPostBack) return;

                string returnUrl = Request.RawUrl;

                hplAdd.NavigateUrl = GetControlUrl("EditFoo", "mid=" + ModuleId, "returnUrl=" + returnUrl); // init navUrl for HyperLink
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdFoo_NeedDataSource handler
        /// </summary>
        protected void grdFoo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try // try to handle grdFoo_OnNeedDataSource
            {
                string orderBy = "Name"; string orderDirection = "ASC";

                if (grdFoo.MasterTableView != null && grdFoo.MasterTableView.SortExpressions.Count > 0)
                {
                    GridSortExpression expression = grdFoo.MasterTableView.SortExpressions[0];

                    orderBy = expression.FieldName; // define order by options
                    {
                        orderDirection = expression.SortOrder == GridSortOrder.Descending ? "DESC" : "ASC"; // define sorting
                    }
                }

                int totalCount = -1, start = grdFoo.CurrentPageIndex * grdFoo.PageSize; // CurrentPageIndex at first == 0!

                grdFoo.DataSource = UnitOfWork.Foos.GetAllView(start, grdFoo.PageSize, orderBy, orderDirection, out totalCount); // get paged view

                grdFoo.VirtualItemCount = totalCount; // bind total count
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdFoo_ItemDataBound handler
        /// </summary>
        protected void grdFoo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try // try to handle grdFoo_ItemDataBound
            {
                if (e.Item.GetType() == typeof(GridDataItem))
                {
                    HyperLink hplink = (e.Item.FindControl("hlEdit") as HyperLink);

                    HyperLink addLink = (e.Item.FindControl("hlAdd") as HyperLink);

                    LinkButton delLink = (e.Item.FindControl("lbtnDelete") as LinkButton);

                    var foo = (e.Item.DataItem as DNNBase.Components.Entities.Foo);

                    string id = foo.KeyID.ToString();

                    string returnUrl = Request.RawUrl;
                    {
                        hplink.NavigateUrl = GetControlUrl("EditFoo", "id=" + id, "mid=" + ModuleId, "returnUrl=" + returnUrl); // init url by FooId param, if modify Foo

                        addLink.NavigateUrl = GetControlUrl("EditFoo", "mid=" + ModuleId, "returnUrl=" + returnUrl); // init url without FooId param, if create new Foo

                        DotNetNuke.UI.Utilities.ClientAPI.AddButtonConfirm(delLink, LocalizeString("Delete.Confirm")); // Add confirm to button before delete Foo
                    }
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdFoo_ItemCommand handler
        /// </summary>
        protected void grdFoo_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                int id;

                if ("Delete" == e.CommandName && Int32.TryParse(e.CommandArgument.ToString(), out id))
                    UnitOfWork.Foos.Delete(id);
            }
            catch (Exception exc) // catch exceptions
            {
                throw exc;
            }
        }

        #endregion

    }
}