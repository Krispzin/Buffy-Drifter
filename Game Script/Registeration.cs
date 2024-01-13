using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registeration : MonoBehaviour
{
    
    public InputField NameField;
    public InputField passwordField;

    public Button submitbutton;
    public void CallRegister()
    {
        StartCoroutine(register());
    }

    IEnumerator register()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", NameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;
        if(www.text == "0")
        {
            Debug.Log("User created successful.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("User creation failed. Error #"+ www.text);
        }
    }
    public void Verifyinput()
    {
        submitbutton.interactable = (NameField.text.Length >= 0 && passwordField.text.Length >= 6);
    }
}
