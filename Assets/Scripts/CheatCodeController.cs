using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatCodeController : MonoBehaviour
{
    public Text successText;
    private KonamiCode konamiCode;

    private void Awake()
    {
        konamiCode = GetComponent<KonamiCode>();
    }

    // Update is called once per frame
    void Update()
    {
       if(konamiCode.success)
        {
            Debug.Log("Good");
            //successText.gameObject.SetActive(true);
        }
    }
}
