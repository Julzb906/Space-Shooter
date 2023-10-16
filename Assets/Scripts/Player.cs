using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedBoost = 5;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private GameObject _SpeedPowerUpPrefab;
    [SerializeField]
    private GameObject _ShieldsPowerUpPrefab;
    [SerializeField]
    private GameObject _ShieldsVisual;
    [SerializeField]
    private GameObject _RightHurtVisual;
    [SerializeField]
    private GameObject _LeftHurtVisual;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    private bool _isShieldsActive;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;
    public bool alive;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogWarning("The Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is Null");
        }

        _ShieldsVisual.SetActive(false);

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Laser is NULL");
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);

        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }
    //speed boost move 8.5
    //5 sec
    public void SpeedBoostActive()
    {
        _speed += _speedBoost;
        StartCoroutine(SpeedUpDisableRoutine());
    }

    IEnumerator SpeedUpDisableRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        _speed -= _speedBoost;
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
          
        }

        //play the laser audio clip
        _audioSource.Play();
    }


    public void ShieldsActive()
     {
        _isShieldsActive = true;
        _ShieldsVisual.SetActive(true);
     }

    public void Damage() 
    {
        //if shields is active
        //do nothing...
        //dactivate shields
        //return;
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _ShieldsVisual.SetActive(false);
            return;
        }

        _lives--;

        _uiManager.UpdateLives(_lives);

        //if lives is 2
        //enable right engine
        //else if lives is 1
        //enable left engine
        if (_lives == 2) 
        { 
            _LeftHurtVisual.SetActive(true);
        }
        else if (_lives == 1) 
        { 
            _RightHurtVisual.SetActive(true);
        }
        else if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive() 
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        if (_isTripleShotActive == true);
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
        } 
    }



    //method to add 10 to the score
    // communicate with the UI to update the score
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

   
}


