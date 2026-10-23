using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotTienda : MonoBehaviour
{
    [SerializeField] private ItemTienda item;
    [SerializeField] private Image icono;
    [SerializeField] private TMP_Text textoNombre;
    [SerializeField] private TMP_Text textoPrecio;
    [SerializeField] private Button boton;

    private void Start()
    {
        icono.sprite = item.icono;
        boton.onClick.AddListener(Comprar);

        if (GameManager.Instance != null)
            GameManager.Instance.OnMonedasCambiadas += OnMonedas;

        Refrescar();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnMonedasCambiadas -= OnMonedas;
    }

    private void OnMonedas(int _) { Refrescar(); }

    private void Comprar()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.Comprar(item))
            GameManager.Instance.Equipar(item);   // fondo y decoración se ponen solos; la comida se ignora
        else
            Debug.Log("No se puede comprar " + item.nombre);

        Refrescar();
    }

    private void Refrescar()
    {
        var gm = GameManager.Instance;
        int cantidad = gm != null ? gm.Cantidad(item.id) : 0;

        textoNombre.text = (item.tipo == TipoItem.Comida && cantidad > 0)
            ? item.nombre + " x" + cantidad
            : item.nombre;

        bool yaComprado = item.tipo != TipoItem.Comida && cantidad > 0;
        textoPrecio.text = yaComprado ? "Comprado" : item.precio.ToString();
        boton.interactable = !yaComprado && gm != null && gm.Monedas >= item.precio;
    }
}