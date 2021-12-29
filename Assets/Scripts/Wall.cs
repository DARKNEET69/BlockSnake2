using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void Awake()
    {
        int x = Random.Range(0, 2);
        if (x == 0) Destroy(gameObject);
    }
}
