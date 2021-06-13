using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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
    static bool mushroomparticle = false;

    public static int ending = 0;
    public static float curioValue = 0;
    public static float braveValue = 0;
    public static float happyValue = 0;

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
        braveValue = bravery.slider.value;
        curioValue = curiosity.slider.value;
        happyValue = happiness.slider.value;
        

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

        

        //if we interact with the yeti load end menu to show text and stats
        if(ending != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
            FindObjectOfType<AudioManager>().Play("Pig");
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
            FindObjectOfType<AudioManager>().Play("Mushroom");
            curiosity.SetBar(1);
            if(!mushroomparticle)
            {
                mushroomparticle = true;
                Instantiate(Particle, spawnPos.transform);
                StartCoroutine(SelfDestruct());
            }

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
            FindObjectOfType<AudioManager>().Play("Plant");
            happiness.SetBar(1);
            if(!plantparticle)
            {
                plantparticle = true;
                Instantiate(Particle, spawnPos.transform);
                StartCoroutine(SelfDestruct());
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
            FindObjectOfType<AudioManager>().Play("Crow");
            text.fontSize = 18;
            text.text = "These feathers must guide somewhere...";


        }

        else if (transform.name == "Crow")
        {
            //do just once, then make crow dissapear
            if (!crow)
            {
                crow = true;
                Instantiate(Particle, spawnPos.transform);
                bravery.slider.value += 1;
                FindObjectOfType<AudioManager>().Play("Crow");
            }

            text.fontSize = 20;
            text.text = "What an scary big Crow!";

            StartCoroutine(SelfDestruct());
        }

        else if (transform.name == "BearHead")
        {
            FindObjectOfType<AudioManager>().Play("Bear");
            //do just once
            if (!bear)
            {
                bear = true;
                Instantiate(Particle, spawnPos.transform);
                bravery.slider.value += 1;
            }

            text.fontSize = 18;
            text.text = "This bear must have been enormous!";

            
        }

        else if (transform.name == "MealPot")
        {
            FindObjectOfType<AudioManager>().Play("Dinner");
            //do just once
            if (!meal)
            {
                meal = true;
                Instantiate(Particle, spawnPos.transform);
                happiness.slider.value += 1;
            }

            text.fontSize = 18;
            text.text = "This reminds me of my family";

            
        }

        else if (transform.name == "Yeti")
        {

            FindObjectOfType<AudioManager>().Play("Yeti");

            if (bravery.slider.value > curiosity.slider.value && bravery.slider.value > happiness.slider.value)
            {
                ending = 1;
               
            }

            if (bravery.slider.value == curiosity.slider.value && bravery.slider.value > happiness.slider.value)
            {
                ending = 2;
                
            }

            if (bravery.slider.value == happiness.slider.value && bravery.slider.value > curiosity.slider.value)
            {
                ending = 3;
                
            }

            if (curiosity.slider.value > bravery.slider.value && curiosity.slider.value > happiness.slider.value)
            {
                ending = 4;
               
            }

            if (curiosity.slider.value == happiness.slider.value && curiosity.slider.value > bravery.slider.value)
            {
                ending = 5;
               
            }

            if (happiness.slider.value > bravery.slider.value && happiness.slider.value > curiosity.slider.value)
            {
                ending = 6;
               
            }

        }
       
        yield return new WaitForSeconds(time);
        this.text.enabled = false;
        this.image.enabled = false;

        
    }
}
