using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float rotationSpeed;
    public float checkDistance;
    public Transform firePosition;
    public LineRenderer line;
    public EnemyAI ai;

    bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        ai.isChasing = playerInRange;
        RaycastHit2D hitInfo = Physics2D.Raycast(firePosition.position, transform.right, checkDistance);

        if (hitInfo.collider) {
            line.SetPosition(1, hitInfo.point);
            if (hitInfo.collider.tag == "Player")
                playerInRange = true;
            else
                playerInRange = false;
        } else {
            line.SetPosition(1, transform.position + transform.right * checkDistance);
        }

        if (!playerInRange)
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        else
            ai.target = hitInfo.transform;

        line.SetPosition(0, firePosition.position);
    }
}
