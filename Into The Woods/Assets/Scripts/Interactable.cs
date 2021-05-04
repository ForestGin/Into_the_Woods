
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;

    public GameObject Text;


    void Start()
    {
        Text = GameObject.Find("Text");
        
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

        if(gameObject.name == "Rock")
            Destroy(gameObject);
    }
}
