using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccountMenu : MonoBehaviour {

    //the login settings
    [Header("Login screen")]
    public GameObject loginParent;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text login_ErrorText;

    [Header("Loading Screen")]
    public GameObject loadingParent;

    [Header("Logged in screen")]
    public GameObject loggedInParent;
    public TMP_Text usernameText;

    public void ResetAllUIElements()
    {
        login_ErrorText.text = "";
        usernameInput.text = "";
        passwordInput.text = "";
    }

    public void Login()
    {
        if(usernameInput.text.Length >= 3f)
        {
            if(passwordInput.text.Length >= 5f)
            {
                AccountManager.singleton.RequestLogin(usernameInput.text, passwordInput.text);
            }
            else
            {
                login_ErrorText.text = "Incorrect password";
            }
        }
        else
        {
            login_ErrorText.text = "Incorrect username.";
        }
    }
}
