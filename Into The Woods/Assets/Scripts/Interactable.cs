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
    Text textbravery;
    Image[] imagesbravery;

    BarsScript curiosity;
    Text textcuriosity;
    Image[] imagescuriosity;

    BarsScript happiness;
    Text texthappiness;
    Image[] imageshappiness;


    static bool crow = false;
    static bool meal = false;
    static bool bear = false;

    public GameObject spawnPos;
    public GameObject Particle;
    static bool pigparticle = false;
    static bool plantparticle = false;

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

        //HIDE BARS AND TEXT
        textbravery = bravery.GetComponentInChildren<Text>();
        imagesbravery = bravery.GetComponentsInChildren<Image>();
        textbravery.enabled = false;
        imagesbravery[0].enabled = false;
        imagesbravery[1].enabled = false;

        textcuriosity = curiosity.GetComponentInChildren<Text>();
        imagescuriosity = curiosity.GetComponentsInChildren<Image>();
        textcuriosity.enabled = false;
        imagescuriosity[0].enabled = false;
        imagescuriosity[1].enabled = false;

        texthappiness = happiness.GetComponentInChildren<Text>();
        imageshappiness = happiness.GetComponentsInChildren<Image>();
        texthappiness.enabled = false;
        imageshappiness[0].enabled = false;
        imageshappiness[1].enabled = false;

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

        //Debug.Log("Bravery" +  bravery.slider.value);
        //Debug.Log("Happiness" +  happiness.slider.value);
    }
    public void OnFocused (Transform playerTransform)
    {
        
        isFocus = true;
        player = playerTransform;
        //hasInteracted = false;
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

        this.TextRoutine = StartCoroutine(ShowText());

       
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    IEnumerator ShowText()//we wait x seconds until text dissapears
    {
        float time = 3;
        this.text.enabled = true;
        this.image.enabled = true;

        

        if (transform.name == "Pig")
        {
            
            bravery.SetBar(1);
            if (!pigparticle)
            {
                pigparticle = true;
                Instantiate(Particle, spawnPos.transform);
            }

            if (bravery.slider.value >= 1 && curiosity.slider.value < 1 && happiness.slider.value < 1)
            {
                //Instantiate(ParticleBravery, transform);
                text.fontSize = 18;
                text.text = "This Pig is blocking the way. He seems hurt...";
            }

            if (bravery.slider.value >= 1 && curiosity.slider.value >=1 && happiness.slider.value < 1)
            {
                text.fontSize = 14;
                text.text = "It seems you've eaten that mushroom. I'll help you";
            }

            if (bravery.slider.value >= 1 && curiosity.slider.value >= 1 && happiness.slider.value >= 1 ||
                bravery.slider.value >= 1 && curiosity.slider.value < 1 && happiness.slider.value >= 1)
            {
                
                text.fontSize = 20;
                text.text = "Have this plant piggie, it will help";
                StartCoroutine(SelfDestruct());

            }

            //yield return new WaitForSeconds(time);
            

        }
        else if (transform.name == "Mushroom")
        {
            curiosity.SetBar(1);


            if (curiosity.slider.value >= 1 && bravery.slider.value < 1 && happiness.slider.value < 1)
            {
                text.fontSize = 20;
                text.text = "This is poisonous. Bad news...";
            }

            if (curiosity.slider.value >= 1 && bravery.slider.value >= 1 && happiness.slider.value < 1)
            {
                text.fontSize = 20;
                text.text = "The pig has eaten this mushroom";
            }

            if (curiosity.slider.value >= 1 && bravery.slider.value >= 1 && happiness.slider.value >= 1 ||
                curiosity.slider.value >= 1 && bravery.slider.value < 1 && happiness.slider.value >= 1)
            {
                text.fontSize = 14;
                text.text = "If I feed the plant to the Pig, the venom will be gone!";
            }


            //yield return new WaitForSeconds(time);


        }
        else if (transform.name == "Plant")
        {

            happiness.SetBar(1);
            if(!plantparticle)
            {
                plantparticle = true;
                Instantiate(Particle, spawnPos.transform);
            }

            if (happiness.slider.value >= 1 && curiosity.slider.value < 1 && bravery.slider.value < 1)
            {
                text.fontSize = 20;
                text.text = "That's weird, a medicinal plant";
            }

            if (happiness.slider.value >= 1 && curiosity.slider.value >= 1 && bravery.slider.value < 1)
            {
                text.fontSize = 14;
                text.text = "This could counter the effects of that mushroom!";
            }

            if (happiness.slider.value >= 1 && curiosity.slider.value >= 1 && bravery.slider.value >= 1 ||
                happiness.slider.value >= 1 && curiosity.slider.value < 1 && bravery.slider.value >= 1)
            {
                text.fontSize = 20;
                text.text = "It could help the hurt Pig!";
                //StartCoroutine(SelfDestruct());
            }

            //yield return new WaitForSeconds(time);


        }

        else if (transform.name == "Feather1" || transform.name == "Feather2")
        {
            text.fontSize = 18;
            text.text = "These feathers must guide somewhere...";


        }

        else if (transform.name == "Crow")
        {
            //do just once, then make crow dissapear
            if (!crow)
            {
                crow = true;
                bravery.slider.value += 1;
            }

            text.fontSize = 20;
            text.text = "What an scary big Crow!";

            StartCoroutine(SelfDestruct());
        }

        else if (transform.name == "BearHead")
        {
            //do just once
            if (!bear)
            {
                bear = true;
                bravery.slider.value += 1;
            }

            text.fontSize = 18;
            text.text = "This bear must have been enormous!";

            
        }

        else if (transform.name == "MealPot")
        {
            //do just once
            if (!meal)
            {
                meal = true;
                happiness.slider.value += 1;
            }

            text.fontSize = 18;
            text.text = "This reminds me of my family";

            
        }

        else if (transform.name == "Yeti")
        {


            if (bravery.slider.value > curiosity.slider.value && bravery.slider.value > happiness.slider.value)
            {
                text.fontSize = 20;
                text.text = "Mathias makes the Yeti run away";
            }

            if (bravery.slider.value == curiosity.slider.value && bravery.slider.value > happiness.slider.value)
            {
                text.fontSize = 14;
                text.text = "Mathias is captured but faces the Yeti and makes him run away";
            }

            if (bravery.slider.value == happiness.slider.value && bravery.slider.value > curiosity.slider.value)
            {
                text.fontSize = 14;
                text.text = "Mathias faces the Yeti but in the end they become friend";
            }

            if (curiosity.slider.value > bravery.slider.value && curiosity.slider.value > happiness.slider.value)
            {
                text.fontSize = 20;
                text.text = "Mathias finds the Yeti and it's captured";
            }

            if (curiosity.slider.value == happiness.slider.value && curiosity.slider.value > bravery.slider.value)
            {
                text.fontSize = 14;
                text.text = "Mathias is captured but makes the Yeti his friend while its captive";
            }

            if (happiness.slider.value > bravery.slider.value && happiness.slider.value > curiosity.slider.value)
            {
                text.fontSize = 20;
                text.text = "Mathias and the Yeti are friends";
            }

        }

        yield return new WaitForSeconds(time);
        this.text.enabled = false;
        this.image.enabled = false;


    }
}
