using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControll : MonoBehaviour
{
    // Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;
    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    //maksimal kecepatan bola
    public float maxBallSpeed;

    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    //berapa kali maximal benturan dengan player sebelum mengubah arah secara random
    public int maxImpactBeforeRandomDir = 7;

    [Header("Private Variable"), Space(10)]
    [SerializeField] float ballVelocity;
    [SerializeField] bool isGoingLeft;
    [SerializeField] int impactLeft;

    void Start()
    {
        impactLeft = maxImpactBeforeRandomDir;

        rigidBody2D = GetComponent<Rigidbody2D>();

        // Mulai game
        RestartGame();
    }


    private void FixedUpdate()
    {
        ballVelocity = rigidBody2D.velocity.magnitude;
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     maxBallSpeed++;
        // }
        // else if(Input.GetKeyDown(KeyCode.V))
        // {
        //     maxBallSpeed--;
        // }


        if (rigidBody2D.velocity.magnitude > maxBallSpeed)
        {
            rigidBody2D.velocity = Vector2.ClampMagnitude(rigidBody2D.velocity, maxBallSpeed);
        }
    }

    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
        impactLeft = maxImpactBeforeRandomDir;
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
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce), ForceMode2D.Force);
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce), ForceMode2D.Force);
            // rigidBody2D.AddForce()
        }

    }

    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();

        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 1);


    }

    public void ReRoute()
    {
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
        float randomDirection = Random.Range(0, 2);

        // Jika nilainya di bawah 1, bola bergerak ke kiri. 
        // Jika tidak, bola bergerak ke kanan.
        if (isGoingLeft)
        {
            // Gunakan gaya untuk menggerakkan bola ini.
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce / yRandomInitialForce), ForceMode2D.Force);
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce / yRandomInitialForce), ForceMode2D.Force);
            // rigidBody2D.AddForce()
        }
    }

    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;

        if (collision.gameObject.tag == "Player")
        {
            // print(collision.transform.name);
            //untuk mencegah arah bola ketika hanya memantul 1 arah (Sumbu x)
            //jika waktu sejak bola tidak menyentuh player, maka arah bola akan berubah secara acak
            if (collision.gameObject.name == "Player1")
            {
                isGoingLeft = false;

            }

            if (collision.gameObject.name == "Player2")
            {
                isGoingLeft = true;

            }

            if (impactLeft > 0)
                impactLeft--;

            else
            {
                ReRoute();
                impactLeft = maxImpactBeforeRandomDir;
            }
        }

    }

    // Untuk mengakses informasi titik asal lintasan

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
