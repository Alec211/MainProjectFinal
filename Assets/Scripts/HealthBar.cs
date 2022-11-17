using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform barValue;
    
    //Greg Dev stuff
    public void SetBarState(int current, int max)
    {
        //Casting so the value work properly
        float state = (float)current;
        state /= max;

        if(state < 0){
            state = 0;
        }
        
        barValue.transform.localScale = new Vector3(state, 1f, 1f);
    }
}
