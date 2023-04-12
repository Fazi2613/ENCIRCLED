using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class AiController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform target; 
    public float distance;
    public int minTime = 0;
    public int maxTime = 4;
    public bool killed = false;
    public int maxHealth  = 100;
    public int currentHealth;
    public int enemiesKilled = 0;
    public int numHits = 0;
    public bool isDead = false;
    public float deathTimer = 10.0f;
    WaveSpawner spawner;
    Collider _collider;
    Rigidbody rb;

    
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        StartCoroutine(WaitBefore());
        target = GameObject.FindWithTag("Player").transform;
    }

    private IEnumerator WaitBefore()
    {
    float waitTime = UnityEngine.Random.Range(minTime, maxTime);
    yield return new WaitForSeconds(waitTime);
    }

    void Update()
    {
        if(killed == false)
        {
            agent.SetDestination(target.position);
            anim.SetFloat("speed", agent.velocity.magnitude);
            float distance = Vector3.Distance(agent.transform.position,target.transform.position);
            if(Input.GetKeyDown(KeyCode.O))
            {
            anim.SetTrigger("distanceT");
            }
            if(Input.GetKeyDown(KeyCode.K))
            {
            anim.SetTrigger("death");
            }
        }
        else if (killed == true)
        {
            gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            agent.SetDestination(agent.transform.position);
            anim.SetFloat("speed", 0);
            
        }               
       if(Input.GetKeyDown(KeyCode.L))
            {
                killed = true;
                
            }
        if(Input.GetKeyDown(KeyCode.N))
            {
                killed = false;
            }
    }
    
    public void TakeDamage()
    {
        currentHealth -= 25;
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            enemiesKilled+=1;
            Die();
        }
    }

    public void Die()
    {
        if(spawner != null) spawner.currentEnemy.Remove(gameObject);
        rb.isKinematic = true;
        rb.detectCollisions = false;
        _collider.enabled = false;
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        GameplayController.instance.EnemyKilled();
        Debug.Log("Target has been destroyed!");
        anim.SetTrigger("isDead");
        Destroy(gameObject,10);
    }

    public void setSpawner(WaveSpawner _spawner){
        spawner = _spawner;
    }
}