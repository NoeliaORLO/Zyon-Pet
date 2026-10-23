using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirTienda : MonoBehaviour
{
    public void Abrir()
    {
        SceneManager.LoadScene("Tienda");
    }
}