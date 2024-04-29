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

    //odleg�o�� w jakiej spawnuj� si� asteroidy
    public float spawnDistance = 10;

    public float safeDistance = 10;

    //odst�p pomi�dzy spawnem kolejnych asteroid
    public float cooldown = 0;

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
		//generyczna funkcja s�u��ca do wylosowania wsp�rz�dnych i umieszczenia
		//w tym miejscu asteroidy z prefaba

		//stworz losow� pozycj� na okr�gu (x,y)
		Vector2 randomCirclePosition = Random.insideUnitCircle.normalized;

		//losowa pozycja w odleg�o�ci 10 jednostek od �rodka �wiata
		//mapujemy x->x, y->z, a y ustawiamy 0
		Vector3 randomPosition = new Vector3(randomCirclePosition.x, 0, randomCirclePosition.y) * spawnDistance;

        //na�� pozycj� gracza - teraz mamy pozycj� 10 jednostek od gracza
        randomPosition += player.position;

        //sprawd� czy miejsce jest wolne
        //! oznacza "nie" czyli nie ma nic w promieniu 5 jednostek od miejsca randomPosition
        if(!Physics.CheckSphere(randomPosition, safeDistance))
        {
			//stw�rz zmienn� asteroid, zespawnuj nowy asteroid koszystaj�c z prefaba
			//w losowym miejscu, z rotacj� domy�ln� (Quaternion.identity)
			GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

			//zwr�� asteroid� jako wynik dzia�ania
			return asteroid;
		}
        else
        {
            return null;
        }
    }
    void AsterodCountControl()
    {
        //przygotuj tablic� wszystkich asteroid�w 
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        //przejd� p�tl� przez wszystkie
        foreach (GameObject asteroid in asteroids)
        {
            //odleg�o�� od gracza

            //wektor przesuni�cia mi�dzy graczem a asteroid�
            //(o lie musz� przesun�� gracza, �eby znalaz� si� w miejscu asteroidy)
            Vector3 delta = player.position - asteroid.transform.position;

            //magnitude to d�ugo�� wektora = odleg�o�ci od gracza
            float distanceToPlayer = delta.magnitude;

            if( distanceToPlayer > 30 )
            {
                Destroy(asteroid);
            }
        }
    }
}
