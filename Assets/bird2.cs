using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird2 : MonoBehaviour
{
    public GameObject bird;
    public Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
                 }

    // Update is called once per frame
    void Update()
    {
        move=bird.transform.position;
        move.x += 1f*Time.deltaTime;
        bird.transform.position = move;
    }
}
