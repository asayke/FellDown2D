using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class BlocksContoller : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float velocityForce;
    [SerializeField] private int minDisable;
    [SerializeField] private int maxDisable;
    private GameObject firstWall;
    private Rigidbody2D firstWallRigibody2D;
    private GameObject nextWall;
    private Rigidbody2D nextWallRigidbody2D;
    private bool isItFirst = true;
    private Random rng = new Random();
    private float wallPrefabWidth;

    public void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void MixBlocks(GameObject wall)
    {
        Random random = new Random();
        int countDisable = random.Next(minDisable, maxDisable + 1);

        List<Transform> blocks = wall.GetComponentsInChildren<Transform>().ToList();
        blocks.Remove(blocks[0]);
        Shuffle(blocks);

        if (!isItFirst)
        {
            for (int i = 0; i < countDisable; i++)
            {
                blocks[i].gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        wallPrefabWidth = wallPrefab.GetComponent<BoxCollider2D>().size.x;
        firstWall = Instantiate(wallPrefab, new Vector3(-4, -3, 0), Quaternion.identity);
        MixBlocks(firstWall);
        isItFirst = false;
        firstWallRigibody2D = firstWall.GetComponent<Rigidbody2D>();

        nextWall = Instantiate(wallPrefab, new Vector3(-4 + wallPrefabWidth, -3, 0), Quaternion.identity);
        MixBlocks(nextWall);
        nextWallRigidbody2D = nextWall.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        firstWallRigibody2D.velocity = Vector2.left * velocityForce;
        nextWallRigidbody2D.velocity = Vector2.left * velocityForce;

        if (firstWall.transform.position.x < -12 - wallPrefabWidth / 2)
        {
            Destroy(firstWall);

            firstWall = nextWall;
            firstWallRigibody2D = nextWallRigidbody2D;

            nextWall = Instantiate(wallPrefab, new Vector3(-4 + wallPrefabWidth, -3, 9), Quaternion.identity);
            MixBlocks(nextWall);
            nextWallRigidbody2D = nextWall.GetComponent<Rigidbody2D>();
        }
    }
}