using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionBase : MonoBehaviour
{
    public virtual void Action(GameObject player)
    {
        if (player.GetComponent<ThirdPersonController>())
        {
           Debug.Log("Interaction");
        }
    }
}
