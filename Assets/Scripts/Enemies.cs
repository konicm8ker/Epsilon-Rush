using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        print("Enemy " + gameObject.name + " hit!");
        Destroy(gameObject);
    }
}
