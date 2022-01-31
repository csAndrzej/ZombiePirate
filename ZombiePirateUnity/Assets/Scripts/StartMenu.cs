using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{
    public GameObject startMenu;

    // Start is called before the first frame update
    void Start()
    {
        Pause();
        startMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Pause()
    {
        Time.timeScale = 0;
        
    }

    public void unPause()
    {
        Time.timeScale = 1;
    startMenu.SetActive(false);

}
    public void ShowStartMenu()
    {
        startMenu.SetActive(true);
    }
}

