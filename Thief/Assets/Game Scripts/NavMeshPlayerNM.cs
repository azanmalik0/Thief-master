using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class NavMeshPlayerNM : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource gameAudio;
    public AudioClip diamondPick;


    [Header("Animation")]

    public GameObject playerModel;
    public Animator playerAnimator;
    

    [Header("Other")]
    public Camera cam;
    public NavMeshAgent agent;
    public RaycastHit hit;
    public Transform exitPrefab;
    public NavMeshSurface navSurface;
    public float rotationSpeed;
    public LineController LC;
    


    private void Start()
    {
        
        playerAnimator =playerModel.GetComponent<Animator>();
        cam = FindObjectOfType<Camera>();
        navSurface.BuildNavMesh();
        LC=LineController.instance;
        Time.timeScale = 1;

    }

    public float T;
    void Update()
    {

        if (agent.destination == agent.transform.position)
        {
              playerAnimator.SetBool("Sneak", false);
        }


        if (Input.GetMouseButtonDown(0))
        {
          Ray ray=cam.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out hit))
            {
                playerAnimator.SetBool("Sneak",true);
               

                agent.SetDestination(hit.point);

                



            }
           
               // Debug.Log("Reached");
              
            
            
        LineController.instance.lr.enabled = true;
        }
        LineController.originPoint = transform.position;
        LineController.targetPoint = hit.point;

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Diamond"))
        {
            gameAudio.PlayOneShot(diamondPick);
            GuardAI.instance.gameAudio.PlayOneShot(GuardAI.instance.doorOpen);
            Destroy(other.gameObject);
            Vector3 pos = new Vector3(8.83f, 1.27f, -10.05f);
            Instantiate(exitPrefab,pos, Quaternion.identity);
        }

        if (other.CompareTag("Exit"))
        {

            SceneManager.LoadScene("GamePlay");


        }
       
    }
}
