using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.UIElements;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour {

	[SerializeField] float thrustForce = 10f;
	[SerializeField] float maxSpeed = 5f;
       	[SerializeField] GameObject boosterFlame; 	
	[SerializeField] float scoreMultiplier = 10f;
       	[SerializeField] UIDocument uiDocument; 
	[SerializeField] GameObject explosionEffect;
	[SerializeField] GameObject bullet;	
	Rigidbody2D rb;

	private float elapsedTime = 0f; 	
	private Label scoreText; 
	private Label bulletText; 
	private Button restartButton; 
	private float score = 0f; 
	private int bulletCount = 20;

	void Start() {
		rb = GetComponent<Rigidbody2D>(); 
		scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
		bulletText = uiDocument.rootVisualElement.Q<Label>("BulletLabel"); 
		restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton"); 
		restartButton.style.display = DisplayStyle.None; 
		restartButton.clicked += ReloadScene; 
	}

	void Update() {
		
		elapsedTime += Time.deltaTime;
		score = Mathf.FloorToInt(elapsedTime*scoreMultiplier); 
		scoreText.text = "Score: " + score; 
		bulletText.text = "Bullets: " + bulletCount; 

		if (Mouse.current.leftButton.isPressed) {
			
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
			Vector2 direction = (mousePos - transform.position).normalized; 

			transform.up = direction; 
			rb.AddForce(direction * thrustForce); 

			if (rb.linearVelocity.magnitude > maxSpeed) {
				rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed; 
			}

		}

		if (Keyboard.current.qKey.wasPressedThisFrame && bulletCount > 0) {
    			Vector3 spawnPos = transform.position + (Vector3)(Vector2)transform.up * 1.5f; 
			Instantiate(bullet, spawnPos, transform.rotation); 
			bulletCount = bulletCount - 1;
		}

		if (Mouse.current.leftButton.wasPressedThisFrame) {
			boosterFlame.SetActive(true);
		} else if (Mouse.current.leftButton.wasReleasedThisFrame) {
			boosterFlame.SetActive(false);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.CompareTag("Obstacle")) {
			Instantiate(explosionEffect, transform.position + (transform.up * 5.0f), transform.rotation); 
			Destroy(gameObject);
			restartButton.style.display = DisplayStyle.Flex; 
		}
	}

	void ReloadScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
	}
}
