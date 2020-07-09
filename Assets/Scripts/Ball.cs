using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f, yPush = 15f;
    [SerializeField] AudioClip[] ballSound;
    [SerializeField] float randomVel=0.2f;

    Vector2 paddleToBallVector;
    bool hasStarted = false;

    //Cached component
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LaunchOnMouseClick();
            LockBallToPaddle();
        }
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(xPush,yPush);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioClip clip = ballSound[UnityEngine.Random.Range(0,ballSound.Length)];
        myAudioSource.PlayOneShot(clip);
        myRigidBody2D.velocity += new Vector2(UnityEngine.Random.Range(0f,randomVel), UnityEngine.Random.Range(0f, randomVel));
    }

}
