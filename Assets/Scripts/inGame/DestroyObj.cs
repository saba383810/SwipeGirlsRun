using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    [SerializeField] private float destroyTime= default;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Destroy),destroyTime);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
    
}
