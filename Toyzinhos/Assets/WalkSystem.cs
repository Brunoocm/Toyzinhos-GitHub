using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WalkSystem : MonoBehaviour
{
    public ParticleSystem particleExplosion;
    public Transform pos;
    public AudioClip danoAudio;
    AudioSource audioSource;

    [Header("Nav Mesh")]
    NavMeshAgent agent;
    public GameObject[] EnemyTransform;
    public GameObject myObject;
    public LayerMask whatIsGround, whatIsEnemy;
    public string tagEnemy;
    Transform childTransform;
    Transform closestTarget;

    [Header("Range Attack")]
    public float timeBetweenRangeAttacks;
    private bool alreadyRangeAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("Melee Attack")]
    public float timeBetweenMeleeAttacks;
    private bool alreadyMeleeAttacked;

    public float sightMeleeRange, attackMeleeRange;
    public bool playerInSightMeleeRange, playerInAttackMeleeRange;

    public bool isMelee;
    public bool isRange;
    private bool oneTime;

    public int dano = 5;

    Animator anim;
    Rigidbody rb;

    //public AttackState attackState;
    //public bool isInAttackRange;

    private void Awake()
    {
        childTransform = GetComponentInChildren<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();


    }

    private void Update()
    {
        EnemyTransform = GameObject.FindGameObjectsWithTag(tagEnemy);

        CheckIsActive();

        //GetObjects();


        //if (EnemyTransform == null)
        //{
        //    EnemyTransform = GameObject.FindGameObjectWithTag(tagEnemy).transform;
        //}
        for (int i = 0; i < EnemyTransform.Length; i++)
        {

        }

        if (isMelee)
        {
            playerInSightMeleeRange = Physics.CheckSphere(transform.position, sightMeleeRange, whatIsEnemy);
            playerInAttackMeleeRange = Physics.CheckSphere(transform.position, attackMeleeRange, whatIsEnemy);

            //if (!playerInSightMeleeRange && !playerInAttackMeleeRange) Patroling();
            if (playerInSightMeleeRange && !playerInAttackMeleeRange) ChasePlayer();
            if (playerInSightMeleeRange && playerInAttackMeleeRange) AttackPlayer();
        }

        if (isRange)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);

            //if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();

        }

        if (!playerInAttackMeleeRange)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    void CheckIsActive()
    {

        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject pointsTarget in EnemyTransform)
        {
            Vector3 directionToTarget = pointsTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                //for (int i = 0; i < EnemyTransform.Length; i++)
                //{
                //    pointsTarget
                //}

                if (pointsTarget == myObject)
                    continue;
                closestDistanceSqr = dSqrToTarget;
                closestTarget = pointsTarget.transform;

            }
            else
            {

            }


        }

    }

    //void GetObjects()
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        EnemyTransform[i] = transform.GetChild(i).transform;

    //    }

    //}

    void ChasePlayer()
    {
        agent.SetDestination(closestTarget.position);
    }

    void AttackPlayer()
    {
        if (isRange)
        {
            agent.SetDestination(transform.position);

            if(closestTarget != null)childTransform.LookAt(new Vector3(closestTarget.position.x, transform.position.y, closestTarget.position.z));

            if (!alreadyRangeAttacked)
            {
                float dist = Vector3.Distance(closestTarget.position, transform.position);

                ////Rigidbody rb = Instantiate(bullet, pos.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                ////rb.AddForce(transform.forward * 1.7f * dist, ForceMode.Impulse);
                ////rb.AddForce(transform.up * dist / 4.3f, ForceMode.Impulse);
                anim.SetTrigger("Attack");

                alreadyRangeAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenRangeAttacks);
            }
        }

        if (isMelee)
        {
            agent.SetDestination(transform.position);

            childTransform.LookAt(new Vector3(closestTarget.position.x, transform.position.y, closestTarget.position.z));

            if (!alreadyMeleeAttacked)
            {
                anim.SetTrigger("Attack");

                alreadyMeleeAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenMeleeAttacks);
            }
        }

    }

    private void ResetAttack()
    {
        if (isRange) alreadyRangeAttacked = false;
        if (isMelee) alreadyMeleeAttacked = false;
    }

    public void Attack()
    {
        if (closestTarget != null)
        {
            float randomNum = Random.Range(0.1f, 1f);
            closestTarget.gameObject.GetComponent<HealthSystem>().Damage(dano);
            Instantiate(particleExplosion, closestTarget.gameObject.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(danoAudio, randomNum);
        }
    

    }
    public void Heal()
    {
        if (closestTarget != null)
        {
            closestTarget.gameObject.GetComponent<HealthSystem>().Heal(dano);
        }

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackMeleeRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightMeleeRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
