using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class characterScript : MonoBehaviour
{
    public Rigidbody2D rb;
    Animator anim;

    private float Speed = 5;
    private float JumpSpeed = 300;
    private float HorizontalInput;
    private bool Jumping;

    public float health = 100;
    public Slider healthBar;
    float score = 0;
    public Text scoreText;

    void Start()
    {
        anim = GetComponent<Animator>();
        scoreText.text = "Score: " + score.ToString();
        anim.SetTrigger("Idle");
        anim.SetBool("Move", false);
    }

    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(HorizontalInput) > 0)
        {
            anim.SetBool("Move", true);
            anim.ResetTrigger("Idle");
        }
        else
        {
            anim.SetBool("Move", false);
            anim.SetTrigger("Idle");
        }

        rb.velocity = new Vector2(HorizontalInput * Speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && !Jumping)
        {
            Jump();
        }

        if (score == 100)
        {
            SceneManager.LoadScene("GameWon");
        }

        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        Vector2 characterPos = transform.position;
        if (characterPos.y <= -4.5f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Jumping = false;
        }

        if (col.gameObject.tag == "Enemy")
        {
            ContactPoint2D contact = col.GetContact(0);
            Vector2 collisionDirection = contact.normal;

            if (Vector2.Dot(collisionDirection, Vector2.up) > 0.5f)
            {
                score += 10;
                scoreText.text = "Score: " + score.ToString();
                Destroy(col.gameObject);
            }
            else
            {
                health -= 20;
                healthBar.value = health;
            }
        }

        if (col.gameObject.name.StartsWith("apple"))
        {
            health += 20;
            healthBar.value = health;
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Coin")
        {
            score += 10;
            scoreText.text = "Score: " + score.ToString();
            Destroy(col.gameObject);
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * JumpSpeed);
        Jumping = true;
        anim.SetTrigger("Jump");
        anim.SetBool("Move", false);
    }
}
