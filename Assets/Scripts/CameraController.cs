using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //wsp�rz�dne gracza
    Transform player;
    //wysoko�� gracza
    public float CameraHeight = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        //pod��cz pozycj� gracza do lokalnej zmiennej korzystaj�c z jego taga
        //to nie jest zapisanie warto�ci jeden raz tylko referencja do obiektu
        //to znaczy, �e player zawsze b�dzie zawiera� aktualn� pozycj� gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //oblicz docelow� pozycj� kamery
        Vector3 targetPosition = player.position + Vector3.up * CameraHeight;

        //p�ynnie przesu� kamer� w kierunku gracza
        //funkcja Vector3.Lerp
        //p�ynnie przechodzi z pozycji pierwszego argumentu do pozycji drugiego w czasie trzeciego
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
    }
}
