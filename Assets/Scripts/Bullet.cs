using UnityEngine;

public class Bullet : MonoBehaviour {

	Rigidbody2D rb; 
	[SerializeField] float bulletSpeed = 10f; 

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		rb.linearVelocity = (transform.up * bulletSpeed); 
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (!collision.gameObject.CompareTag("Player")) {
			Destroy(gameObject); 
		}
	}
}
