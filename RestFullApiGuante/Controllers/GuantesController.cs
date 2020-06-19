using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using ServiciosWeb.Datos.Modelo;

namespace RestFullApiGuantes.Controllers
{
    public class GuantesController : ApiController
    {
        // GET: api/Guantes
        [HttpGet]
        public IEnumerable<Guantes> LoadAllGloves()
        {
            using (DAM_JulenBartolome_DEVEntities entities = new DAM_JulenBartolome_DEVEntities())
            {
                var entity = entities.Guantes.ToList();
                return entity;
            }
        }


        // GET: api/Guantes/5
        [HttpGet]
        public HttpResponseMessage LoadAllGlovesById(int id)
        {
            using (DAM_JulenBartolome_DEVEntities entities = new DAM_JulenBartolome_DEVEntities())
            {
                var entity = entities.Guantes.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Los guantes con id = " + id.ToString() + " no existe");
                }
            }
        }

        // POST
        [HttpPost]
        public HttpResponseMessage AddNewGlove([FromBody] Guantes guantes)
        {
            try
            {
                using (DAM_JulenBartolome_DEVEntities entities = new DAM_JulenBartolome_DEVEntities())
                {
                    entities.Guantes.Add(guantes);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, guantes);
                    message.Headers.Location = new Uri(Request.RequestUri + guantes.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpDelete]
        public HttpResponseMessage DeleteById(int id)
        {
            try
            {
                using (DAM_JulenBartolome_DEVEntities entities = new DAM_JulenBartolome_DEVEntities())
                {
                    var entity = (entities.Guantes.FirstOrDefault(e => e.Id == id));
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Los guantes con el id = " + id.ToString() + " no se han encontrado");

                    }
                    else
                    {
                        entities.Guantes.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPut]
        public HttpResponseMessage UpdateGloveById([FromBody] int id, [FromUri] Guantes guantes)
        {
            try
            {
                using (DAM_JulenBartolome_DEVEntities entities = new DAM_JulenBartolome_DEVEntities())
                {
                    var entity = (entities.Guantes.FirstOrDefault(e => e.Id == id));
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Los guantes con el id = " + id.ToString() + " no se han encontrado");

                    }
                    else
                    {
                        entity.Nombre = guantes.Nombre;
                        entity.Color = guantes.Color;
                        entity.Precio = guantes.Precio;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }

}