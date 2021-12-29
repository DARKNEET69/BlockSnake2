using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
        Destroy(gameObject);
    }
}
