using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendGrafo.DAL
{
    public static class GrafoRepository
    {
        private static List<Vertice> ListaAdyacencia = new List<Vertice>();

        public static string AgregarVertice(ciudad objInfo)
        {
            ListaAdyacencia.Add(new Vertice(objInfo));
            return "Insertado";
        }

        public static string AgregarArista(int VertOrign, int VertDest, float cost3)
        {
            if (VertOrign >= 0 && VertOrign < ListaAdyacencia.Count && VertDest >= 0 && VertDest < ListaAdyacencia.Count)
            {
                ListaAdyacencia[VertOrign].AgregarArista(VertDest, cost3);
                return "Arista Agregada";
            }
            return "La posición del origen o destino no es válida";
        }

        public static List<int> MostrarAristasVertice(int posiVert)
        {
            if (posiVert >= 0 && posiVert < ListaAdyacencia.Count)
            {
                return ListaAdyacencia[posiVert].MuestraAristas().ToList();
            }
            return null;
        }

        public static List<ciudad> MostrarVertices()
        {
            return ListaAdyacencia.Select(v => v.info).ToList();
        }

    }
}