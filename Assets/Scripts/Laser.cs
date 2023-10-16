using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Android;

public class Laser : MonoBehaviour
{

    [SerializeField]
    private float _speed = 8f;

    public bool _isEnemyLaser = false;
    

    // Update is called once per frame
    public void Update()
    {
        if (_isEnemyLaser == false) 
        {
            PlayerLaserUp();
        }
        else
        {
            EnemyLaserDown();
        }
        
    }

    void PlayerLaserUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            //checks for parent and destroys that as well
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
    
    void EnemyLaserDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    public void SetEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Player player = other.GetComponent<Player>();
        if (other.tag == "Player" && _isEnemyLaser == true) 
        {
            if (player != null) 
            { 

                player.Damage(); 
            }
        }
    }
}
