using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulb : MonoBehaviour {

    // Use this for initialization
    public static bool bulbTrigger = true;
    private int tick = 0;
    private int max = 3;
    private long time_record = 0;
    private long OverMilliseconds = 980;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (bulbTrigger)
        {
            if(TimeJuede.OverTime(ref time_record, OverMilliseconds))
            {
                switch (tick)
                {
                    case 0:BulbB.bulbBTrigger = true;
                        break;
                    case 1:BulbR.bulbRTrigger = true;
                        break;
                    case 2:BulbY.bulbYTrigger = true;
                        break;

                    default:break;
                }
                tick = (tick + 1)% max;
            }
            bulbTrigger = false;
        }
	}
}
