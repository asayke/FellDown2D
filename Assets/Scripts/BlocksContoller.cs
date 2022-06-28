using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Random = System.Random;

public class BlocksContoller : MonoBehaviour
{
    [SerializeField] private GameObject[] blockPrefabs = new GameObject[]{};
    [SerializeField] private float velocityForce;
    private GameObject firstWall;
    private Rigidbody2D firstWallRigibody2D;
    private GameObject nextWall;
    private Rigidbody2D nextWallRigidbody2D;
    
    private void Start()
    {
        firstWall = Instantiate(blockPrefabs[new Random().Next(0, blockPrefabs.Length)], new Vector3(0f, -3.2f, 4), Quaternion.identity);
        firstWallRigibody2D = firstWall.GetComponent<Rigidbody2D>();
        
        nextWall = Instantiate(blockPrefabs[new Random().Next(0, blockPrefabs.Length)], new Vector3(0.2f + 5 , -3.2f, 4), Quaternion.identity);
        nextWallRigidbody2D = nextWall.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        firstWallRigibody2D.velocity = Vector2.left * velocityForce;
        nextWallRigidbody2D.velocity = Vector2.left * velocityForce;
        
        if (firstWall.transform.position.x < -13)
        {
            float lastX = firstWall.transform.position.x;
            float between = lastX + 13f;
            Destroy(firstWall);

            firstWall = nextWall;
            firstWallRigibody2D = nextWallRigidbody2D;
            
            nextWall = Instantiate(blockPrefabs[new Random().Next(0, blockPrefabs.Length)], new Vector3(0.2f + 13 + between, -3.2f, 4), Quaternion.identity);
            nextWallRigidbody2D = nextWall.GetComponent<Rigidbody2D>();
        }
    }
}
