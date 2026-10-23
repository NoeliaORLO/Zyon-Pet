using System.Collections;
using UnityEngine;

[System.Serializable]
public class DatosMascota
{
    public Etapa etapa = Etapa.Bebe;
    public int hambre = Mascota.MAX;
    public int limpieza = Mascota.MAX;
    public int diversion = Mascota.MAX;
    public int exp = 0;
}