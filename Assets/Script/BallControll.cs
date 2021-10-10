using UnityEngine;

public class BallControll : MonoBehaviour
{
    public GameManager manager;
    // Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;

    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    //Kecepatan bola di awal
    [Tooltip("Kecepatan awal bola")]
    public float initialBallMagnitudSpeed = 10;

    //maksimal kecepatan bola, walaupun mendapat gaya tambahan dari raket
    [Tooltip("Maksimal kecepatan bola")]
    public float maxBallMagnitudeSpeed;

    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    [Header("Private Variable/Debugging"), Space(10)]
    [SerializeField] float ballVelocity;
    [SerializeField] bool isGoingLeft;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        // Mulai game
        RestartGame();
    }


    private void FixedUpdate()
    {
        ballVelocity = rigidBody2D.velocity.magnitude;

        if (rigidBody2D.velocity.magnitude > maxBallMagnitudeSpeed)
        {
            rigidBody2D.velocity = Vector2.ClampMagnitude(rigidBody2D.velocity, maxBallMagnitudeSpeed);
        }
    }

    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        // Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
        float randomDirection = Random.Range(0, 2);

        // Jika nilainya di bawah 1, bola bergerak ke kiri. 
        // Jika tidak, bola bergerak ke kanan.
        if (randomDirection < 1.0f)
        {
            // Gunakan gaya untuk menggerakkan bola ini.
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce).normalized * (initialBallMagnitudSpeed * 10) / 2, ForceMode2D.Force);
        }

        else
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce).normalized * (initialBallMagnitudSpeed * 10) / 2, ForceMode2D.Force);
            // rigidBody2D.AddForce()
        }
    }

    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();
        manager.player1.transform.position = new Vector2(manager.player1.transform.position.x, 0);
        manager.player2.transform.position = new Vector2(manager.player2.transform.position.x, 0);

        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 1);
    }

    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.name == "Player1")
            {
                isGoingLeft = false;
            }

            if (collision.gameObject.name == "Player2")
            {
                isGoingLeft = true;

            }
        }
    }

    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
