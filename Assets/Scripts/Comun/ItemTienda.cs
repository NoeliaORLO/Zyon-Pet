using UnityEngine;

public enum TipoItem { Fondo, Decoracion, Comida }

[CreateAssetMenu(menuName = "Tienda/Item")]
public class ItemTienda : ScriptableObject
{
    public string id;            // único: "fondo1", "decoracion1", "comida1"
    public string nombre;
    public Sprite icono;         // el que se ve en la tienda
    public Sprite spriteJuego;   // el que se verá en Principal (fondo o decoración)
    public int precio;
    public TipoItem tipo;
    public int expExtra = 20;    // solo para la comida
}