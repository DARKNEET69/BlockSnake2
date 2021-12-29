using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour
{
    public Camera Camera;
    public Transform LevelPrefab;

    private GameManager GM;
    private Transform target;
    private int n = 0;

    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void LateUpdate()
    {        
        if (!GM.IsDied)
        {
            if ((n - transform.position.y) < 1)
            {
                n += 100;
                Instantiate(LevelPrefab, new Vector2(0, n), Quaternion.identity);
            }

            if (GameObject.FindGameObjectWithTag("Player"))
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
                transform.position = new Vector3(0, target.position.y + 5, -10);
            }
        }       
    }    

    public void GameRestart()
    {
        n = 0;        
    }

    public void GameRevive()
    {
        n -= 100;
    }
}
