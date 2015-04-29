namespace DNNBase
{
    using DotNetNuke.UI;
    using DotNetNuke.UI.Modules;

    using DotNetNuke.Web;
    using DotNetNuke.Web.Razor;

    using System.IO;

    /// <summary>
    /// Razor template evaluator.
    /// </summary>
    public class RazorEvaluator<T> where T: class
    {
        #region Protected Proeprties

        /// <summary>
        /// Gets or sets resource file.
        /// </summary>
        protected string LocalResourceFile { get; set; }

        #endregion

        #region Protected Proeprties : Context

        /// <summary>
        /// Gets or sets module isntance context.
        /// </summary>
        protected ModuleInstanceContext ModuleContext { get; set; }

        #endregion

        #region Public Methods : Evaluator

        /// <summary>
        /// Evaluates token replacement.
        /// </summary>
        public string Evaluate(T model, string template)
        {
            var razor = new RazorEngine(template, ModuleContext, LocalResourceFile);
            {
                var writer = new StringWriter(); razor.Render<T>(writer, model);
                {
                    return writer.ToString();
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public RazorEvaluator(ModuleInstanceContext context, string resource)
        {
            ModuleContext = context; LocalResourceFile = resource;
        }

        #endregion
    }
}