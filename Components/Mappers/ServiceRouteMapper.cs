namespace DNNBase.Components.Mappers
{
    using DotNetNuke.Web.Api;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using System.Web.Http;

    /// <summary>
    /// Service route mapper.
    /// </summary>
    public class ServiceRouteMapper : IServiceRouteMapper
    {
        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public void RegisterRoutes(IMapRoute routeManager)
        {
            routeManager.MapHttpRoute("dnnbase", "default", "{controller}/{action}", new string[1]
            {
                "DNNBase.Components.Services"
            });
        }

        #endregion
    }
}