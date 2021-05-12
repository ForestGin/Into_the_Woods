using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarsScript : MonoBehaviour
{
    public Slider slider;
   
    void Start()
    {
        SetBar(0);
    }
    public void SetMaxBar(int max)
    {
        slider.maxValue = max;
    }

    public void SetBar(int num)
    {
        
        slider.value = num;
      
    }
}
