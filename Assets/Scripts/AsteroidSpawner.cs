using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    //gracz (jego pozycja)
    Transform player;

    //prefab statycznej asteroidy
    public GameObject staticAsteroid;

    //czas od ostatnio wygenerowanej asteroidy
    float timeSinceSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //znajd� gracza i przypisz do zmiennej
        player = GameObject.FindWithTag("Player").transform;

        //zeruj czas
        timeSinceSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //dolicz czas od ostatniej klatki
        timeSinceSpawn += Time.deltaTime;
        //je�eli czas przekroczy� sekund� to spawnuj i zresetuj
        if (timeSinceSpawn > 1)
        {
			GameObject asteroid = SpawnAsteroid(staticAsteroid);
            timeSinceSpawn = 0;
		}
        
    }

    GameObject SpawnAsteroid(GameObject prefab)
    {
        //generyczna funkcja s�u��ca do wylosowania wsp�rz�dnych i umieszczenia
        //w tym miejscu asteroidy z prefaba

        //losowa pozycja w odleg�o�ci 10 jednostek od �rodka �wiata
        Vector3 randomPosition = Random.onUnitSphere * 10;

        //na�� pozycj� gracza - teraz mamy pozycj� 10 jednostek od gracza
        randomPosition += player.position;

        //stw�rz zmienn� asteroid, zespawnuj nowy asteroid koszystaj�c z prefaba
        //w losowym miejscu, z rotacj� domy�ln� (Quaternion.identity)
        GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

        //zwr�� asteroid� jako wynik dzia�ania
        return asteroid;
    }
}
