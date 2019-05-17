using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(QuitToMenu);
    }

    public void QuitToMenu()
    {
        Debug.Log("Quit");
        SceneManager.LoadScene(0);
    }
}
