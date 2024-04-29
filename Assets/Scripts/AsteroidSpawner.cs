#nullable enable
using NUnit.Framework.Constraints;
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

    //odleg³oœæ w jakiej spawnuj¹ siê asteroidy
    public float spawnDistance = 10;

    public float safeDistance = 10;

    //odstêp pomiêdzy spawnem kolejnych asteroid
    public float cooldown = 0;

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
        if(timeSinceSpawn > cooldown)
        {
            SpawnAsteroid(staticAsteroid);
            timeSinceSpawn = 0;
        }

        AsterodCountControl();

        timeSinceSpawn += Time.deltaTime;
    }

    GameObject? SpawnAsteroid(GameObject prefab)
    {
		//generyczna funkcja s³u¿¹ca do wylosowania wspó³rzêdnych i umieszczenia
		//w tym miejscu asteroidy z prefaba

		//stworz losow¹ pozycjê na okrêgu (x,y)
		Vector2 randomCirclePosition = Random.insideUnitCircle.normalized;

		//losowa pozycja w odleg³oœci 10 jednostek od œrodka œwiata
		//mapujemy x->x, y->z, a y ustawiamy 0
		Vector3 randomPosition = new Vector3(randomCirclePosition.x, 0, randomCirclePosition.y) * spawnDistance;

        //na³ó¿ pozycjê gracza - teraz mamy pozycjê 10 jednostek od gracza
        randomPosition += player.position;

        //sprawd¿ czy miejsce jest wolne
        //! oznacza "nie" czyli nie ma nic w promieniu 5 jednostek od miejsca randomPosition
        if(!Physics.CheckSphere(randomPosition, safeDistance))
        {
			//stwórz zmienn¹ asteroid, zespawnuj nowy asteroid koszystaj¹c z prefaba
			//w losowym miejscu, z rotacj¹ domyœln¹ (Quaternion.identity)
			GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

			//zwróæ asteroidê jako wynik dzia³ania
			return asteroid;
		}
        else
        {
            return null;
        }
    }
    void AsterodCountControl()
    {
        //przygotuj tablicê wszystkich asteroidów 
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        //przejdŸ pêtl¹ przez wszystkie
        foreach (GameObject asteroid in asteroids)
        {
            //odleg³oœæ od gracza

            //wektor przesuniêcia miêdzy graczem a asteroid¹
            //(o lie muszê przesun¹æ gracza, ¿eby znalaz³ siê w miejscu asteroidy)
            Vector3 delta = player.position - asteroid.transform.position;

            //magnitude to d³ugoœæ wektora = odleg³oœci od gracza
            float distanceToPlayer = delta.magnitude;

            if( distanceToPlayer > 30 )
            {
                Destroy(asteroid);
            }
        }
    }
}
