using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX, maxX;
    [SerializeField] float speed = 5f;

    Ball ball;
    GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        float xPos=GetXPos();
        xPos = Mathf.Clamp(xPos, minX, maxX);
        Vector2 paddlePos = new Vector2(xPos, transform.position.y);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if(gameStatus.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            //return MouseMovement();
            return KeyboardMovement();

        }

    }

    private float KeyboardMovement()
    {
        float deltaX = transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        return deltaX;
    }

    private float MouseMovement()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        return mousePosInUnits;
    }
}
