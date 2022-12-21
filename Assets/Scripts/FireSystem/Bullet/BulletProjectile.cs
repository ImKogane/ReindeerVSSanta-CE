using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform VFX;

    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 20.0f;
        bulletRigidbody.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BulletTarget>() != null)
        {
            //Hit Target
            StartCoroutine(SpawnVFX());
        }
        else
        {
            //HitSomethingElse
            StartCoroutine(SpawnVFX());
        }
        
    }

    IEnumerator SpawnVFX()
    {
        Transform vfx = Instantiate(VFX, transform.position, Quaternion.identity);
       

        yield return new WaitForSeconds(0.5f);

        Destroy(vfx.gameObject);
        Destroy(gameObject);
        Debug.Log("Destroy VFX");
        

    }
    

}

