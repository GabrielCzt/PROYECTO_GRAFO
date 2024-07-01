using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendGrafo.DAL
{
    public class Vertice
    {
        public ciudad info;
        public ListaAristas ListaEnlaces = new ListaAristas();
        public Vertice(ciudad datos)
        {
            info = datos;
        }
        public string AgregarArista(int numV2, float cost2)
        {
            return ListaEnlaces.InsertaObjeto1(numV2, cost2);
        }

        public int[] MuestraAristas()
        {
            return ListaEnlaces.MostrarDatosColeccion();
        }
        public string infoCiudad()
        {
            return $"id ={info.idCiudad} ciudad={info.NomCiudad} totalhabitantes={info.totalhabitantes} superficie={info.superficieKm}";
        }
    }
}