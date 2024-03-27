using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasherController : MonoBehaviour
{
    //gracz
    GameObject player;
    //prędkość podążania za graczem
    public float walkSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //patrz się na gracza
        transform.LookAt(player.transform.position);
        //idz do przodu
        transform.position += transform.forward * Time.deltaTime * walkSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Trafiony");
        //obiekt z którym mamy kolizje
        GameObject projectile = collision.gameObject;

        //tylko jeśli trafił nas gracz
        if(projectile.CompareTag("PlayerProjectile"))
        {
            //zniknij pocisk

            Destroy(projectile);

            //zniknij przeciwnika
            Destroy(transform.gameObject);
        }
       /* if (collision.gameObject.CompareTag("Player"))
        {
            //weszlismy w gracza - poinformuj go o tym
            collision.gameObject.GetComponent<PlayerController>().Hit(gameObject);
        }*/
    }
}
