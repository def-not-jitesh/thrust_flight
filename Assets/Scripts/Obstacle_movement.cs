using UnityEngine;

public class Obstacle_movement : MonoBehaviour {

	[SerializeField] float minSize = 0.5f; 
	[SerializeField] float maxSize = 2.0f; 
	[SerializeField] float minSpeed = 50f;
	[SerializeField] float maxSpeed = 100f; 
	Rigidbody2D rb; 

    	void Start() {

		rb = GetComponent<Rigidbody2D>(); 

        	float randomSize = Random.Range(minSize, maxSize); 
		transform.localScale = new Vector3(randomSize, randomSize, 1); 

		float randomSpeed = Random.Range(minSpeed, maxSpeed);
		Vector2 randomDirection = Random.insideUnitCircle; 	

		rb.AddForce(randomDirection * randomSpeed / randomSize); 
    	}

    	void Update() {
        
    	}
}
