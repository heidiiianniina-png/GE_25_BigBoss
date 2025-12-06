using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuParent;
    public KeyCode toggleKey = KeyCode.P;
    //public bool hasBeenPaused = false;
    
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleTheTarget();  
        }
    }

	void ToggleTheTarget()
	{
        if (!menuParent.activeSelf) //(!hasBeenPaused)
        {
            //hasBeenPaused = true;
            Time.timeScale = 0.0f;
			//menuParent.SetActive(true);
		}
        else
        {
            //hasBeenPaused = false;
            Time.timeScale = 1.0f;
			//menuParent.SetActive(false);
		}
        
		menuParent.SetActive(!menuParent.activeSelf);
	}
}
