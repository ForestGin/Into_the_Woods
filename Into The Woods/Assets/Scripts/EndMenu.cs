using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    GameObject canvas;
    Transform trans;
    Text text;

   

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        trans = canvas.transform.Find("EndText");

        text = trans.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Interactable.ending == 1)
        {
            text.fontSize = 60;
            text.text = "Mathias makes the Yeti run away";
        }
    }
}
