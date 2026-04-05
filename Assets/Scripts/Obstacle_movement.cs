using UnityEngine;

public class Obstacle_movement : MonoBehaviour {

	[SerializeField] float minSize = 0.5f; 
	[SerializeField] float maxSize = 2.0f; 
	[SerializeField] float minSpeed = 50f;
	[SerializeField] float maxSpeed = 100f; 
	
	Rigidbody2D rb; 
	private int health; 

    	void Start() {

		rb = GetComponent<Rigidbody2D>(); 

        	float randomSize = Random.Range(minSize, maxSize); 
		transform.localScale = new Vector3(randomSize, randomSize, 1); 
		health = Mathf.FloorToInt(randomSize * 5); 

		float randomSpeed = Random.Range(minSpeed, maxSpeed);
		Vector2 randomDirection = Random.insideUnitCircle; 	

		rb.AddForce(randomDirection * randomSpeed / randomSize); 
    	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Bullet")) {
			health = health - 5; 

			if (health <= 0) {
				Destroy(gameObject); 
			}
		}
	}
}
