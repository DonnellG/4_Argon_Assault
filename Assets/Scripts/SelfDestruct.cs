using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float deleteTimer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
