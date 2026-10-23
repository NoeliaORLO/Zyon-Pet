using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private string escenaJuego = "Principal";
    [SerializeField] private GameObject botonContinuar;

    private void Start()
    {
        bool hayPartida = GameManager.Instance != null && GameManager.Instance.HayPartida;
        if (botonContinuar != null) botonContinuar.SetActive(hayPartida);
    }

    public void NuevaPartida()
    {
        if (GameManager.Instance != null) GameManager.Instance.ReiniciarPartida();
        SceneManager.LoadScene(escenaJuego);
    }

    public void Continuar()
    {
        SceneManager.LoadScene(escenaJuego);
    }

    public void Salir()
    {
        Application.Quit();
    }
}