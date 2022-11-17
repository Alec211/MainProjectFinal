using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWeapon : MonoBehaviour
{
    float timeTillHidden = 1f;
    float timer;

    private void OnEnable()
    {
        timer = timeTillHidden;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0){
            gameObject.SetActive(false);
        }
    }
}
