using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float LIFE_TIME;
    float timeAlive;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player" && gameObject.tag == "EnemyBullet"){
            player.BulletHit();
        }
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
