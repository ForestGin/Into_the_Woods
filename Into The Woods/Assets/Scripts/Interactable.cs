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
    Text text;

    private Coroutine TextRoutine = null;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        trans = canvas.transform.Find("Text");
        text = trans.GetComponent<Text>();

        text.enabled = false;
        
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


        

        if (gameObject.name == "Apple")
        {

            text.text = "This is a Sphere";
            yield return new WaitForSeconds(time);


        }
        if (gameObject.name == "Rock")
        {

            text.text = "This is a Cube";
            yield return new WaitForSeconds(time);


        }
            
      

        this.text.enabled = false;
        //yield return new WaitForSeconds(3);
        //text.enabled = false;
       
    }
}
