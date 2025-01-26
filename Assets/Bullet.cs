using System.Collections;
using System.Collections.Generic;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float spinSpeed = 5f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            return;
        }
        rb.AddForce(transform.up * speed * rb.mass / Time.fixedDeltaTime);
        rb.angularVelocity = spinSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Scene scene = SceneManager.GetActiveScene();
            if (rb == null)
            {
                
                SceneManager.LoadScene(scene.name);
                return;
            }
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
            SceneManager.LoadScene(scene.name);
        }
        Destroy(gameObject);
    }
}
