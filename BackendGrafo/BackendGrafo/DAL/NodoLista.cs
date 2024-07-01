using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendGrafo.DAL
{
    public class NodoLista
    {
        //posición del vertice en la lista de adyacencia a la que tiene
        //un enlace

        public ciudad infoV;
        public int vertexNum = -1; //No se pone cero porque existe el vertice en la posición 0
        //Costo para llegar a ese vertice
        public float costo { get; set; }
        //Enlace de lista ligada
        public NodoLista next;
    }
}