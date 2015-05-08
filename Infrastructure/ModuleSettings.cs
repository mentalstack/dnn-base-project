namespace DNNBase.Infrastructure
{
    using DotNetNuke.Entities.Modules;

    using System;
    using System.Collections;

    /// <summary>
    /// Foo module settings.
    /// </summary>
    public class FooModuleSettings
    {
        #region Private Fields

        /// <summary>
        /// Module id
        /// </summary>
        private int _moduleId = -1;

        /// <summary>
        /// Default template skin
        /// </summary>
        private string _directory = "Default";

        #endregion

        #region Public Properties
        
        public string TemplateDirectory
        {
            get { return _directory; }
            set { _directory = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads module settings.
        /// </summary>
        public static FooModuleSettings Load(int moduleId)
        {
            var controller = new ModuleController();
            {
                return new FooModuleSettings(moduleId, controller.GetTabModule(moduleId).TabModuleSettings);
            }
        }

        /// <summary>
        /// Updates module settings.
        /// </summary>
        public void Update()
        {
            var controller = new ModuleController();

            if (!String.IsNullOrEmpty(TemplateDirectory))
            {
                controller.UpdateTabModuleSetting(_moduleId, "TemplateDirectory", TemplateDirectory);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        protected FooModuleSettings(int moduleId, Hashtable settings)
        {
            _moduleId = moduleId;

            if (settings.ContainsKey("TemplateDirectory")) 
            {
                TemplateDirectory = settings["TemplateDirectory"].ToString();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FooModuleSettings() { }

        #endregion
    }
}