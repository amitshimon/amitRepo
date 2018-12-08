using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectServerSide.Controllers
{
    [RoutePrefix("api/entity")]
    [EnableCors("*","*",  "*")]
    public class EntityController : ApiController
    {
        #region Propertys
        private SourceData _sourceData;

        private Logger _logger;

        public SourceData SourceData
        {
            get
            {
                try
                {
                    if (_sourceData == null)
                    {
                        _sourceData = new SourceData();
                        return _sourceData;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Fatal("SourceData in EntityController" + ex.InnerException);
                }
                return _sourceData;
            }
        }

        public Logger Logger
        {
            get
            {
                try
                {
                    if (_logger == null)
                    {
                        _logger = new Logger();
                        return _logger;
                    }
                }
                catch (Exception ex)
                {
                   // Logger.Fatal("DeleteEntity function in SourceData" + ex.InnerException);
                }
                return _logger;
            }
        }
        #endregion
        
        // GET: api/Entity
        [HttpGet]
        [Route("getAllEntitys")]
        public HttpResponseMessage Get()
        {
            try
            {
                List<WebEntityModel> entitys = SourceData.GetAll().ToList();
                if (entitys != null && entitys.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entitys);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Happend");
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal("getAllEntitys function in EntityController" + ex.InnerException);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Happend" + ex.InnerException.ToString());
            }
        }

        // GET: api/Entity/5
        [HttpGet]
        [Route("getEntityById/{id:Guid}")]
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                var entity = SourceData.GetEntityById(id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Entity With That Id" + id.ToString() + "found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal("getEntityById function in EntityController" + ex.InnerException);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error");
            }
        }

        // POST: api/Entity
        [HttpPost]
        [Route("createEntity")]
        public HttpResponseMessage Post([FromBody]EntityModel value)
        {
            try
            {
                bool isSuccess = SourceData.CreateEntity(value);
                if (isSuccess)
                {
                    var message = Request.CreateResponse(HttpStatusCode.Created, value);
                    message.Headers.Location = new Uri(Request.RequestUri + value.id.ToString());
                    return message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No entity was created");
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal("createEntity function in EntityController" + ex.InnerException);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        // PUT: api/Entity/5
        [HttpPut]
        [Route("updateEntity/{id:Guid}")]
        public HttpResponseMessage Put(Guid id, [FromBody]EntityModel value)
        {
            try
            {
                bool isSuccess = SourceData.UpdateEntity(id, value);

                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, value);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Such Entity With id: " + id.ToString() + " Was Found");
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal("updateEntity function in EntityController" + ex.InnerException);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        // DELETE: api/Entity/5
        [HttpDelete]
        [Route("deleteEntity/{id:Guid}")]
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                bool isSuccess = SourceData.DeleteEntity(id);

                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Entity With id:" + id + " Dont Exsist");
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal("DeleteEntity function in EntityController" + ex.InnerException);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Happend" + ex.InnerException.ToString());
            }
        }
    }
}
