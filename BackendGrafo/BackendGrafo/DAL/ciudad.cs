using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendGrafo.DAL
{
    public class ciudad
    {
        public int idCiudad { set; get; }
        public string NomCiudad { set; get; }
        public int totalhabitantes { set; get; }
        public float superficieKm { set; get; }
        public ciudad() { }
        public ciudad(int id1, string nomC, int toth, float sup)
        {
            idCiudad = id1;
            NomCiudad = nomC;
            totalhabitantes = toth;
            superficieKm = sup;
        }
    }
}