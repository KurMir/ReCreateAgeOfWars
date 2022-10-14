using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateScript : MonoBehaviour
{
    [Header("Ultimate Settings")]
    [SerializeField] private float ultimateMoveSpeed;
    [Header("GameObjects/Tranforms")]
    [SerializeField] private GameObject nearestEnemyRayCast;
    [SerializeField] private LayerMask enemyLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectNearestEnemy();
    }

    void DetectNearestEnemy() {

    }
}
