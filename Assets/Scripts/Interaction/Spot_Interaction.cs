using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spot_Interaction : InteractionBase
{
    [SerializeField] private string spotName;
    public Transform spotSpell;

    public override void Action(GameObject player)
    {
        if (player.GetComponent<ThirdPersonController>())
        {
           Debug.Log(spotName + " interaction");
           player.GetComponent<ThirdPersonShooterController>().bulletProjectile = spotSpell;
        }
    }
}
