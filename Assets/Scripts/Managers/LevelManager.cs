using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Variables

    [SerializeField, Tooltip("Point ahead of camera where the pipes are generated.")]
    private Transform generationPoint;

    [Space]
    // distance between pipes
    private float distanceBetween;
    [SerializeField, Tooltip("Minimum distance between two corresponding pipes.")]
    private float minPipeGap;
    [SerializeField, Tooltip("Maximum distance between two corresponding pipes.")]
    private float maxPipeGap;
    [SerializeField, Tooltip("Minimum distance between two consecutive pipes.")]
    private float minDistanceBetween;
    [SerializeField, Tooltip("Maximum distance between two consecutive pipes.")]
    private float maxDistanceBetween;

    [SerializeField, Tooltip("Minimum height point down to which pipe generation point can go.")]
    private Transform minHeightPoint;
    // minimum height to which pipe generation point can go
    private float minHeight;
    [SerializeField, Tooltip("Maximum height point up to which pipe generation point can go.")]
    private Transform maxHeightPoint;
    // position of the maxHeightPoint
    private float maxHeight;
    [SerializeField, Tooltip("Maximum height difference between two consecutive pipes.")]
    private float maxHeightChange;
    // actual height change
    private float heightChange;

    #endregion

    #region Default Methods
    private void Start()
    {
        // min and max position of the pipe on y-axis
        minHeight = minHeightPoint.position.y;
        maxHeight = maxHeightPoint.position.y;
    }

    private void Update()
    {
        // if bird is behind the generation point
        if (transform.position.x < generationPoint.position.x)
        {
            CreateRandomPipes();
            MovePipeGenerator();
        }
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Move pipe generator to the end of the last pipe to avoid overlapping pipes.
    /// </summary>
    private void MovePipeGenerator()
    {
        transform.position = new Vector3(transform.position.x + Random.Range(minDistanceBetween, maxDistanceBetween), transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Create platform with a random width and in the random position.
    /// </summary>
    private void CreateRandomPipes()
    {
        // 0 - bottom pipe, 1 - top pipe
        Vector3[] pipePositions = new Vector3[2];
        // pick random position without overlapping
        pipePositions = PickRandomPipePositions();
        // create bottom pipe
        InstantiatePipe(pipePositions[0], false);
        // create top pipe
        InstantiatePipe(pipePositions[1], true);
    }

    private void InstantiatePipe(Vector3 position, bool createTop)
    {
        if (createTop)
        {
            Instantiate(GameAssets.GetInstance().topPipe, position, new Quaternion(0, 0, 180, 0));
        }
        else
        {
            Instantiate(GameAssets.GetInstance().bottomPipe, position, new Quaternion(0, 0, 0, 0));
        }
    }

    /// <summary>
    /// Pick 2 random positions for the corresponding top and bottom pipes using provided "boundaries" variables (e.g. maxHeightChange).
    /// </summary>
    private Vector3[] PickRandomPipePositions()
    {
        // pick random distance between pipes
        distanceBetween = Random.Range(minDistanceBetween, maxDistanceBetween);
        // change current position of the pipe generator by random value
        heightChange = transform.position.y + Random.Range(-maxHeightChange, maxHeightChange);
        // force height of the platform to be within screen height
        heightChange = Mathf.Clamp(heightChange, minHeight, maxHeight);
        // return position for new pipes
        Vector3[] pipePositions = new Vector3[2];
        // bottom pipe
        pipePositions[0] = new Vector3(transform.position.x + distanceBetween, heightChange, transform.position.z);
        // top pipe
        pipePositions[1] = new Vector3(transform.position.x + distanceBetween, heightChange + Random.Range(minPipeGap, maxPipeGap), transform.position.z);
        return pipePositions;
    }

    #endregion
}
