using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowSpot_Interaction : InteractionBase
{
    public override void Action(GameObject player)
    {
        if (player.GetComponent<ThirdPersonController>())
        {
           Debug.Log("Snow spot interaction");
        }
    }
}
