using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIGameController : MonoBehaviour
{
    public Text EngineForceView;

	// Use this for initialization
    public static UIGameController runtime;

    private void Awake()
    {
        runtime = this;
    }

    void Start ()
	{
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void ShowInfoPanel(bool isShow)
    {
        EngineForceView.gameObject.SetActive(!isShow);
    }

    public void ShowInfo()
    {
       
    }
    public void HideInfo()
    {
        
    }

    public void RestartGame()
    {
       
    }
}
