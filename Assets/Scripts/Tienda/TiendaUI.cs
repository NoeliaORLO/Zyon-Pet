using UnityEngine;
using UnityEngine.SceneManagement;

public class TiendaUI : MonoBehaviour
{
    [SerializeField] private string escenaJuego = "Principal";

    public void Volver()
    {
        SceneManager.LoadScene(escenaJuego);
    }
}