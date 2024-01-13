using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField NameField;
    public InputField passwordField;

    public Button submitbutton;
    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }
    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", NameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            DBMenager.username = NameField.text;
            DBMenager.score = int.Parse(www.text.Split('\t')[1]);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("user logIn failed. Error#" + www.text);
        }
    }
    public void Verifyinput()
    {
        submitbutton.interactable = (NameField.text.Length >= 0 && passwordField.text.Length >= 6);
    }
}
