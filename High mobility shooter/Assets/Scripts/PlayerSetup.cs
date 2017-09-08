using UnityEngine;

public class PlayerSetup : MonoBehaviour {

    public GameObject GUI;
    private GameObject uiIns;

    [HideInInspector]
    public PlayerUI ui;

    private void Awake()
    {
        uiIns = Instantiate(GUI);
        ui = uiIns.GetComponent<PlayerUI>();
    }
}
