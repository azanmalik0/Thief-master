using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class GuardAI : MonoBehaviour
{
    [Header("Animation")]

    public GameObject guardModel;
    public Animator guardAnimator;


    [Header("Audio")]

    public AudioSource gameAudio;
    public AudioSource BGM;
    public AudioClip playerDeath;
    public AudioClip theme;
    public AudioClip doorOpen;
    public AudioClip gunFire;

    [Header("Other")]
    public static GuardAI instance;
    public Transform[] points;
    public int nextPoint;
    public NavMeshAgent agent;
    public Transform player;
    public ParticleSystem muzzleFlash;
    public ParticleSystem playerBleed;
    public GameObject keyPrefab;
    public float spinSpeed;

    public float sightRange, attackRange, timeBetweenAttacks;

    public bool playerInSightRange, playerInAttackRange, readyToAttack = true;

    public LayerMask whatIsPlayer, whatIsObstacle;
    public GameObject point;
    public GameObject nearDeathPanel;


    [Header("HealthBar")]
    public float maxHP = 100000;
    public float currentHP;
    public HealthBar healthBar;


    [Header("Raycasting")]
    public Ray ray;
    RaycastHit hitinfo;
    public float raycastRange;

    private void Awake()
    {
        instance = this;
        BGM.PlayOneShot(theme);
    }
    private void Start()
    {
        keyPrefab.transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed);
        guardAnimator = guardModel.GetComponent<Animator>();
        readyToAttack = false;
        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);
        nextPoint = 0;
        muzzleFlash.gameObject.SetActive(false);
        playerBleed.gameObject.SetActive(false);
        nearDeathPanel.gameObject.SetActive(false);



    }
    private void Update()
    {

        ray = new Ray(point.transform.position, point.transform.forward);



        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (playerInSightRange && !playerInAttackRange)
        {
            guardAnimator.SetBool("Fire", false);
            ChasePlayer();


        }
        if (playerInAttackRange && playerInSightRange)
        {
            guardAnimator.SetBool("Fire", true);
            AttackPlayer();

        }
        if (!playerInSightRange && !playerInAttackRange)
        {
            guardAnimator.SetBool("Fire", false);

            Patrol();
        }
        if (readyToAttack && playerInAttackRange)
        {

            TakeDamage(0.2f);

        }
        if (!playerInSightRange && readyToAttack == false)
        {
            guardAnimator.SetBool("Fire", false);
            Patrol();
        }
        if (playerInSightRange && readyToAttack == false && playerInAttackRange)
        {
            guardAnimator.SetBool("Fire", false);
            Patrol();
        }
        if (playerInSightRange && readyToAttack == false && !playerInAttackRange)
        {
            guardAnimator.SetBool("Fire", false);
            ChasePlayer();
        }
        if (currentHP <= 0)
        {

            SceneManager.LoadScene("GamePlay");

        }
        if (currentHP <= 29f)
        {
            nearDeathPanel.gameObject.SetActive(true);

        }

    }
    void Patrol()
    {
        readyToAttack = false;
        muzzleFlash.gameObject.SetActive(false);
        playerBleed.gameObject.SetActive(false);
        agent.SetDestination(points[nextPoint].position);

        if (transform.position.x == points[nextPoint].position.x)
        {


            nextPoint++;
            if (nextPoint >= points.Length)
            {

                nextPoint = 0;
            }
        }
    }
    void TakeDamage(float damage)
    {

        muzzleFlash.gameObject.SetActive(true);
        playerBleed.gameObject.SetActive(true);
        currentHP -= damage;
        healthBar.SetHealth(currentHP);

    }
    private void ChasePlayer()
    {
        readyToAttack = false;
        muzzleFlash.gameObject.SetActive(false);
        playerBleed.gameObject.SetActive(false);

        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        raycastRange = Vector3.Distance(point.transform.position, player.transform.position);

        agent.SetDestination(this.transform.position);
        transform.LookAt(player);


        if (!Physics.Raycast(ray, out hitinfo, raycastRange, whatIsObstacle))
        {
            readyToAttack = true;

            Debug.DrawLine(ray.origin, ray.origin + point.transform.forward * raycastRange, Color.green);






        }
        else
        {

            readyToAttack = false;
            Debug.DrawLine(ray.origin, ray.origin + point.transform.forward * raycastRange, Color.red);



        }
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}


