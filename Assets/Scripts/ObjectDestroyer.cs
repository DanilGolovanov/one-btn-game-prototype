using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField, Tooltip("DestructionPoint object from the scene.")]
    private GameObject platformDestructionPoint;

    private void Start()
    {
        platformDestructionPoint = GameObject.Find("DestructionPoint");
    }

    private void Update()
    {
        // destroy object if platform destruction point passed it (means destruction point is on the right)
        if (transform.position.x < platformDestructionPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}
