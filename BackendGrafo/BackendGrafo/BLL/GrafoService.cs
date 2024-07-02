using BackendGrafo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendGrafo.BLL
{
    public class GrafoService
    {
       

        public string AgregarVertice(ciudad objInfo)
        {
            return GrafoRepository.AgregarVertice(objInfo);
        }

        public string AgregarArista(int VertOrign, int VertDest, float cost3)
        {
            return GrafoRepository.AgregarArista(VertOrign, VertDest, cost3);
        }

        public List<int> ObtenerAristas(int posiVert)
        {
            return GrafoRepository.MostrarAristasVertice(posiVert);
        }

        public List<ciudad> ObtenerVertices()
        {
            return GrafoRepository.MostrarVertices();
        }

        public List<int> RecorridoDFS(int start)
        {
            return GrafoRepository.RecorridoDFS(start);
        }

    }
}