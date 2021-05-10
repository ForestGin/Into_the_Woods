using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class EvilController : MonoBehaviour
{
    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        if (curSpeed > 0)
        {
            animator.SetInteger("walk", 1);
        }
        else
        {
            animator.SetInteger("walk", 0);
        }
        if (curSpeed >= 0)
        {
            controller.SimpleMove(forward * curSpeed);
        }
    }
}

