using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField]
    private float jumpMultiplier = 100f;

    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private float moveSpeed = 10;

    private ScoreManager scoreManager;
    private GameManager gameManager;

    private Animator myAnimator;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }
        rigidbody2D.velocity = new Vector2(moveSpeed, rigidbody2D.velocity.y);
        myAnimator.SetFloat("VerticalSpeed", rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        rigidbody2D.velocity = Vector2.up * jumpMultiplier;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // stop increasing the score
        scoreManager.scoreIncreasing = false;
        // stop the game and open the death menu
        gameManager.FinishGame();
    }
}
