using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowderLevelHandler : MonoBehaviour
{

    public int[] position1 = new int[4];
    public int[] position2 = new int[4];
    public int[] position3 = new int[4];

    public GameObject Panel1;

    public Text PowderLVLText;

    private int tmpint;
    private string tmpstring;
    public void Awaken(){

        // pause time, open canvas

        // trigger when new level is reached
        List<int> selection=  randomNrNoDuplicate(3, 7);
    }
    void onPowderLVLUp(){
        Globals.FlourNextLevel *= 2;
        Globals.FlourLevel += 1;

    }

    private List<int> randomNrNoDuplicate(int Lenght, int nOptions){
        int Rand;
        List<int> selection = new List<int>();
        selection = new List<int>(new int[Lenght]);
     
            for (int j = 1; j < Lenght; j++)
            {
                Rand = Random.Range(0,nOptions);
     
                while (selection.Contains(Rand))
                {
                    Rand = Random.Range(0,nOptions);
                }
                selection[j] = Rand;
            }
        return selection;
    }
}
