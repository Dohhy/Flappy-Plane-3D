using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public ParticleSystem cloudExplosionParticle;

    public ParticleSystem planeExplosionParticle;
    public ParticleSystem smokeParticle;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private Animator animator;

    private float m_speed = 2.8f;

    private float m_minJumpForce = 2.4f;
    private float m_maxJumpForce = 2.8f;

    private bool m_isFalling = false;

    [Header("Sound Components")]
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip scoreSound;
    public AudioClip hitCloudSound;
    public AudioClip hitGroundSound;

    //Jump Stuffs
    private bool m_isJumpable = true;

    private void Update()
    {
        if (transform.position.x <= 20.0f) {
            transform.Translate(Vector3.right * Time.deltaTime * m_speed * 2, Space.World);
        }
        else if (transform.position.x > 20.0f) {
            transform.Translate(Vector3.right * Time.deltaTime * m_speed, Space.World);
        }
        animator.SetFloat("height_f", transform.position.y);

        if (transform.position.y > 3.0f)
        {
            m_isJumpable = false;
        }
        else if (transform.position.y <= 2.7f)
        {
            m_isJumpable = true;
        }
    }

    private void Jump()
    {
        audioSource.PlayOneShot(jumpSound);
        animator.SetTrigger("jump_trig");
        float jumpForce = Random.Range(m_minJumpForce, m_maxJumpForce);
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pizza"))
        {
            if (!GameManager.isPlaneCrashed) {
                audioSource.PlayOneShot(scoreSound);
                gameManager.AddScore(1);
            }
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            audioSource.PlayOneShot(hitGroundSound);
            Instantiate(planeExplosionParticle, transform.position, planeExplosionParticle.transform.rotation);
            Instantiate(smokeParticle, transform.position, smokeParticle.transform.rotation);
            gameManager.GameOver();
        }
        if (other.gameObject.CompareTag("Cloud"))
        {
            Instantiate(cloudExplosionParticle, other.transform.position, cloudExplosionParticle.transform.rotation);
            Destroy(other.gameObject);
            audioSource.PlayOneShot(hitCloudSound);
            gameManager.CloudCollision();
        }
    }

    //Buttons
    public void JumpButton()
    {
        if (m_isJumpable) { Jump(); }
    }
}
