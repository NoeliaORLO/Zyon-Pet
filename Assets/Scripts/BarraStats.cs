using UnityEngine;
using UnityEngine.UI;

public class BarraStats : MonoBehaviour
{
    public Slider slider;

    public void SetMaxExp(int exp)
    {
        slider.maxValue = exp;
        slider.value = exp;
    }
    public void setExp(int exp)
    {
        slider.value = exp;
    }
}
