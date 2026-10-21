using UnityEngine;
using SceneManagement = UnityEngine.SceneManagement.SceneManager;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private string escenaJuego = "Juego";
    [SerializeField] private GameObject botonContinuar;
    [SerializeField] private GameObject panelAjustes;

    private void Start()
    {
        bool hayPartida = GameManager.Instance != null && GameManager.Instance.HayPartida;
        if (botonContinuar != null) botonContinuar.SetActive(hayPartida);
        if (panelAjustes != null) panelAjustes.SetActive(false);
    }

    public void NuevaPartida()
    {
        if (GameManager.Instance != null) GameManager.Instance.ReiniciarPartida();
        SceneManagement.LoadScene(escenaJuego);
    }

    public void Continuar()
    {
        SceneManagement.LoadScene(escenaJuego);
    }

    public void AbrirAjustes() { panelAjustes.SetActive(true); }
    public void CerrarAjustes() { panelAjustes.SetActive(false); }

    public void Salir()
    {
        Application.Quit(); // solo funciona en la build, no en el editor
    }
}