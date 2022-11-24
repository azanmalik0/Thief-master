//using UnityEngine;
//using UnityEngine.AI;

//public class LevelGenerator : MonoBehaviour
//{

//    public int width;
//    public int height;
//    public float minV;
//    public NavMeshSurface navSurface;

//    public GameObject obstacle;
//    public GameObject player;
//    public GameObject jewel;

//    private bool playerSpawned = false;
//    private bool jewelSpawned = false;

//    // Use this for initialization
//    void Start()
//    {
//        GenerateLevel();
//        navSurface.BuildNavMesh();
//    }

//    // Create a grid based level
//    void GenerateLevel()
//    {
//        // Loop over the grid
//        for (int x = 0; x <= width; x += 2)
//        {
//            for (int y = 0; y <= height; y += 2)
//            {
//                // Should we place a wall?
//                if (Random.value > minV)
//                {
//                    // Spawn a wall
//                    Vector3 pos = new Vector3(x - width / 2f, 1f, y - height / 2f);
//                    Instantiate(obstacle, pos, Quaternion.identity, transform);
//                }
//                else if (!playerSpawned) // Should we spawn a player?
//                {
//                    // Spawn the player
//                    Vector3 pos = new Vector3(x - width / 2f, 1.25f, y - height / 2f);
//                    Instantiate(player, pos, Quaternion.identity);
//                    playerSpawned = true;
//                }
//                else if (!jewelSpawned) // Should we spawn the jewel?
//                {
//                    // Spawn the jewel
//                    Vector3 pos = new Vector3(x - width / 4f, 2.25f, y - height / 4f);
//                    Instantiate(jewel, pos, jewel.transform.rotation);
//                    jewelSpawned = true;

//                }
//            }
//        }
//    }

//}
