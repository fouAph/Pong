using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Tombol untuk menggerakkan ke atas
    public KeyCode upButton = KeyCode.W;

    // Tombol untuk menggerakkan ke bawah
    public KeyCode downButton = KeyCode.S;

    // Kecepatan gerak
    public float speed = 10.0f;

    // Batas atas dan bawah game scene (Batas bawah menggunakan minus (-))
    public float topBoundary = 9.0f;
    public float bottomBoundary = 9.0f;


    // Rigidbody 2D raket ini
    private Rigidbody2D rigidBody2D;

    // Skor pemain
    private int score;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Dapatkan kecepatan raket sekarang.
        Vector2 velocity = rigidBody2D.velocity;

        // Jika pemain menekan tombol ke atas, beri kecepatan positif ke komponen y (ke atas).
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }

        // Jika pemain menekan tombol ke bawah, beri kecepatan negatif ke komponen y (ke bawah).
        else if (Input.GetKey(downButton) )
        {
            velocity.y = -speed;
        }

        // Jika pemain tidak menekan tombol apa-apa, kecepatannya nol.
        else
        {
            velocity.y = 0.0f;
        }

        // Masukkan kembali kecepatannya ke rigidBody2D.
        rigidBody2D.velocity = velocity;

        // Dapatkan posisi raket sekarang.
        Vector3 position = transform.position;

        // Jika posisi raket melewati batas atas (yBoundary), kembalikan ke batas atas tersebut.
        if (position.y > topBoundary)
        {
            position.y = topBoundary;
        }

        // Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas atas tersebut.
        else if (position.y < bottomBoundary)
        {
            position.y = bottomBoundary;
        }

        // Masukkan kembali posisinya ke transform.
        transform.position = position;
    }

    // Menaikkan skor sebanyak 1 poin
    public void IncrementScore()
    {
        score++;
    }

    // Mengembalikan skor menjadi 0
    public void ResetScore()
    {
        score = 0;
    }

    // Mendapatkan nilai skor
    public int Score
    {
        get { return score; }
    }
}
