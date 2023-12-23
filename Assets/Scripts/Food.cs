using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Food object
public class Food : MonoBehaviour
{

    public int points = 10;
    public int lifetime = 30;
    public int growthAmount = 1;
    float time;
    private bool isHighlighted = false;
    private Color originalMaterialColor;

    private void Awake()
    {
        originalMaterialColor = GetComponent<Renderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Snake")
        {
            GameManager.instance.AddPoints(points);
            GameManager.instance.foodPool.ReturnFood(this);
            GameManager.instance.Snake.Grow(growthAmount);
        }
    }
    public void Update()
    {
        time = time + Time.deltaTime;
        if (time > lifetime)
        {
            GameManager.instance.foodPool.ReturnFood(this);
        }
    }

    public void randomize()
    {
        time = 0;
        float randomx, randomy, randomz;
        randomx = UnityEngine.Random.Range(-10.0f, 10.0f);
        randomy = UnityEngine.Random.Range(-10.0f, 10.0f);
        //randomz = UnityEngine.Random.Range(0.5f, 10.0f);
        transform.position = new Vector3(randomx, .5f, randomy);
    }
    public void ApplyHighlightEffect()
    {
        if (!isHighlighted)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = Color.red;
            isHighlighted = true;
        }
    }

    public void RemoveHighlightEffect()
    {
        if (isHighlighted)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = originalMaterialColor;
            isHighlighted = false;
        }
    }
}