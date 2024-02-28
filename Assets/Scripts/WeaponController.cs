using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //zasi璕 broni
    public float range = 10f;

    //transform gracza
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // pozycja gracza
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = TagTargeter("Enemy");
        if (target != transform)
        {
            Debug.Log("Celuje do: " + target.gameObject.name);
            transform.LookAt(target.position + Vector3.up);
        }
    }
    Transform TagTargeter(string tag)
    {
        //tablica wszystkich obiekt闚 pasuj鉍ych do taga podanego jako agument
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);

        //szukamy najbli窺zego
        Transform closestTarget = transform;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            //wektor przesuni璚ia wzgl璠em gracza
            Vector3 difference = target.transform.position - player.position;
            //odleg這 od gracza
            float distance = difference.magnitude;

            if (distance < closestDistance && distance < range)
            {
                closestTarget = target.transform;
                closestDistance = distance;
            }
        }
        return closestTarget;
    }

    Transform LegeacyTargeter()
    {
        //znajdz wszystkie colidery w promieniu = range i zapisz je do tablicy collidersInRange
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, range);

        //do cel闚 testowych 
        //Debug.Log("Ilo collider闚 w zasi璕u broni: " +  collidersInRange.Length);

        //szukamy najbli窺zego przeciwnika

        Transform target = transform;
        float targetDistance = Mathf.Infinity;

        foreach (Collider collider in collidersInRange)
        {
            //wyci鉚nij transforma od tego coldiera

            //najpierw znajdz kapsu貫/model (w豉iciela colidera)
            GameObject model = collider.gameObject;

            if (model.transform.parent != null)
            {
                //znajdz rodzica modelu czyli przeciwnika
                GameObject enemy = model.transform.parent.gameObject;

                //sprawdz czy to co znalaz貫� jest przeciwnikiem
                if (enemy.CompareTag("Enemy"))
                {
                    //jei to przeciwnik to okre wektor przesuni璚ia
                    Vector3 diference = player.position - enemy.transform.position;
                    //policz d逝go wektora (odleg這)
                    float distance = diference.magnitude;
                    if (distance < targetDistance)
                    {
                        //znaleziono nowy cel bli瞠j
                        target = enemy.transform;
                        targetDistance = distance;
                    }
                }
            }


        }

        //do cel闚 testowych
        Debug.Log("Celuje do: " + target.gameObject.name);

        return target;
    }
}