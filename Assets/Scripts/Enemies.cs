using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    [SerializeField] GameObject enemyDeathFX = null;
    [SerializeField] Transform parent = null;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int maxHits = 10;
    ScoreBoard scoreBoard;

    void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        Collider enemyBoxCollider = gameObject.AddComponent<BoxCollider>();
        enemyBoxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(scorePerHit);
        maxHits--;
        if(maxHits == 0)
        {
            KillEnemy();
        }
        Invoke("RestoreMaxHits", 25f);
        
    }

    private void RestoreMaxHits()
    {
        if(this.gameObject.tag == "Enemy 1")
        {
            this.scorePerHit = 12;
            this.maxHits = 2;
        }
        else if(gameObject.tag == "Enemy 2")
        {
            this.scorePerHit = 100;
            this.maxHits = 4;
        }
        else if(gameObject.tag == "Enemy 3")
        {
            this.scorePerHit = 512;
            this.maxHits = 6;
        }
        else if(gameObject.tag == "Boss")
        {
            this.scorePerHit = 1048;
            this.maxHits = 64;
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(enemyDeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        // Destroy(gameObject);
        this.gameObject.transform.localScale = new Vector3(0,0,0);
        Invoke("RestoreEnemyScale", 25f);
    }

    private void RestoreEnemyScale()
    {
        if(this.gameObject.tag == "Boss")
        {
            this.gameObject.transform.localScale = new Vector3(40,80,40);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(40,40,40);
        }
        RestoreMaxHits();
        
    }
}
