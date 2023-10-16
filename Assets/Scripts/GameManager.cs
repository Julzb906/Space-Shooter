using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        //check if R key is pressed
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            //Current Game Scene
            SceneManager.LoadScene(1); 
        }

        //if the escape is pressed
        //quit game
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            
        }

    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
