using UnityEngine;
using UnityEngine.SceneManagment;

public class ButtonFunctions : MonoBehaviour
{
    public void PlayGame (){
        SceneManager.LoadScene(SceneManager.GetActiveScene().builtIndex +1);
    }
}
