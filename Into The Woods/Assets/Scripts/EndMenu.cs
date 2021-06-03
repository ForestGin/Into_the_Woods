using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    GameObject canvas;
    Transform trans;
    Transform transBra;
    Transform transCur;
    Transform transHap;
    Text text;
    Text braveNum;
    Text curioNum;
    Text happyNum;

   

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        trans = canvas.transform.Find("EndText");
        transBra = canvas.transform.Find("BraveryNum");
        transCur = canvas.transform.Find("CuriosityNum");
        transHap = canvas.transform.Find("HappinessNum");

        text = trans.GetComponent<Text>();
        braveNum = transBra.GetComponent<Text>();
        curioNum = transCur.GetComponent<Text>();
        happyNum = transHap.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        braveNum.text = Interactable.braveValue.ToString();
        curioNum.text = Interactable.curioValue.ToString();
        happyNum.text = Interactable.happyValue.ToString();

        if (Interactable.ending == 1)
        {
            text.fontSize = 60;
            text.text = "Mathias makes the Yeti run away";
        }

        if (Interactable.ending == 2)
        {
            text.fontSize = 50;
            text.text = "Mathias is captured but faces the Yeti and makes him run away";
        }

        if (Interactable.ending == 3)
        {
            text.fontSize = 50;
            text.text = "Mathias faces the Yeti but in the end they become friend";
        }

        if (Interactable.ending == 4)
        {
            text.fontSize = 60;
            text.text = "Mathias finds the Yeti and it's captured";
        }

        if (Interactable.ending == 5)
        {
            text.fontSize = 50;
            text.text = "Mathias is captured but makes the Yeti his friend while its captive";
        }

        if (Interactable.ending == 6)
        {
            text.fontSize = 60;
            text.text = "Mathias and the Yeti are friends";
        }
    }
}
