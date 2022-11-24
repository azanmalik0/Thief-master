using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Transform exitPrefab;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Time.timeScale = 1;
    }


    void Update()
    {
        float moveX;
        float moveZ;
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveX, transform.position.y, moveZ);
        moveDirection *= speed * Time.deltaTime;
        rb.velocity = moveDirection;


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Diamond"))
        {
            Destroy(other.gameObject);
            Vector3 pos = new Vector3(8.83f, 1.27f, -10.05f);
            Instantiate(exitPrefab, pos, Quaternion.identity);
        }

        if (other.CompareTag("Guard"))
        {
            //GuardMovement.instance.gameObject.GetComponent<NavMeshAgent>().speed = 0;

            StartCoroutine(DeathDelay());
        }

        if (other.CompareTag("Exit"))
        {

            SceneManager.LoadScene("GamePlay");


        }
        if (other.CompareTag("FOV"))
        {
            GuardAI.instance.gameObject.transform.LookAt(transform);
            Time.timeScale = 0;
            StartCoroutine(DeathDelay());
            //GuardMovement.instance.gameObject.transform.rotation = Quaternion.Slerp(GuardMovement.instance.gameObject.transform.rotation,Quaternion.LookRotation(transform.position),rotationSpeed*Time.deltaTime);


        }
       
        
    }
        IEnumerator DeathDelay()
        {
            yield return new WaitForSecondsRealtime(3f);
            SceneManager.LoadScene("GamePlay");

        }
}
