namespace DNNBase.Components.Services
{
    using DNNBase.Components;
    using DNNBase.Components.Repositories;

    using DotNetNuke.Web.Api;

    using System.Net;
    using System.Net.Http.Formatting;
    using System.Net.Http;

    using System;

    using System.Web.Http;

    using DotNetNuke.Security;

    using DNNBase.Components.Entities;

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
        /// Format ok resopnse
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

        /// <summary>
        /// Add new foo
        /// </summary>
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public HttpResponseMessage Add([FromBody]Foo foo) 
        {
            try 
            {
                this.UnitOfWork.Foos.Add(foo.Name, foo.Description);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get foo by ID
        /// </summary>
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpGet]
        public HttpResponseMessage GetById(int fooId)
        {
            try
            {
                Foo foo = this.UnitOfWork.Foos.GetBy(fooId);
                return ResponseOK(foo);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get all foos
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var list = this.UnitOfWork.Foos.GetAll();
                return ResponseOK(list);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete foo by ID
        /// </summary>
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public HttpResponseMessage Delete(int fooId)
        {
            try
            {
                this.UnitOfWork.Foos.Delete(fooId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Update foo
        /// </summary>
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public HttpResponseMessage Update(Foo foo)
        {
            try
            {
                this.UnitOfWork.Foos.Update(foo.FooId, foo.Name, foo.Description);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion
    }
}