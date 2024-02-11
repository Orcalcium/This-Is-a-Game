using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[DefaultExecutionOrder(1000)]
public class MainMenu : MonoBehaviour
{
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartNew);
    }
    void StartNew()
    {
        SceneManager.LoadScene("TestScene");
        Debug.Log("Click");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
