using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Transform player;
    //odległość od końca poziomu
    public float levelExitDistance = 100;
    //punkt końca poziomu
    public Vector3 exitPosition;
    public GameObject exitPrefab;
	//zmienna - flaga - oznaczająca ukoñczenie poziomu
	public bool levelComplete = false;
    //taka sama zmienna tylko jeśli przegramy
    public bool levelFailed = false;
	// Start is called before the first frame update
	void Start()
    {
        //znajdz gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //wylosuj pozycję na kole o średnicy 100 jednostek
        Vector2 spawnCircle = Random.insideUnitCircle; //losowa pozycja x,y wewnątrz koła o r=1
        //chcemy tylko pozycję na okręgu, a nie wewnątrz koła
        spawnCircle = spawnCircle.normalized; //pozycje x,y w odległości 1 od środka
        spawnCircle *= levelExitDistance; //pozycja x,y w odległości 100 od środka
        //konwertujemy do Vector3
        //podstawiamy: x=x, y=0, z=y
        exitPosition = new Vector3(spawnCircle.x, 0, spawnCircle.y);
        Instantiate(exitPrefab, exitPosition, Quaternion.identity);

        //wystartuj czas
        Time.timeScale = 1f;
    }

	// Update is called once per frame
	void Update()
    {
        
    }
    //funkcja jest uruchamiana kiedy dany poziom (level) jest zakończony sukcesem
    public void OnSuccess()
    {
        //zatrzymaj fizykę gry
        Time.timeScale = 0f;
        //ustaw flagę - poziom zakończony
        levelComplete = true;
        //zatrzymaj ambientową muzykę
        Camera.main.transform.GetComponent<AudioSource>().Stop();
        //odegraj dźwięk końca poziomu
        Camera.main.transform.Find("LevelCompleteSound").GetComponent<AudioSource>().Play();
    }
    public void OnFailure()
    {
        Time.timeScale = 0f;
        levelFailed = true;
        Camera.main.transform.Find("GameOverSound").GetComponent<AudioSource>().Play();
    }
}
