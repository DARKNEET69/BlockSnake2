using System.Collections;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public TextMeshPro Counter;
    public int Health;
    public float ColorValue;
    public SpriteRenderer Render;

    private bool canDamaged = true;

    public void Awake()
    {
        int x = Random.Range(0, 10);
        int h = Mathf.RoundToInt(Random.Range(1, 16));

        if (x < 1)
        {
            Health = 30 + h;
        }
        else if (x < 5)
        {
            Health = 15 + h;
        }
        else
        {
            Health = h;
        }

        BGColor(Health);
        Counter.SetText(Health.ToString());
    }

    private void BGColor(int h)
    {
        if (h < 15)
        {
            float n = h / 15f;
            Render.color = new Color(0f, 1f, 1f - n);
            
        }
        else if (h < 30)
        {
            float n = (h - 15) / 15f;
            Render.color = new Color(n, 1f, 0f);
        }
        else
        {
            float n = (h - 30) / 15f;
            Render.color = new Color(1f, 1f - n, 0f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canDamaged)
        {
            StartCoroutine(Delay());
            Health--;
            BGColor(Health);
            Counter.SetText(Health.ToString());
            if (Health < 1) Destroy(gameObject);
        }        
    }

    IEnumerator Delay()
    {
        canDamaged = false;
        yield return new WaitForSeconds(0.05f);
        canDamaged = true;
    }
}
