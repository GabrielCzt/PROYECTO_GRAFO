using BackendGrafo.BLL;
using BackendGrafo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace BackendGrafo.Presentation
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GrafoController : ApiController
    {
        private readonly GrafoService _grafoService = new GrafoService();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/grafo/agregar-vertice")]
        public IHttpActionResult AgregarVertice(ciudad objInfo)
        {
            var result = _grafoService.AgregarVertice(objInfo);
            return Ok(result);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/grafo/agregar-arista")]
        public IHttpActionResult AgregarArista([FromBody] AristaInputModel input)
        {
            if (input == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            var result = _grafoService.AgregarArista(input.VertOrign, input.VertDest, input.cost3);
            return Ok(result);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/grafo/obtener-aristas/{posiVert}")]
        public IHttpActionResult ObtenerAristas(int posiVert)
        {
            var result = _grafoService.ObtenerAristas(posiVert);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/grafo/obtener-vertices")]
        public IHttpActionResult ObtenerVertices()
        {
            var vertices = _grafoService.ObtenerVertices();
            if (vertices == null || vertices.Count == 0)
            {
                return NotFound(); // Devuelve 404 si no hay vértices encontrados
            }
            return Ok(vertices);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/grafo/dfs/{inicio}")]
        public IHttpActionResult RecorridoDFS(int inicio)
        {
            var result = _grafoService.RecorridoDFS(inicio);
            if (result == null || result.Count == 0)
                return NotFound();
            return Ok(result);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/grafo/bfs/{inicio}")]
        public IHttpActionResult RecorridoBFS(int inicio)
        {
            var result = _grafoService.RecorridoBFS(inicio);
            if (result == null || result.Count == 0)
                return NotFound();
            return Ok(result);
        }


    }
    public class AristaInputModel
    {
        public int VertOrign { get; set; }
        public int VertDest { get; set; }
        public float cost3 { get; set; }
    }
}
