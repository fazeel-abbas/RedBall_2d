using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonScript : MonoBehaviour
{
    /* // Start is called before the first frame update
     void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {

     } */

    public void GameMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void Level_1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Level_2()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void Level_3()
    {
        SceneManager.LoadScene("Level_3");
    }

}
