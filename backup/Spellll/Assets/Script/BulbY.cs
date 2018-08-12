using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbY : MonoBehaviour
{
    public static bool bulbYTrigger = true;
    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bulbYTrigger)
        {
            animator.SetTrigger("bulbYTrigger");
            bulbYTrigger = false;
        }
    }
}
