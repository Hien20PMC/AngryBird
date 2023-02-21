using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float defend;
    public GameObject Smoke;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //do lon luc 
        if (collision.relativeVelocity.magnitude>defend)
        {
            Destroy(gameObject, 0.1f);

            Instantiate(Smoke,transform.position, Quaternion.identity);
        }
        else
        {
            defend -= collision.relativeVelocity.magnitude;
        }
    }
}
