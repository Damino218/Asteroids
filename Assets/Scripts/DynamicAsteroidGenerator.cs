using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAsteroidGenerator : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //znajd� gracza na scenie
        player = GameObject.FindWithTag("Player");
        //obr�� si� przodem (z+) do gracza
        transform.LookAt(player.transform.position);
        //odniesienie do rigidbody asteroidy
        rb = GetComponent<Rigidbody>();
        //popchnij asteroid� w kierunku gracza
        rb.AddForce(transform.forward * 1, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
