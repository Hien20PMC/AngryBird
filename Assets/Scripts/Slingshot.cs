using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField] private Vector3 dir;
    [SerializeField] private Vector3 originPos;

  
    [SerializeField] private float timer = 0;
    [SerializeField]
    private bool isFly;
    
    public float range = 5;
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;
    public Vector3 currentPosition;
    public float maxLenght;
    public float bottomBoundary;
    public GameObject birdPrefab;
    public float birdPositionOffset;
    public float force;
    [SerializeField]
    GameObject thisBird;
    [SerializeField]
    Collider2D birdCollider;
    bool isMouseDown;

    public GameObject PointPrefab;

    public GameObject[] Points;

    public int numberOfpoints;

    Vector2 Dir;
    void Start()
    {

        Points = new GameObject[numberOfpoints];

        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
        CreateBird();
        for (int i = 0; i < numberOfpoints; i++)
        {
            Points[i] = Instantiate(PointPrefab, transform.position, Quaternion.identity);
        }
    }
    //Tao ra chim 
    public void CreateBird()
    {

        thisBird = Instantiate(birdPrefab);
        birdCollider = thisBird.GetComponent<Collider2D>();
        birdCollider.enabled = false;

        ResetStrips();
    }

    void Update()
    {

        if (isFly)
        {

            Move();
            return;
        }

     

        if (isMouseDown)
        {


            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLenght);
            currentPosition = ClampBoundary(currentPosition);



            SetStrips(currentPosition);
            if (birdCollider)
            {
                birdCollider.enabled = true;
            }
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i].transform.position = PointPositon(i * 0.1f);
            }
        }
        else
        {

            ResetStrips();
        }

    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
    }

    private void Move()
    {

        timer += Time.deltaTime;
        thisBird.transform.position = (Vector2)originPos + ((Vector2)dir * (force) * timer) + 0.5f * Physics2D.gravity * (timer * timer);

    }
    private void Shoot()
    {
        originPos = thisBird.transform.position;
        isFly = true;
        dir = (center.transform.position - currentPosition);
       
        //bird.isKinematic = false;
        //Vector3 birdForce = (center.position - currentPosition) * force;
        //bird.velocity = birdForce;
        //bird.transform.position = (Vector2)bird.transform.position + ((Vector2)(currentPosition - center.transform.position) * (force * -1) * 1) + 0.5f * Physics2D.gravity * (1 * 1); ;
        //bird = null;
        //birdCollider = null;
        // Invoke("CreateBird", 2);
    }

    private void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }
    private void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);
        if (thisBird)
        {
            Vector3 dir = position - center.position;
            thisBird.transform.position = position + dir.normalized * birdPositionOffset;
        }
    }

    private Vector3 ClampBoundary(Vector3 vector) // Gioi han Strip
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 4000);
        return vector;
    }

    private Vector2 PointPositon(float t)
    {
        Vector2 currentPointPos = (Vector2)thisBird.transform.position + ((Vector2)(center.transform.position-currentPosition) * (force) * t) + 0.5f * Physics2D.gravity * (t * t);
        return currentPointPos;
    }

  
}
