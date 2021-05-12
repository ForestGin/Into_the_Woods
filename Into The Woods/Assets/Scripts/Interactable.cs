using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;

    GameObject canvas;
    Transform trans;
    Transform trans2;
    Text text;
    RawImage image;
    
    private Coroutine TextRoutine = null;

    BarsScript bravery;
    BarsScript curiosity;
    BarsScript happiness;



    void Start()
    {
        canvas = GameObject.Find("Canvas");
        trans = canvas.transform.Find("Text");
        trans2 = canvas.transform.Find("Image");
        bravery = GameObject.FindGameObjectWithTag("Bravery").GetComponent<BarsScript>();
        curiosity = GameObject.FindGameObjectWithTag("Curiosity").GetComponent<BarsScript>();
        happiness = GameObject.FindGameObjectWithTag("Happiness").GetComponent<BarsScript>();

        text = trans.GetComponent<Text>();
        image = trans2.GetComponent<RawImage>();

        text.enabled = false;
        image.enabled = false;

    }

    void Update()
    {
        
        

        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                Debug.Log("INTERACTING");
                Interact();
                hasInteracted = true;
            }

        }
    }
    public void OnFocused (Transform playerTransform)
    {
        
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius); 
    }

    public void Interact()
    {
        
        Debug.Log("Interacting with " + transform.name);

        Debug.Log("Picking up item");

        this.TextRoutine = StartCoroutine(ShowText());

       
    }

    IEnumerator ShowText()//we wait x seconds until text dissapears
    {
        float time = 3;
        this.text.enabled = true;
        this.image.enabled = true;

        

        if (gameObject.name == "Pig")
        {
            
            bravery.SetBar(1);

            if (happiness.slider.value < 1 && bravery.slider.value >= 1)
            {
                text.text = "This Pig seems hurt...";
            }

            if (happiness.slider.value >= 1)
            {
                text.text = "Have this plant piggie";
                Destroy(gameObject);
                
            }

            yield return new WaitForSeconds(time);
            

        }

        if (gameObject.name == "Mushroom")
        {
            curiosity.SetBar(1);


            if (bravery.slider.value < 1)
                text.text = "This seems bad news";

            if (bravery.slider.value >= 1)
                text.text = "This must be the cause";
                    

           

            yield return new WaitForSeconds(time);


        }


        if (gameObject.name == "Plant")
        {
            if (curiosity.slider.value < 1)
                text.text = "What is this plant?";

            if (curiosity.slider.value >= 1 && bravery.slider.value >= 1)
            {
                happiness.SetBar(1);
                text.text = "This is the medicinal plant I need!";
            }

            yield return new WaitForSeconds(time);


        }


        this.text.enabled = false;
        this.image.enabled = false;
        //yield return new WaitForSeconds(3);
        //text.enabled = false;

    }
}
