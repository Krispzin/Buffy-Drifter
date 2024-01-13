using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginMenuTest : MonoBehaviour
{
    public Button registerbtn;
    public Button loginbtn;
    public Button playbtn;
    public Text playerDisplay;
    private void Start()
    {
        if (DBMenager.Login)
        {
            playerDisplay.text = "Player: " + DBMenager.username;
        }
        registerbtn.interactable = !DBMenager.Login;
        loginbtn.interactable = !DBMenager.Login;
        playbtn.interactable = DBMenager.Login;
    }
    public void GoToRegister()
    {
        SceneManager.LoadScene(2);
    }
    public void GoToLogin()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToGame()
    {
        SceneManager.LoadScene(3);
    }
}
