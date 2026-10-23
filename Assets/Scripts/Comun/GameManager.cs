using System;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public DatosMascota DatosMascota { get; private set; } = new DatosMascota();

    [Header("Monedas")]
    [SerializeField] private int monedasIniciales = 100;

    public int Monedas { get; private set; }

    public event Action<int> OnMonedasCambiadas;

    // --------------------------------- INVENTARIO ---------------------------------------
    private readonly Dictionary<string, int> inventario = new Dictionary<string, int>();

    public int Cantidad(string id) { return inventario.TryGetValue(id, out int c) ? c : 0; }
    public bool Tiene(string id) { return Cantidad(id) > 0; }

    public bool Comprar(ItemTienda item)
    {
        // Fondos y decoraciones se compran una sola vez; la comida, las veces que quieras
        if (item.tipo != TipoItem.Comida && Tiene(item.id)) return false;
        if (!Gastar(item.precio)) return false;

        inventario[item.id] = Cantidad(item.id) + 1;
        return true;
    }
    public bool Usar(string id)
    {
        if (!Tiene(id)) return false;
        inventario[id]--;
        return true;
    }
    // --------------------------------------------------------------------------------------
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Monedas = monedasIniciales;

        HayPartida = PlayerPrefs.GetInt("hayPartida", 0) == 1;
    }

    public void GanarMonedas(int cantidad)
    {
        if (cantidad <= 0) return;

        Monedas += cantidad;
        OnMonedasCambiadas?.Invoke(Monedas);
    }

    public bool Gastar(int cantidad)
    {
        if (cantidad <= 0 || Monedas < cantidad) return false;

        Monedas -= cantidad;
        OnMonedasCambiadas?.Invoke(Monedas);
        return true;
    }

    // Si creamos una Nueva Partida:
    public void ReiniciarPartida()
    {
        DatosMascota = new DatosMascota();
        Monedas = monedasIniciales;
        inventario.Clear();
        OnMonedasCambiadas?.Invoke(Monedas);

        MarcarPartidaIniciada();
    }
    public bool HayPartida { get; private set; }

    public void MarcarPartidaIniciada() { 
        HayPartida = true;
        PlayerPrefs.SetInt("hayPartida", 1);
        PlayerPrefs.Save();
    }
}