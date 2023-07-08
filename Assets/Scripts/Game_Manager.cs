using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public List<GameObject> Enemys;
    public bool EndGame = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Target");
        Enemys = new List<GameObject>();
        Enemys = enemies.ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEndGame())
        {
            EndGame = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }


    bool isEndGame()
    {
        foreach(GameObject obj in Enemys)
        {
            if (obj.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
