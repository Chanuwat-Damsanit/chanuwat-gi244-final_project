using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSfx;
    public AudioClip crashSfx;

    private Rigidbody rb;
    private InputAction jumpAction;
    private bool isOnGround = true;
    private bool canDoubleJump = false;

    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;

    public InputAction dashAction;

    public int playerHp = 0;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        Physics.gravity *= gravityModifier;

        jumpAction = InputSystem.actions.FindAction("Jump");
        dashAction = InputSystem.actions.FindAction("Sprint"); 

        gameOver = false;

        playerHp = 3; 
    }

    void Update()
    {
        if (jumpAction.triggered && isOnGround && !gameOver)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSfx);
            canDoubleJump = true; 
        }

        else if (jumpAction.triggered && !isOnGround && !gameOver && canDoubleJump) 
        {
            canDoubleJump = false; 
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSfx);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerHp -= 1;

            var explosion = Instantiate(explosionParticle, collision.transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion.gameObject, 1);

            //ObstacleObjectPool Return
            ObstacleObjectPool.GetInstance().Return(collision.gameObject);

            playerAudio.PlayOneShot(crashSfx);
            if (playerHp <= 0)
            {
                Debug.Log("Game Over!");
                gameOver = true;
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                dirtParticle.Stop();
            }
        }
    }
}