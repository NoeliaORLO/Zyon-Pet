using UnityEngine;
using UnityEngine.UI;

public class BarraStats : MonoBehaviour
{
    [SerializeField] private Medidor medidor = new Medidor(); // los números de la barra
    [SerializeField] private Image relleno;                   // la imagen que se llena

    private void OnEnable()
    {
        medidor.AlCambiar += Redibujar;
        Redibujar(medidor);
    }

    private void OnDisable()
    {
        medidor.AlCambiar -= Redibujar;
    }

    private void Redibujar(Medidor m)
    {
        relleno.fillAmount = m.Proporcion();
    }

    // Los llama Mascota
    public void SetMaxExp(int max)
    {
        medidor.maximo = max;
        medidor.Fijar(max);
        Redibujar(medidor);
    }

    public void setExp(int valor)
    {
        medidor.Fijar(valor);
    }
}