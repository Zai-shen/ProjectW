using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PowderLevelHandler : MonoBehaviour
{
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Text PowderLVLText;

    private int tmpint;
    public void OnLevelUp(){
        // trigger when new level is reached
        Globals.FlourNextLevel *= 2;
        Globals.FlourLevel += 1;

        int Rand;
        int Lenght = 3;
        int noptions = 7;
        List<int> selection = new List<int>();
        selection = new List<int>(new int[Lenght]);
     
            for (int j = 1; j < Lenght; j++)
            {
                Rand = Random.Range(0,noptions);
     
                while (selection.Contains(Rand))
                {
                    Rand = Random.Range(0,noptions);
                }
                selection[j] = Rand;
                print(selection[j]);
            }
    }
}
