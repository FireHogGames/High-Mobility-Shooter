using DatabaseControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviour {

    private string username;
    private string password;
    private AccountMenu menu;

    public static AccountManager singleton;

    #region singleton patern

    private void Start()
    {
        singleton = this;
        DontDestroyOnLoad(this);
        menu = FindObjectOfType<AccountMenu>();
    }

    #endregion

    #region login
    public void RequestLogin(string userName, string userPassword)
    {
        if (userName.Length >= 3f)
        {
            if (userPassword.Length >= 5f)
            {
                //test
                menu.loadingParent.SetActive(true);
                menu.loginParent.SetActive(false);
                StartCoroutine(LoginUser(userName, userPassword));
                
            }
            else
            {
                menu.login_ErrorText.text = "Incorrect password";
            }
        }
        else
        {
            menu.login_ErrorText.text = "Incorrect username.";
        }
    }

    //Called by Button Pressed Methods. These use DatabaseControl namespace to communicate with server.
    private IEnumerator LoginUser(string playerUsername, string playerPassword)
    {
        

        IEnumerator e = DCF.Login(playerUsername, playerPassword); // << Send request to login, providing username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            if(menu != null)
            {
                //Username and Password were correct. Stop showing 'Loading...' and show the LoggedIn UI. And set the text to display the username.
                menu.ResetAllUIElements();
                username = playerUsername;
                password = playerPassword;
                menu.loadingParent.gameObject.SetActive(false);
                menu.loggedInParent.gameObject.SetActive(true);
                menu.usernameText.text = "Logged In As: " + username;
            }
        }
        else
        {
            //Something went wrong logging in. Stop showing 'Loading...' and go back to LoginUI
            menu.loadingParent.gameObject.SetActive(false);
            menu.loginParent.gameObject.SetActive(true);
            if (response == "UserError")
            {
                //The Username was wrong so display relevent error message
                menu.login_ErrorText.text = "Error: Username not Found";
            }
            else
            {
                if (response == "PassError")
                {
                    //The Password was wrong so display relevent error message
                    menu.login_ErrorText.text = "Error: Password Incorrect";
                }
                else
                {
                    //There was another error. This error message should never appear, but is here just in case.
                    menu.login_ErrorText.text = "Error: Unknown Error. Please try again later.";
                }
            }
        }
    }
    #endregion

    #region register

    public void RequestRegister(string userName, string userPassword, string confirm)
    {
        if(userName.Length >= 3f)
        {
            if(userPassword.Length >= 5f)
            {
                if(confirm == userPassword)
                {
                    menu.loadingParent.SetActive(true);
                    menu.registerParent.SetActive(false);
                    StartCoroutine(RegisterUser(userName, userPassword));
                }
                else
                {
                    menu.register_ErrorText.text = "Passwords don't match";
                }
            }
            else
            {
                menu.register_ErrorText.text = "Password to short";
            }
        }
        else
        {
            menu.register_ErrorText.text = "Username to short!!!";
        }
    }

    IEnumerator RegisterUser(string playerUsername, string playerPassword)
    {
        IEnumerator e = DCF.RegisterUser(playerUsername, playerPassword, "Kills[0]/Deaths[0]/Level[0]"); // << Send request to register a new user, providing submitted username and password. It also provides an initial value for the data string on the account, which is "Hello World".
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            //Username and Password were valid. Account has been created. Stop showing 'Loading...' and show the loggedIn UI and set text to display the username.
            menu.ResetAllUIElements();
            menu.loadingParent.gameObject.SetActive(false);
            menu.loggedInParent.gameObject.SetActive(true);
            username = playerUsername;
            password = playerPassword;
            menu.usernameText.text = "Logged In As: " + playerUsername;
        }
        else
        {
            //Something went wrong logging in. Stop showing 'Loading...' and go back to RegisterUI
            menu.loadingParent.gameObject.SetActive(false);
            menu.registerParent.gameObject.SetActive(true);
            if (response == "UserError")
            {
                //The username has already been taken. Player needs to choose another. Shows error message.
                menu.register_ErrorText.text = "Error: Username Already Taken";
            }
            else
            {
                //There was another error. This error message should never appear, but is here just in case.
                menu.register_ErrorText.text = "Error: Unknown Error. Please try again later.";
            }
        }
    }

    #endregion

    #region log out
    public void LogOut()
    {
        menu.ResetAllUIElements();
        username = "";
        password = "";
        menu.loginParent.gameObject.SetActive(true);
        menu.loggedInParent.gameObject.SetActive(false);
    }
    #endregion
}
