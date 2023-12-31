﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _degreesPerSecond = 12;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _degreesPerSecond) * Time.deltaTime);
    }

    //check for laser collission (Trigger)
    //instantiate explosion at the position of the astroid (us)
    //destroy the explosion after 3 seconds

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser") 
        {
            
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
            
        }
    }
}
