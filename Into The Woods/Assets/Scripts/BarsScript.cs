using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarsScript : MonoBehaviour
{
    public Slider braverySlider;
    void Start()
    {
        Set(0);
    }
    public void SetMaxBar(int max)
    {
        braverySlider.maxValue = max;
    }

    public void Set(int num)
    {
        braverySlider.value = num;
    }
}
