using System;
using UnityEngine;


/// Un medidor es un valor que va de 0 a "maximo" (hambre, diversión, limpieza, experiencia...).
/// Solo guarda números y avisa con eventos cuando cambia. No dibuja nada.

[Serializable] // permite que aparezca (y se pueda editar) en el Inspector
public class Medidor
{
    public float maximo = 100;
    public float actual = 0;

    // Eventos: otros scripts se "suscriben" para enterarse de lo que pasa
    public Action<Medidor> AlCambiar;         // cada vez que el valor cambia
    public Action<Medidor> AlLlegarAlMaximo;  // cuando llega a "maximo" (ej: evolucionar)
    public Action<Medidor> AlLlegarAlMinimo;  // cuando llega a 0 (ej: la mascota se pone triste)

    // Devuelve un número entre 0 y 1 (0 = vacío, 0.5 = mitad, 1 = lleno)
    public float Proporcion()
    {
        return actual / maximo;
    }

    // Suma (o resta, si es negativo) una cantidad al valor actual
    public void Cambiar(float cantidad)
    {
        float antes = actual;

        // Clamp = "recortar": el valor nunca baja de 0 ni pasa del máximo
        actual = Mathf.Clamp(actual + cantidad, 0, maximo);

        // Si ya estaba en el límite y no ha cambiado nada, no avisamos
        if (actual == antes) return;

        AlCambiar?.Invoke(this);

        if (actual >= maximo) AlLlegarAlMaximo?.Invoke(this);
        if (actual <= 0) AlLlegarAlMinimo?.Invoke(this);
    }

    // Pone el valor directamente (en vez de sumar o restar)
    public void Fijar(float valor)
    {
        Cambiar(valor - actual);
    }
}