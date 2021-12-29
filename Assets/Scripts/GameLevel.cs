using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public Transform LevelPrefab;
   
    private void LateUpdate()
    { 
        if (Mathf.Abs(transform.position.y - GameObject.FindGameObjectWithTag("MainCamera").transform.position.y) > 101) Destroy(gameObject);        
    }
}