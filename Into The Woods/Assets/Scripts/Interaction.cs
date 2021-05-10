using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    Interactable focus;
    Camera cam;

    string[] keys = new string[] { "w", "a", "s", "d" };

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))//left click to interact
            {
                //check if we hit interactable object 
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if(interactable != null)
                {
                    SetFocus(interactable);
                }

                //debug raycast 
                Debug.Log("WE HIT" + hit.collider.name + " " + hit.point);
            }

            
        }

        //WE NEED TO UNFOCUS INTERACTABLE OBJECT IF PLAYER USES WASD

        if (AnyKeyDown(keys))
        {
            RemoveFocus();
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
        }
        
        newFocus.OnFocused(transform);
    }

    void RemoveFocus ()
    {
        if(focus != null)
            focus.OnDefocused();

        focus = null;
    }

    public bool AnyKeyDown(IEnumerable<string> keys)
    {
        foreach (string key in keys)
        {
            if (Input.GetKeyDown(key)) return true;
        }
        return false;
    }
}
