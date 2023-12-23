using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Food object
public class Food : MonoBehaviour
{

    public int points = 10;
    public int lifetime = 30;
    float time;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Snake")
        {
            GameManager.instance.AddPoints(points);
            GameManager.instance.foodPool.ReturnFood(this);
            Grow();
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

    public int growthAmount = 1;
    public void randomize()
    {
        time = 0;
        float randomx, randomy, randomz;
        randomx = UnityEngine.Random.Range(-10.0f, 10.0f);
        randomy = UnityEngine.Random.Range(-10.0f, 10.0f);
        randomz = UnityEngine.Random.Range(0.5f, 10.0f);
        transform.position = new Vector3(randomx, randomz, randomy);
    }
    void Grow()
    {
        for (int i = 0; i < growthAmount; i++)
        {
            GameObject newPart = Instantiate(FindObjectOfType<Snake>().bodyPrefab.gameObject);
            newPart.transform.position = FindObjectOfType<Snake>().bodyParts[FindObjectOfType<Snake>().bodyParts.Count - 1].position + new Vector3(0, 0, 0.7f);
            FindObjectOfType<Snake>().bodyParts.Add(newPart.transform);
        }

    }
}