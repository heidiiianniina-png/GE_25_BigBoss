using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref2 : MonoBehaviour
{
    public int sceneBuildIndex;

    // Trigger zone for 3D physics
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log(2);
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
}
