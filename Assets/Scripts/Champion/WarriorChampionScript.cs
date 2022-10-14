using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class WarriorChampionScript : MonoBehaviour
{
    [Header("Warrior Champion Settings")]
    public float attackCooldownTime;
    public float attkCooldownTimer;
    public bool enemyOccupied;
    private float moveSpeed;
    private Vector2 direction;
    private float directionNumber;

    
    private float mana;
    private float baseDamage;
    private float baseDefence;

    [Header("GameObjects/Transforms")]
    
    public Transform AttackPoint;
    public float attackRange;
    public LayerMask enemyLayerMask;
    public GameObject EnemyRaycastObject;
    public float enemyRayDistance;
    public GameObject WarriorChampionUnit;
    private Rigidbody2D rb;
    public GameObject closestEnemy;

    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
    }
    void Start()
    {
        closestEnemy = null;
        if (this.gameObject.tag == "P1")
        {
            direction = Vector2.right;
            directionNumber = 1f;
        }
        if (this.gameObject.tag == "P2")
        {
            direction = Vector2.left;
            directionNumber = -1f;
            enemyRayDistance = enemyRayDistance * -1;
        }
    }

    void Update()
    {
        DetectInFrontEnemy();
    }

    void DetectInFrontEnemy()
    {
        RaycastHit2D EnemyHit = Physics2D.Raycast(EnemyRaycastObject.transform.position, direction * new Vector2(directionNumber, 0f), enemyRayDistance, enemyLayerMask);
        if (EnemyHit.collider != null)
        {
            if (this.gameObject.tag == "P2") { EnemyHit.distance = -EnemyHit.distance; } // For Test/Debug
            Debug.DrawRay(EnemyRaycastObject.transform.position, direction * EnemyHit.distance * new Vector2(directionNumber, 0f), Color.red);
            enemyOccupied = true;
             moveSpeed = 0f;
            if (enemyOccupied)
            {
                MeleeAttack();
            }
        }
        else
        {
            enemyOccupied = false;
            Animator anim = WarriorChampionUnit.GetComponent<Animator>();
            anim.SetTrigger("Walk");
            moveSpeed = (this.gameObject.tag == "P2") ? -0.6f : 0.6f; // soon      
        }
        WarriorChampionMove(moveSpeed);
    }

    void MeleeAttack()
    {
        if(closestEnemy != null){
            attkCooldownTimer -= Time.deltaTime;
            if (attkCooldownTimer <= 0.0f)
            {
                attkCooldownTimer = attackCooldownTime;
                Animator anim = WarriorChampionUnit.GetComponent<Animator>();
                anim.SetTrigger("Attack");
                closestEnemy.GetComponent<DamageScript>().DamageDealt(21); // soon
            }
        }
    }

    public void WarriorSkill1()
    {

    }

    public void WarriorSkill2()
    {

    }

    public void Teleport()
    {

    }

    void OnDrawGizmosSelected() //Drawing Gizmos
    {
        if (AttackPoint == null)
            return;
            Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }

    void WarriorChampionMove(float speed)
    {
        rb.velocity = new Vector2(speed, 0f);
    }

}
