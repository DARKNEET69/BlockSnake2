using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public GameObject Snake;
    public GameObject Camera;
    public GameObject Scenes;
    public GameObject DeathScreen;
    public bool IsDied = true;

    private GameObject snakeClone;
    private string skin;
    private Vector3 snakePosition;    


    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Skin")) PlayerPrefs.SetString("Skin", "green");
        skin = PlayerPrefs.GetString("Skin");
        if (!PlayerPrefs.HasKey("BuySkin")) PlayerPrefs.SetString("BuySkin", "green");        
        if (!PlayerPrefs.HasKey("Coin")) PlayerPrefs.SetInt("Coin", 0);

        Menu();        
    }

    public void Play()
    {
        ChangeSceneTo("Game");
        NewGame();
    }

    public void Shop()
    {
        ChangeSceneTo("Shop");
        EndGame();
    }

    public void Menu()
    {
        DeathScreen.SetActive(false);
        ChangeSceneTo("Menu");
        EndGame();
    }

    public void ChangeSceneTo(string scene)
    {
        
        for (int i = 0; i < Scenes.transform.childCount; i++)
        {
            if (Scenes.transform.GetChild(i).name == scene)
            {
                Scenes.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Scenes.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void NewGame()
    {
        IsDied = false;
        snakePosition = new Vector3(0, 0, 50);
        CreateSnake();     
    }

    public void EndGame()
    {
        DestroySnake();       
    }

    public void Death()
    {
        IsDied = true;
        snakePosition = snakeClone.transform.position;
        Camera.transform.position = new Vector3(0, Camera.transform.position.y - 50, -10);
        Destroy(GameObject.Find("Level(Clone)"));
        DestroySnake();
        DeathScreen.SetActive(true);
    }

    public void Restart()
    {
        DeathScreen.SetActive(false);
        Destroy(GameObject.Find("Level(Clone)"));
        NewGame();
    }
    public void Respawn()
    {
        DeathScreen.SetActive(false);
        Destroy(GameObject.Find("Level(Clone)"));
        CreateSnake();
    }

    public void Exit()
    {
        Application.Quit();
    }

    //-------------------SNAKE--------------------

    public void CreateSnake()
    {
        if (!snakeClone)
        {
            snakeClone = Instantiate(Snake, snakePosition, Quaternion.identity);
            for (int i = 0; i < snakeClone.transform.Find("Head").childCount; i++)
            {
                snakeClone.transform.Find("Head").GetChild(i).gameObject.SetActive(false);
            }            
            snakeClone.transform.Find("Head").Find(skin).gameObject.SetActive(true);
            snakeClone.transform.SetParent(transform.Find("Game"));
        }
    }   

    public void DestroySnake()
    {
        if (snakeClone) Destroy(snakeClone.gameObject);        
    }
    
    public void SnakeSkin(string skin)
    {
        this.skin = skin;
        PlayerPrefs.SetString("Skin", this.skin);
    }
}
