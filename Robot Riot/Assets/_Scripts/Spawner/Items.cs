using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField]
    protected ItemObject Item;
    [SerializeField]
    private float rotateSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed *Time.deltaTime);

        //When sudden death starts get rid of items
        if (MatchTimer.suddenDeath)
        {
            Destroy(gameObject);
        }
    }
}
