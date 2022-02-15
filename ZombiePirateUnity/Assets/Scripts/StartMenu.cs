using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{
   
    public GameObject startMenu;
    public GameObject IngameUI;
    public GameObject HPbar;
    public GameObject StoryUI;

    // Start is called before the first frame update
    void Start()
    {
        Pause();
        startMenu.SetActive(true);
        IngameUI.SetActive(false);
        HPbar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void Pause()
    {
        Time.timeScale = 0;
        IngameUI.SetActive(false);
    }

    public void unPause()
    {
        Time.timeScale = 1;
    startMenu.SetActive(false);
        IngameUI.SetActive(true);
        HPbar.SetActive(true);

    }
    public void ShowStartMenu()
    {
        startMenu.SetActive(true);
    }

    public void ShowStory()
    {
        StoryUI.SetActive(true);
    }
    public void BackStory()
    {
        StoryUI.SetActive(false);
    }
}

