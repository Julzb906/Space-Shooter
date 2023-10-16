using System.Collections;
using System.Collections.Generic;
using System.Transactions;

using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    
    [SerializeField] //0 = TripleShot 1 = Speed 2 = Shields
    private int _powerupID;
    private AudioSource _audioSource;

    private float _minFlickerDuration = 0.15f;
    private float _maxFlickerDuration = 0.15f;
    private bool _isFlickering;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
       
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            

            Player player = other.transform.GetComponent<Player>();
            if (player != null) 
            {
                
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;

                }
               
            }
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject,0.8f);

            if (!_isFlickering) 
            {
                StartCoroutine(Flicker());
            }
        }

        
    }
    private IEnumerator Flicker()
    {
        _isFlickering = true;
        _spriteRenderer.color = new Color (1f, 1f, 1f, 0f); //makes transparent
        yield return new WaitForSeconds(Random.Range(_minFlickerDuration, _maxFlickerDuration));
        _spriteRenderer.color = new Color (1f, 1f, 1f, 1f);
        _isFlickering = false;
    }

}
