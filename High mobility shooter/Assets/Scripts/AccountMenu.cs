using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccountMenu : MonoBehaviour {

    //the login settings
    [Header("Login screen")]
    public GameObject loginParent;
    public TMP_InputField Login_usernameInput;
    public TMP_InputField login_passwordInput;
    public TMP_Text login_ErrorText;

    //register fields
    [Header("Login screen")]
    public GameObject registerParent;
    public TMP_InputField register_usernameInput;
    public TMP_InputField register_passwordInput;
    public TMP_InputField register_confirmPassword;
    public TMP_Text register_ErrorText;

    [Header("Loading Screen")]
    public GameObject loadingParent;

    [Header("Logged in screen")]
    public GameObject loggedInParent;
    public TMP_Text usernameText;

    public void ResetAllUIElements()
    {
        login_ErrorText.text = "";
        Login_usernameInput.text = "";
        login_passwordInput.text = "";
    }

    public void Login()
    {
        AccountManager.singleton.RequestLogin(Login_usernameInput.text, login_passwordInput.text);
    }

    public void Register()
    {
        AccountManager.singleton.RequestRegister(register_usernameInput.text, register_passwordInput.text, register_confirmPassword.text);
    }

    public void LogOut()
    {
        AccountManager.singleton.LogOut();
    }

    public void OpenRegister()
    {
        registerParent.SetActive(true);
        loginParent.SetActive(false);
    }

    public void Back()
    {
        registerParent.SetActive(false);
        loginParent.SetActive(true);
    }
}
