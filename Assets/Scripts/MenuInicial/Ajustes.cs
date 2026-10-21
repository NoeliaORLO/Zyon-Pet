using UnityEngine;
using UnityEngine.UI;

public class Ajustes : MonoBehaviour
{
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderBrillo;
    [SerializeField] private Image overlayBrillo; // imagen negra a pantalla completa

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
        // El brillo real del móvil no se controla bien desde Unity;
        // se simula con una capa negra semitransparente
        if (overlayBrillo != null)
            overlayBrillo.color = new Color(0, 0, 0, 1f - Mathf.Clamp(v, 0.2f, 1f));
        PlayerPrefs.SetFloat("brillo", v);
    }
}