using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public DatosMascota DatosMascota { get; private set; } = new DatosMascota();

    [Header("Monedas")]
    [SerializeField] private int monedasIniciales = 100;

    public int Monedas { get; private set; }

    public event Action<int> OnMonedasCambiadas;

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

    // NUEVO: para "Nueva partida"
    public void ReiniciarPartida()
    {
        DatosMascota = new DatosMascota();
        Monedas = monedasIniciales;
        OnMonedasCambiadas?.Invoke(Monedas);
    }
    public bool HayPartida { get; private set; }

    public void MarcarPartidaIniciada() { HayPartida = true; }
}