using DatabaseControl;
using System.Collections;
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
        LoginUser(userName, userPassword);
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



    #endregion
}
