using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LevelChange : MonoBehaviour
{
    
 void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.gameObject.tag == "Exit_Level_1")
        {


            SceneManager.LoadScene("Level_3");
        }

         if(other.gameObject.tag == "Exit_Level_3")
        {



          
            SceneManager.LoadScene("PlayAgain");
        }
    }

    public void PlayAgain()
    {


        SceneManager.LoadScene("Level_1");



    }


}
