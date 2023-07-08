using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BakerMovement : MonoBehaviour
{
    private Camera _mainCam;
    private NavMeshAgent _agent;
    
    float maxAngle = 50f;
    float maxAngleIncrease = 5f;
    float maxDistance = 10f;
    float minDistance = 6f;
    
    void Start()
    {
        _mainCam = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (DestinationReached())
        {
            StartNewMovement();
        }
        
        // if (Input.GetMouseButtonDown(0))
        // {
        //     // Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;
            //
            // if (Physics.Raycast(ray, out hit))
            // {
            //     _agent.SetDestination(hit.point);
            // }
        // }
    }

    private bool RandomTrueFalse()
    {
        return (Random.value > 0.5f);
    }

    private void StartNewMovement()
    {
        float tempMaxAngle = 0f;
        float distance = Random.Range(minDistance, maxDistance);
        
        int maxIterations = 50;
        int iteration = 0;
        bool searching = true;

        while (searching && iteration < maxIterations)
        {
            iteration++;
            tempMaxAngle = maxAngle + (maxAngleIncrease * iteration);

            float notEnoughChange = 15f;
            float angle = Random.Range(notEnoughChange, tempMaxAngle);
            angle *= RandomTrueFalse() ? 1 : -1;

            GameObject coneRanger = new GameObject("coneRanger");
            coneRanger.transform.position = transform.position;
            coneRanger.transform.localRotation = transform.localRotation *= Quaternion.Euler(0, angle, 0);
            coneRanger.transform.position += coneRanger.transform.forward * distance;

            Vector3 newDestination = coneRanger.transform.position;
            Destroy(coneRanger);

            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, coneRanger.transform.position, NavMesh.AllAreas, path))
            {
                _agent.SetDestination(newDestination);
                searching = false;
            }
        }

    }

    private bool DestinationReached()
    {
        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (!_agent)
            return;
        
        Gizmos.DrawLine(transform.position, _agent.destination);
    }
}
