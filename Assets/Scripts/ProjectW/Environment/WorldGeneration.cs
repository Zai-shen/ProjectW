using System.Collections;
using System.Collections.Generic;
using ProjectW.Utility;
using Unity.AI.Navigation;
using UnityEngine;

public class WorldGeneration : UnitySingleton<WorldGeneration>
{
    public int WorldSizeX, WorldSizeZ;
    public int Mountains;
    public int Trees;
    public GameObject MountainPrefab;
    public GameObject TreePrefab;

    public NavMeshSurface NavigationMeshSurface;
    
    public void BuildWorld()
    {
        BuildMountains();
        BuildTrees();

        RecalculateNavMesh();
    }

    private void RecalculateNavMesh()
    {
        NavigationMeshSurface.BuildNavMesh();
    }


    private void BuildMountains()
    {
        Collider mountainColl = MountainPrefab.GetComponent<Collider>();
        Vector3 mountainSize = mountainColl.bounds.size;

        BuildObjects(Mountains ,mountainSize, MountainPrefab);
    }

    private void BuildTrees()
    {
        Collider treeColl = TreePrefab.GetComponent<Collider>();
        Vector3 treeSize = treeColl.bounds.size;

        BuildObjects(Trees ,treeSize, TreePrefab);
    }

    private void BuildObjects(int amount, Vector3 objectSize, GameObject prefab)
    {
        for (int i = 0; i < amount; i++)
        {
            if (FindFreePlace(objectSize, out Vector3 place))
            {
                GameObject go = GameObject.Instantiate(prefab, place, Quaternion.identity);
                Quaternion rotation = new Quaternion();
                Vector3 view = Random.insideUnitSphere;
                view.y = 0;
                rotation.SetLookRotation(view, Vector3.up);
                go.transform.localRotation = rotation;
            }
            else
            {
                Debug.Log("No available place");
            }
        }
    }

    private bool FindFreePlace(Vector3 objectSize, out Vector3 result)
    {
        result = default;
        int maxIterations = 50;
        int iteration = 0;
        bool searching = true;
        int playerSpacingRadiusBox = 10;
        
        while (searching && iteration < maxIterations)
        {
            iteration++;

            float randomX = Random.Range(-WorldSizeX / 2, WorldSizeX / 2);
            float randomZ = Random.Range(-WorldSizeZ / 2, WorldSizeZ / 2);
            Vector3 randomPosition = new Vector3(randomX, 50, randomZ);

            if (randomX >= 0 && randomX < playerSpacingRadiusBox)
                continue;
            if (randomX <= 0 && randomX > -playerSpacingRadiusBox)
                continue;
            
            if (randomZ >= 0 && randomZ < playerSpacingRadiusBox)
                continue;
            if (randomZ <= 0 && randomZ > -playerSpacingRadiusBox)
                continue;

            
            if (!(randomPosition.x < (WorldSizeX / 2) - objectSize.x
                  && randomPosition.x > (-WorldSizeX / 2) + objectSize.x))
                continue;
            if (!(randomPosition.z < (WorldSizeZ / 2) - objectSize.z
                  && randomPosition.z > (-WorldSizeZ / 2) + objectSize.z))
                continue;

            RaycastHit hit;
            if (Physics.Raycast(randomPosition, Vector3.down, out hit, 100f))
            {
                if (!hit.transform.CompareTag("Ground"))
                {
                    continue;
                }

                searching = false;
            }

            result = hit.point;
        }

        return !searching;
    }
}
