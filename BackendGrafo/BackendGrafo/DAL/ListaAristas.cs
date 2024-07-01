using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendGrafo.DAL
{
    public class ListaAristas
    {
        private NodoLista inicio = null;
        private int contElemnts = 0;

        public string InsertaObjeto1(int numV, float cost)
        {
            string mensaje = "";
            NodoLista nuevo = new NodoLista();
            nuevo.vertexNum = numV;
            nuevo.costo = cost;
            if (inicio == null)
            { // no había objetos en la colección
              // es el primero
                inicio = nuevo;
                contElemnts++;
                mensaje = "Primer elemento de la colección";
            }
            else
            { // ya hay objetos en la colección, no se cuantos
              // hay que recorrer esos objetos y dejar una referencia
              // en el último objeto
                NodoLista t = null;
                t = inicio;
                while (t.next != null)
                {
                    t = t.next;
                }
                //cuando llego aquí estoy seguro que t está en el
                //último
                t.next = nuevo;
                contElemnts++;
                mensaje = "Ya no es el primer Elemento";
            }
            return mensaje;
        }

        public int[] MostrarDatosColeccion()
        {
            int[] cads = new int[contElemnts];
            NodoLista z = null;
            z = inicio;
            int w = 0; // para la localidad del arreglo
            while (z != null)
            {
                cads[w] = z.vertexNum;
                z = z.next;
                w++;
            }

            return cads;
        }
        public float ObtenerCosto(int numV)
        {
            NodoLista current = inicio;
            while (current != null)
            {
                if (current.vertexNum == numV)
                {
                    return current.costo;
                }
                current = current.next;
            }
            return float.MaxValue; // Si no encuentra el vértice, devuelve un costo infinito
        }


    }
}