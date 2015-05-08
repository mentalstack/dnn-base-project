namespace DNNBase.Components.Services
{
    using DotNetNuke.Web.Api;

    using DNNBase.Components;
    using DNNBase.Components.Repositories;

    using System.Net;

    using System.Net.Http;
    using System.Net.Http.Formatting;

    /// <summary>
    /// Web Api controller.
    /// </summary>
    public class WebController : DnnApiController
    {
        #region Private Fields

        /// <summary>
        /// Unit of work instance.
        /// </summary>
        private UnitOfWork _uow = new UnitOfWork();

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets unit of work.
        /// </summary>
        protected UnitOfWork UnitOfWork
        {
            get { return _uow; }
        }

        #endregion

        #region Private Methods : Helpers

        /// <summary>
        /// Format OK resopnse
        /// </summary>
        private HttpResponseMessage ResponseOK()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Format OK resopnse
        /// </summary>
        private HttpResponseMessage ResponseOK(object data)
        {
            JsonMediaTypeFormatter formatter = Configuration.Formatters.JsonFormatter;
            {
                return Request.CreateResponse(HttpStatusCode.OK, data, formatter); // return response
            }
        }

        #endregion

        #region Public Methods

        // your awesome web-api here

        #endregion
    }
}