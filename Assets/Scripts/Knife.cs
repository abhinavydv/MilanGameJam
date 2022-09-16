using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] float LIFE_TIME;
    float timeAlive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive > LIFE_TIME)
        {
            Destroy(gameObject);
            return;
        }
        timeAlive += Time.deltaTime;
    }
}
