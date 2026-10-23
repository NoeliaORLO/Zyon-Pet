using UnityEngine;
using UnityEngine.UI;

public class Ajustes : MonoBehaviour
{
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderBrillo;
    [SerializeField] private Image overlayBrillo; // imagen negra a pantalla completa

    [SerializeField] private GameObject panelAjustes;

    public void Abrir() { panelAjustes.SetActive(true); }
    public void Cerrar() { panelAjustes.SetActive(false); }

    private void Start()
    {
        sliderMusica.value = PlayerPrefs.GetFloat("musica", 0.8f);
        sliderBrillo.value = PlayerPrefs.GetFloat("brillo", 1f);
        AplicarMusica(sliderMusica.value);
        AplicarBrillo(sliderBrillo.value);

        sliderMusica.onValueChanged.AddListener(AplicarMusica);
        sliderBrillo.onValueChanged.AddListener(AplicarBrillo);
    }

    private void AplicarMusica(float v)
    {
        AudioListener.volume = v;          // volumen global
        PlayerPrefs.SetFloat("musica", v);
    }

    private void AplicarBrillo(float v)
    {
        Debug.Log("Brillo: " + v + " | overlay: " + (overlayBrillo != null));
        if (overlayBrillo != null)
            overlayBrillo.color = new Color(0, 0, 0, (1f - v) * 0.85f);
        PlayerPrefs.SetFloat("brillo", v);
    }
}