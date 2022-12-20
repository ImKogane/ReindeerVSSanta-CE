using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiftSpot_Interaction : InteractionBase
{
    public override void Action(GameObject player)
    {
        if (player.GetComponent<ThirdPersonController>())
        {
           Debug.Log("Gift spot interaction");
        }
    }
}
