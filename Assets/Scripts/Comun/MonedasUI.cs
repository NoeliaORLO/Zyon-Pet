using TMPro;
using UnityEngine;

public class MonedasUI : MonoBehaviour
{
    [SerializeField] private TMP_Text texto;

    private void Start()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnMonedasCambiadas += Actualizar;
        Actualizar(GameManager.Instance.Monedas);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnMonedasCambiadas -= Actualizar;
    }

    private void Actualizar(int valor) { texto.text = valor.ToString(); }
}