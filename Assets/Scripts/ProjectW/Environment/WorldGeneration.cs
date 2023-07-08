using System.Collections;
using System.Collections.Generic;
using ProjectW.Utility;
using UnityEngine;

public class WorldGeneration : UnitySingleton<WorldGeneration>
{
    public int WorldSizeX, WorldSizeY;
    public int Mountains;
    public int Trees;
    
    
    private void Start()
    {
        
    }
    
    public void BuildWorld()
    {
        //On random places spawn the prefab obstacles
        //watch for size
        //cast a ray
        //check for layer and hit
    }
}
