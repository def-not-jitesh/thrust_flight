using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour {

	[SerializeField] float thrustForce = 10f;
	[SerializeField] float maxSpeed = 5f;
       	[SerializeField] GameObject boosterFlame; 	
	[SerializeField] float scoreMultiplier = 10f;
       	[SerializeField] UIDocument uiDocument; 
	[SerializeField] GameObject explosionEffect; 	
	Rigidbody2D rb;

	private float elapsedTime = 0f; 	
	private Label scoreText; 
	private float score = 0f; 

	void Start() {
		rb = GetComponent<Rigidbody2D>(); 
		scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
	}

	void Update() {
		
		elapsedTime += Time.deltaTime; 
		score = Mathf.FloorToInt(elapsedTime*scoreMultiplier); 
		scoreText.text = "Score: " + score; 

		if (Mouse.current.leftButton.isPressed) {
			
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
			Vector2 direction = (mousePos - transform.position).normalized; 

			transform.up = direction; 
			rb.AddForce(direction * thrustForce); 

			if (rb.linearVelocity.magnitude > maxSpeed) {
				rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed; 
			}

		}

		if (Mouse.current.leftButton.wasPressedThisFrame) {
			boosterFlame.SetActive(true);
		} else if (Mouse.current.leftButton.wasReleasedThisFrame) {
			boosterFlame.SetActive(false);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (!collision.gameObject.CompareTag("Wall")) {
			Instantiate(explosionEffect, transform.position, transform.rotation); 
			Destroy(gameObject);
		}
	}
}
