﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //handle to text
    [SerializeField]
    private Text _scoreText;
   
    [SerializeField]
    private Image _LivesImg;
    [SerializeField] 
    private Sprite[] _liveSprites;
    
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //assign text component to the handle
        _scoreText.text = "Score: " + 0;

        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if ( _gameManager == null) 
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    public void Update()
    {
       
    }

    public void UpdateScore(int playerScore) 
    {
        
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    { 
        //display img sprite
        //give it a new one based on current lives index
        _LivesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0) 
        {
            GameOverSequence();
        }
    }

    public void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _restartText.gameObject.SetActive(true);
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
   
}
