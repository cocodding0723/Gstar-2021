using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    NavMeshAgent e_enemy =null;

    [SerializeField]
    Transform[] e_WayPoints = null;
    int e_Count = 0;                    
    // Start is called before the first frame update
    void Start()
    {
        e_enemy = GetComponent<NavMeshAgent>();
        //InvokeRepeating("MoveToNextPointer",0f,2f);
        StartCoroutine(MoveToNextPointer());
    }

    IEnumerator MoveToNextPointer()
    {
        while(true)
        {
            if(e_enemy.velocity == Vector3.zero)            //속도가 0이 되면
            {
                e_enemy.SetDestination(e_WayPoints[e_Count++].position);
                Debug.Log(e_Count);
                if(e_Count >= e_WayPoints.Length)
                {
                    e_Count =0;
                }
            
            }
            yield return new WaitForSeconds(2f);
        }
 
    }
    
}
