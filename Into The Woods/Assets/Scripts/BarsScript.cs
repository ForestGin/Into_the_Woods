using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarsScript : MonoBehaviour
{
    public Slider braverySlider;
    void Start()
    {
        SetBar(0);
    }
    public void SetMaxBar(int max)
    {
        braverySlider.maxValue = max;
    }

    public void SetBar(int num)
    {
        braverySlider.value = num;
    }
}
