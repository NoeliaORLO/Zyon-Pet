using UnityEngine;

public class AplicadorTienda : MonoBehaviour
{
    [Header("Catálogo")]
    [SerializeField] private ItemTienda[] catalogo;

    [Header("Dónde se aplican")]
    [SerializeField] private SpriteRenderer pared;        // objeto Pared
    [SerializeField] private SpriteRenderer decoracion;   // objeto junto a la mascota (vacío al principio)

    private void Start()
    {
        if (GameManager.Instance == null) return;

        Aplicar(TipoItem.Fondo, pared);
        Aplicar(TipoItem.Decoracion, decoracion);
    }

    private void Aplicar(TipoItem tipo, SpriteRenderer destino)
    {
        if (destino == null) return;

        string id = GameManager.Instance.Equipado(tipo);
        if (string.IsNullOrEmpty(id))
        {
            if (tipo == TipoItem.Decoracion) destino.enabled = false; // nada equipado: oculta
            return;
        }

        foreach (var item in catalogo)
        {
            if (item.id == id && item.spriteJuego != null)
            {
                destino.sprite = item.spriteJuego;
                destino.enabled = true;
                return;
            }
        }
    }
}