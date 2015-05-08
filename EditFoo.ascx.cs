namespace DNNBase
{
    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using System;

    using Telerik.Web.UI;

    /// <summary>
    /// EditFoo Module
    /// </summary>
    public partial class EditFoo : ModuleBase
    {
        #region Private Fields

        /// <summary>
        /// Rad AJAX manager.
        /// </summary>
        private RadAjaxManager _ajaxManager = null;

        #endregion

        #region Protected Methods : Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/EditFoo.resx";

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
        /// Page_Load handler.
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

                hlReturn.NavigateUrl = Request.QueryString["returnUrl"].ToString(); // Back to the Past

                int id = -1;
                string queryId = Request.QueryString["id"] ?? "";

                if (Int32.TryParse(queryId, out id))
                {
                    DNNBase.Components.Entities.Foo foo = UnitOfWork.Foos.GetBy(id);

                    txtName.Text = foo.Name;
                    txtDescription.Text = foo.Description;
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// btnAdd_Click handler.
        /// </summary>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            int id = -1;
            string queryId = Request.QueryString["id"] ?? "";

            if (Int32.TryParse(queryId, out id))
            {
                UnitOfWork.Foos.Update(id, txtName.Text, txtDescription.Text);
            }
            else
            {
                UnitOfWork.Foos.Add(txtName.Text, txtDescription.Text);
            }

            Response.Redirect(Request.QueryString["returnUrl"].ToString()); // Back to the Past
        }
    }

        #endregion
}