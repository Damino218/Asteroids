using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f;
	public float flySpeed = 5f;
	//odniesienie do menedzera poziomu
	GameObject levelManagerObject;
    //stan os³on w procentach
    float shieldCapacity = 1;

	// Start is called before the first frame update
	void Start()
    {
		levelManagerObject = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {
        //dodaj do wspó³rzêdnych wartoœæ x=1, y=0, z=0 pomno¿one przez czas
        //mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //prezentacja dzia³¹nia wyg³adzonego sterowania (emulacja joystika)
        //Debug.Log(Input.GetAxis("Vertical"));

        //sterowanie prêdkoœci¹
        //stwórz nowy wektor przesuniêcia o wartoœæ 1 do przodu
        Vector3 movement = transform.forward;
        //pomnó¿ go przez czas do ostatniej klatki
        movement *= Time.deltaTime;
        //pomnó¿ go przez "Wychylenie joystika"
        movement *= Input.GetAxis("Vertical");
        //pomnó¿ przez prêdkoœæ lotu
        movement *= flySpeed;
        //dodaj ruch do obiektu
        //zmiana na fizykê
        // --- transform.position += movement;

        //komponent fizyki wewn¹trz gracza
        Rigidbody rb = GetComponent<Rigidbody>();
        //dodaj si³e - do przodu statku w trybie zmiany prêdkoœci
        transform.GetComponent<Rigidbody>().AddForce(movement, ForceMode.VelocityChange);


        //obrót
        //modyfikuj oœ "Y" obiektu player
        Vector3 rotation = Vector3.up;
        //przemnó¿ przez czas
        rotation *= Time.deltaTime;
        //przemnó¿ przez klawiaturê
        rotation *= Input.GetAxis("Horizontal");
        //pomnó¿ przez prêdkoœæ obrotu
        rotation *= rotationSpeed;
        //dodaj obrót do obiektu
        //nie mo¿emy u¿yæ += poniewa¿ unity u¿ywa Quaternionów do zapisu rotacji
        transform.Rotate(rotation);

		UpdateUI();

	}
        
	private void UpdateUI()
    {
        //metoda wykonuje wszystko zwi¹zane z aktualizacj¹ interfejsu u¿ytkownika

        //wyci¹gnij z menadzera poziomu pozycjê wyjœcia
        Vector3 target = levelManagerObject.GetComponent<LevelManager>().exitPosition;
        //obróæ znacznik w stronê wyjœcia
        transform.Find("NavUI").Find("TargetMarker").LookAt(target);
        //zmieñ iloœæ procentowo widoczn¹ w interfejsie
        TextMeshProUGUI shieldText =
            GameObject.Find("Canvas").transform.Find("ShieldCapacityText").GetComponent<TextMeshProUGUI>();
        shieldText.text = " Shield: " + (shieldCapacity*100).ToString() + "%";
	}

    private void OnCollisionEnter(Collision collision)
	{
        //uruchamia siê automatycznie jeœli zetkniemy siê z i innym coliderem

        //sprawdŸ czy dotknêliœmy asteroidy
        if (collision.collider.transform.CompareTag("Asteroid"))
        {
            //transform asteroidy
            Transform asteroid = collision.collider.transform;
            //policz wektor wed³ug którego odebchniemy asteroidê
            Vector3 shieldForce = asteroid.position - transform.position;
            //popchnij asteroidê
            asteroid.GetComponent<Rigidbody>().AddForce(shieldForce, ForceMode.VelocityChange);
        }
	}

}
