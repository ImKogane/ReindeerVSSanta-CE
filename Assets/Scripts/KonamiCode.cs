using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCode : MonoBehaviour
{
    private const float wait = 1f;

    private KeyCode[] keys = new KeyCode[]
    {
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.B,
        KeyCode.A
    };
    public bool success;

    private IEnumerator Start()
    {
        float timer = 0f;
        int index = 0;

        while(true)
        {
            if (Input.GetKeyDown(keys[index]))
            {
                index++;
                Debug.Log(index);

                if(index == keys.Length)
                {
                    success = true;
                    timer = 0f;
                }
                else
                {
                    timer = wait;
                }
                
            }

            timer -= Time.deltaTime;
            if(timer < 0f)
            {
                Debug.Log("Reset");
                timer = 0;
                index = 0;
            }
        }
    }

}
