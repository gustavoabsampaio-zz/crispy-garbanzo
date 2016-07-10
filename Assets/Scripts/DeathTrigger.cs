using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class DeathTrigger : MonoBehaviour {

    

	void Start () {
	
	}
	
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
