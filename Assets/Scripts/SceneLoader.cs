using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float levelLoadDely = 2f;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Invoke("LoadFirstLevel", levelLoadDely);
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}
