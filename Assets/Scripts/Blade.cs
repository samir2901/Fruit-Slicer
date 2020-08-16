using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;



public class Blade : MonoBehaviour
{
    public GameObject bladeTrailPrefab;    
    public float minCuttingVelocity = 0.1f;
    public int score = 0;
    public Text scoreTxt;

    Vector2 previousPos;
    bool isCutting = false;
    GameObject currentTrail;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        scoreTxt.text = "0";
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }
        if (isCutting)
        {
            updateBlade();
        }
    }    

    void updateBlade()
    {        
        Vector2 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPos;
        float velocity = (newPos - previousPos).magnitude * Time.deltaTime;
        if(velocity > minCuttingVelocity)
        {
            circleCollider.enabled = true;
        }
        else
        {
            circleCollider.enabled = false;
        }
        previousPos = newPos;
    }
    void StartCutting()
    {
        isCutting = true;
        currentTrail = Instantiate(bladeTrailPrefab, transform);
        previousPos = cam.ScreenToWorldPoint(Input.mousePosition);
        circleCollider.enabled = false;
    }
    void StopCutting()
    {
        isCutting = false;
        currentTrail.transform.SetParent(null);
        Destroy(currentTrail, 2f);
        circleCollider.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            score += 10;
            scoreTxt.text = score.ToString(); 
        }
    }
}
