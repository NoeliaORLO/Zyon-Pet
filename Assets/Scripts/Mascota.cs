using UnityEngine;

public enum Etapa { Bebe, Evolucion2, Evolucion3 } // Lista de las etapas del Zyon

/// Lógica de la mascota: necesidades (hambre, limpieza y diversión), experiencia y evolución. Todas las necesidades van de 0 a MAX (1000), por lo que alcanzar 100 puntos significa
/// que esa área está totalmente cubierta.

public class Mascota : MonoBehaviour // MonoBehaviour -> permite poner el script en un GameObject
{
    public const int MAX = 1000; // creamos la constante MAX para reutilizarla a lo largo del script

    // Usaremos Header y Tooltip para facilitar nuestro trabajo en el Inspector.

    [Header("Tiempo")]
    [Tooltip("Cada cuántos segundos baja 1 punto cada necesidad")]
    [SerializeField] private float segundosPorPunto = 5f; // SerializeField -> permite que la variable aparezca en el Inspector aunque sea private.

    [Header("Cuánto sube cada acción")]
    [SerializeField] private int valorAlimentar = 250;
    [SerializeField] private int valorLimpiar = 350;
    [SerializeField] private int valorJugar = 150;
    [SerializeField] private int expPorAccion = 5;

    [Header("Evolución")]
    [Tooltip("Exp necesaria: [0] Bebé -> Evolución 2, [1] Evolución 2 -> Evolución 3")]
    [SerializeField] private int[] expParaEvolucionar = { 1500, 2000 }; // experiencia necesaria para evolucionar de Bebé a EV2 (1500) y de EV2 a EV3 (2000)
    [Tooltip("Un sprite por etapa, en orden: Bebé, Evolución 2, Evolución 3")]
    [SerializeField] private Sprite[] spritesEtapa; // creamos una lista de sprites para guardar los dibujos de cada etapa de la mascota

    [Header("Barras de la interfaz")]
    [SerializeField] private BarraExp barraHambre;
    [SerializeField] private BarraExp barraLimpieza;
    [SerializeField] private BarraExp barraDiversion;
    [SerializeField] private BarraExp barraExp;

    // Todas las necesidades van de 0 a MAX (1000). Con MAX la necesidad está totalmente cubierta.

    // Estado de la mascota: cuando es bebé, tiene el hambre, limpieza y diversión al máximo, y la experiencia al mínimo.
    private Etapa etapa = Etapa.Bebe;
    private int hambre = MAX;      // 1000 = saciado
    private int limpieza = MAX;    // 1000 = limpio
    private int diversion = MAX;   // 1000 = divertido
    private int exp = 0;

    private float timer = 0f; // Cronómetro interno, empieza en 0.
    private SpriteRenderer sr; // Objeto SpriteRenderer para dibujar la imagen de la mascota (se creará cuando se invoque Awake())

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>(); // Se dibuja la mascota
    }

    private void Start()
    {
        // ponemos las barras de necesidades al máximo
        barraHambre.SetMaxExp(MAX);
        barraLimpieza.SetMaxExp(MAX);
        barraDiversion.SetMaxExp(MAX);

        barraExp.SetMaxExp(UmbralActual()); // se sitúa la exp en 1500
        barraExp.setExp(exp); // se vacía la exp a 0 para comenzar el juego

        ActualizarSprite();
        ActualizarBarras();
    }

    // 1. Paso del tiempo con Update()
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= segundosPorPunto)
        {
            timer = 0f;
            hambre = Mathf.Max(0, hambre - 1);
            limpieza = Mathf.Max(0, limpieza - 1);
            diversion = Mathf.Max(0, diversion - 1);
            ActualizarBarras();
        }
    }

    // 2. Acciones del Juego (las llaman los botones)
    public void Alimentar() { Atender(ref hambre, valorAlimentar); }
    public void Limpiar() { Atender(ref limpieza, valorLimpiar); }
    public void Jugar() { Atender(ref diversion, valorJugar); }

    private void Atender(ref int necesidad, int valor)
    {
        // Si la necesidad ya estaba llena no da experiencia (evita spamear el botón)
        bool estabaLlena = necesidad >= MAX;

        necesidad = Mathf.Min(MAX, necesidad + valor);

        if (!estabaLlena)
            GanarExp(expPorAccion);

        ActualizarBarras();
    }

    // ---------- 3. EXPERIENCIA Y EVOLUCIÓN ----------
    private void GanarExp(int cantidad)
    {
        if (etapa == Etapa.Evolucion3) return; // ya está en la última etapa

        exp += cantidad;

        if (exp >= UmbralActual())
        {
            Evolucionar();
            return;
        }

        barraExp.setExp(exp);
    }

    private void Evolucionar()
    {
        etapa++;
        ActualizarSprite();

        barraExp.SetMaxExp(UmbralActual());
        // En la última etapa la barra se queda llena; en las demás, vuelve a empezar
        exp = (etapa == Etapa.Evolucion3) ? UmbralActual() : 0;
        barraExp.setExp(exp);

        Debug.Log("¡La mascota ha evolucionado a " + etapa + "!");
    }

    // Experiencia que hace falta para pasar de la etapa actual a la siguiente
    private int UmbralActual()
    {
        if (etapa == Etapa.Evolucion3) return 1;
        return expParaEvolucionar[(int)etapa];
    }

    // ---------- 4. REFLEJAR EL ESTADO EN PANTALLA ----------
    private void ActualizarSprite()
    {
        if (spritesEtapa != null && spritesEtapa.Length > (int)etapa)
            sr.sprite = spritesEtapa[(int)etapa];
    }

    private void ActualizarBarras()
    {
        barraHambre.setExp(hambre);
        barraLimpieza.setExp(limpieza);
        barraDiversion.setExp(diversion);
    }
}