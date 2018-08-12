using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbB : MonoBehaviour
{
    public static bool bulbBTrigger = true;
    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bulbBTrigger)
        {
            animator.SetTrigger("bulbBTrigger");
            bulbBTrigger = false;
        }
    }
}
