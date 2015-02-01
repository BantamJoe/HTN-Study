using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float dampTime = 0.25f;

    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        if (anim)
        {
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            //if (state.nameHash == (int)PlayerState.WALKING)
            //{
            //    //do something
            //}
            anim.SetFloat("velX", h);
            anim.SetFloat("velZ", v);
            anim.speed = 1.5f;
        }
    }
}