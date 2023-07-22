using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private Transform portal;
    public float damage = 10f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, portal.position, moveSpeed * Time.deltaTime);
    }
}
