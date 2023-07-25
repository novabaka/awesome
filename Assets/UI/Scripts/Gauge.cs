using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Slider slider;

    public void SetMaxGauge(int gauge)
    {
        slider.maxValue = gauge;
    }

    public void SetGauge(float gauge)
    {
        slider.value = gauge;
    }
}
