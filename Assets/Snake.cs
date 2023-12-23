using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{

    public Transform bodyPrefab;
    public Vector3 startDirection;
    public float speed = 1f;
    public List<Transform> bodyParts = new List<Transform>();

    void Start()
    {
        bodyParts.Add(transform);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float diagonal= Input.GetAxis("Depth");
        Debug.Log(diagonal);
        Vector3 direction = new Vector3(horizontal, diagonal, vertical).normalized;
        Move(direction);
    }

    void Move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime; ;
        for (int i = 1; i < bodyParts.Count; i++)
        {
            Vector3 prevPosition = bodyParts[i - 1].position;
            bodyParts[i].position = prevPosition;
        }
    }
}