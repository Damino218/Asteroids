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
        //znajd¿ gracza i przypisz do zmiennej
        player = GameObject.FindWithTag("Player").transform;

        //zeruj czas
        timeSinceSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //dolicz czas od ostatniej klatki
        timeSinceSpawn += Time.deltaTime;
        //je¿eli czas przekroczy³ sekundê to spawnuj i zresetuj
        if (timeSinceSpawn > 1)
        {
			GameObject asteroid = SpawnAsteroid(staticAsteroid);
            timeSinceSpawn = 0;
		}
        
    }

    GameObject SpawnAsteroid(GameObject prefab)
    {
        //generyczna funkcja s³u¿¹ca do wylosowania wspó³rzêdnych i umieszczenia
        //w tym miejscu asteroidy z prefaba

        //losowa pozycja w odleg³oœci 10 jednostek od œrodka œwiata
        Vector3 randomPosition = Random.onUnitSphere * 10;

        //na³ó¿ pozycjê gracza - teraz mamy pozycjê 10 jednostek od gracza
        randomPosition += player.position;

        //stwórz zmienn¹ asteroid, zespawnuj nowy asteroid koszystaj¹c z prefaba
        //w losowym miejscu, z rotacj¹ domyœln¹ (Quaternion.identity)
        GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

        //zwróæ asteroidê jako wynik dzia³ania
        return asteroid;
    }
}
