using UnityEngine;

public class Apple : MonoBehaviour
{
    public int Health = 1;
    public Transform CoinPrefab;
    
    public void Awake()
    {        
        int a = Random.Range(0, 100);
        int x = Random.Range(-2, 3) * 10;
        transform.position = new Vector2(x, transform.position.y);

        if (a < 1 && transform.position.y > 1000)
        {
            Instantiate(CoinPrefab, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
        else Health = Random.Range(1, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
