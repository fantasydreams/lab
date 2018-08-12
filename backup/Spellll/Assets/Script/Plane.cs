using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {
    public static bool Plane_Trigger = true;
    private Animator animator;
    private long time_record = 0;
    private long Over_Milliseconds = 3000;
    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Plane_Trigger)
        {
            //if (TimeJuede.OverTime(ref time_record, Over_Milliseconds))
            //{
                animator.SetTrigger("planeTrigger");
            //}
            Plane_Trigger = false;
        }
	}
}
