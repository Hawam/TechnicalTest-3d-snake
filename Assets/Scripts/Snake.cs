using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{

    public Transform bodyPrefab;
    public Vector3 startDirection;
    public float speed = 1f;
    public List<Transform> bodyParts = new List<Transform>();
    private Food previousHighlightedFood; // Track the previously highlighted food object

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
        HighlightNearestFood();
    }

    void Move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;

        if (bodyParts.Count > 1)
        {
            for (int i = bodyParts.Count - 1; i >= 1; i--)
            {
                Vector3 prevPosition = bodyParts[i - 1].position;
                Vector3 newPosition = prevPosition + (bodyParts[i].position - prevPosition).normalized * 0.5f;
                bodyParts[i].position = newPosition;
            }
        }
    }

    private void HighlightNearestFood()
    {
        Food nearestFood = null;
        float nearestDistance = float.MaxValue;

        foreach (Food food in GameManager.instance.foodPool.foodPool)
        {
            if (food.gameObject.activeInHierarchy)
            {
                float distance = Vector3.Distance(transform.position, food.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestFood = food;
                }
            }
        }

        // Remove highlight effect from the previous highlighted food object
        if (previousHighlightedFood != null)
        {
            previousHighlightedFood.RemoveHighlightEffect();
        }

        // Apply highlight effect to the nearest food item
        if (nearestFood != null)
        {
            nearestFood.ApplyHighlightEffect();
            previousHighlightedFood = nearestFood; // Update the previously highlighted food object
        }
    }

    public void Grow(int growthAmount)
    {
        for (int i = 0; i < growthAmount; i++)
        {
            GameObject newPart = Instantiate(bodyPrefab.gameObject);
            bodyParts.Add(newPart.transform);
        }
    }

}