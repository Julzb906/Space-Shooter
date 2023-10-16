using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 3f;
    private float _canFire = -1;
    private Animator _animator;
    private AudioSource _audioSource;
  
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) 
        {
            Debug.LogError("Player is NULL");
        }
       
        //assign the component
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator is NULL");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Explosion sound is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

     
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].SetEnemyLaser();
            }
            
        }
    }

    void CalculateMovement() 
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }

    }


    void OnTriggerEnter2D(Collider2D other) 
    
    {
        if (other.tag == "Player") 
        {
           
            Player player = other.transform.GetComponent<Player>();
            _canFire = 100;

            if (player != null) 
            {
                player.Damage();
            }
            
            //trigger anim
            _animator.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            _speed = 0;
            Destroy(this.gameObject, 3.0f);


        }


        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _canFire = 100;

            if (_player != null) 
            {
                _player.AddScore(10);
            }
            //trigger anim
            _animator.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            _speed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 3.0f);
        }

    }

}
