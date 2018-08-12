using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbR : MonoBehaviour {
    public static bool bulbRTrigger = true;
    private Animator animator;
    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (bulbRTrigger)
        {
            animator.SetTrigger("bulbrTrigger");
            bulbRTrigger = false;
        }
	}
}
