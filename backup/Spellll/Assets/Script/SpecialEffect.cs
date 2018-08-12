using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffect : MonoBehaviour {
    public static bool isActivated = true;
    private Animator Anitor;
    private long timeRecord = 0; //上一次被激活的时间戳
    private long Over_Milliseconds = 3000;
    // Use this for initialization
    void Start () {
        Anitor = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isActivated)
        {
            //if (TimeJuede.OverTime(ref timeRecord, Over_Milliseconds))
            //{
                Anitor.SetTrigger("isActivated");
            //}
            isActivated = false;
        }
	}
}
