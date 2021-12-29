using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class SnakeController : MonoBehaviour
{
    public GameManager GM;
    public TextMeshPro Counter;
    public float ForwardSpeed = 10;
    public float Sensitivity = 1;
    public int Length = 5;    

    private Rigidbody2D componentRigidbody;
    private Vector2 exPos;
    private float horizontalSpeed = 0;
    private bool isDied;

    public Transform SnakeHead;
    private float SegDis;    

    private List<Transform> segments = new List<Transform>();
    private List<Vector2> positions = new List<Vector2>();

    private float acceleration = 0;
    private bool damage_delay = true;

    private void Awake()
    {
        positions.Add(SnakeHead.position);
        Length = 5;
    }

    private void Start()
    {
        if (SnakeHead.Find("japan").gameObject.activeSelf || SnakeHead.Find("ukraine").gameObject.activeSelf || SnakeHead.Find("ussr").gameObject.activeSelf) SegDis = 3;
        else SegDis = 2;
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        componentRigidbody = GetComponent<Rigidbody2D>();
        for (int i = 0; i < Length; i++) AddBody(1);
    }

    private void Update()
    {
        if (!GM.IsDied)
        {
            horizontalSpeed = 0;

            if (Input.GetMouseButtonDown(0))
            {
                exPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                horizontalSpeed = 0;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 delta = (Vector2)Input.mousePosition - exPos;
                horizontalSpeed += delta.x * Sensitivity;
                exPos = Input.mousePosition;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                AddBody(10);
                Length++;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                RemoveBody();
                Length--;
            }            
        }

        componentRigidbody.velocity = new Vector2(horizontalSpeed * 5, ForwardSpeed + acceleration);

        float distance = ((Vector2)SnakeHead.position - positions[0]).magnitude;

        if (distance > SegDis)
        {
            Vector2 direction = ((Vector2)SnakeHead.position - positions[0]).normalized;

            positions.Insert(0, positions[0] + direction * SegDis);
            positions.RemoveAt(positions.Count - 1);

            distance -= SegDis;
        }

        for (int i = 0; i < segments.Count; i++)
        {
            segments[i].position = Vector2.Lerp(positions[i + 1], positions[i], distance / SegDis);
        }

        Counter.SetText(segments.Count.ToString());
        if (acceleration < 100) acceleration += 0.002f;
        else acceleration = 100;
    }

    public void AddBody(int x)
    {
        for (int i = x; i > 0; i--)
        {
            Transform Segment = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity, transform);
            segments.Add(Segment);
            positions.Add(Segment.position);
        }        
    }

    public void RemoveBody()
    {
        if (segments.Count > 1)
        {
            Destroy(segments[segments.Count - 1].gameObject);
            segments.RemoveAt(segments.Count - 1);
            positions.RemoveAt(segments.Count + 1);
        }
        else
        {
            GM.Death();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            AddBody(collision.gameObject.GetComponent<Apple>().Health);
            Length++;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            if (damage_delay)
            {
                StartCoroutine(Delay());
                RemoveBody();
                Length--;
            }            
        }
    }

    IEnumerator Delay()
    {
        damage_delay = false;
        yield return new WaitForSeconds(0.05f);
        damage_delay = true;
    }
}