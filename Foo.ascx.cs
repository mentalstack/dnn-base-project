namespace DNNBase
{
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.UI.WebControls;
    using Telerik.Web.UI;

    /// <summary>
    /// Interview module control.
    /// </summary>
    public partial class Foo : ModuleBase
    {
        #region Defines

        public const string TEMPLATES_PATH = "~\\DesktopModules\\DNNBase\\Templates";

        #endregion

        #region Private Fields

        /// <summary>
        /// Rad AJAX manager.
        /// </summary>
        RadAjaxManager _ajaxManager = null;

        #endregion

        #region Private Fields : Templating

        /// <summary>
        /// Interview module settings.
        /// </summary>
        private Infrastructure.FooModuleSettings _settings = null;

        #endregion

        #region Private Fields : State



        #endregion

        #region Private Methods

        /// <summary>
        /// Converts url to relative.
        /// </summary>
        public string ToRelativeUrl(FileInfo source)
        {
            string result = source.FullName.Replace(Server.MapPath("~\\"), null);
            {
                return result.Replace(@"\", "/").Insert(0, "~/");
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
                _settings = Infrastructure.FooModuleSettings.Load(TabModuleId);
                {
                    LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/Foo.resx";
                }

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

                if (!String.IsNullOrEmpty(_settings.DefaultMessage))
                {
                    lblMessage.Text = _settings.DefaultMessage; lblMessage.Visible = true;
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion
    }
}
