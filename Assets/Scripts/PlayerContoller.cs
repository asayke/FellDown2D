using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float rotateToMapForce;
    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D playerRigibody2D;
    private GameObject player;
    private Collider2D playerCollider2D;
    private bool isOnMap;
    
    private bool isGrounded()
    {
        float extraHeight = 0.01f;
        RaycastHit2D LowerRaycastHit2D = Physics2D.Raycast(playerCollider2D.bounds.center, Vector2.down,
            playerCollider2D.bounds.extents.y + extraHeight, platformLayerMask);
        RaycastHit2D UpperRaycastHit2D = Physics2D.Raycast(playerCollider2D.bounds.center, Vector2.up,
            playerCollider2D.bounds.max.y, platformLayerMask);
        
        return LowerRaycastHit2D.collider != null || UpperRaycastHit2D.collider != null;
    }

    public void ChangeGravity()
    {
        if (isGrounded() && isOnMap)  
            playerRigibody2D.gravityScale *= -1;
    }

    private void Start()
    {
        player = Instantiate(playerPrefab, new Vector3(-10f, -2.7f, 0), Quaternion.identity);
        playerRigibody2D = player.GetComponent<Rigidbody2D>();
        playerCollider2D = player.GetComponent<Collider2D>();
        playerRigibody2D.AddForce(Vector2.right * rotateToMapForce);
    }

    private void Update()
    {
        if (player.transform.position.x >= -5.5f)
        {
            isOnMap = true;
            playerRigibody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
}