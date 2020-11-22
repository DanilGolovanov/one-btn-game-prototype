using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("Player object with PlayerController script attached.")]
    private BirdController bird;

    private Vector3 lastBirdPosition;
    private float distanceToMove;

    private void Start()
    {
        // get bird
        bird = FindObjectOfType<BirdController>();
        // get initial bird position
        lastBirdPosition = bird.transform.position;
    }

    private void Update()
    {
        MoveCameraWhenBirdMoves();
    }

    /// <summary>
    /// Move camera on x-axis at the same speed as player runs.
    /// </summary>
    private void MoveCameraWhenBirdMoves()
    {
        distanceToMove = bird.transform.position.x - lastBirdPosition.x;
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
        lastBirdPosition = bird.transform.position;
    }
}
