using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

internal class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Food food;
    public FoodPool foodPool = new FoodPool();
    public int poolSize;
    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        foodPool.Initialize(poolSize);
        StartCoroutine("generateFood");
    }

    internal void AddPoints(int points)
    {
        score += points;
        winText.GetComponent<TextMeshProUGUI>().text = score + "";
    }
    public int score;
    public GameObject winText;

    void Update()
    {
        Debug.Log(score);
        if (score >= 100)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        winText.SetActive(true);
    }
    IEnumerator generateFood()
    {
        yield return new WaitForSeconds(0.2f);
        //float randomx, randomy, randomz;
        //randomx = UnityEngine.Random.Range(-10.0f, 10.0f);
        //randomy = UnityEngine.Random.Range(-10.0f, 10.0f);
        //randomz = UnityEngine.Random.Range(0.5f, 10.0f);

        Food food = foodPool.GetFood();
        if (food != null)
        {
            //food.transform.position = new Vector3(randomx, randomz, randomy);
            food.randomize();
        }

        StartCoroutine(generateFood());
    }

    public class FoodPool
    {
        private List<Food> foodPool = new List<Food>();

        public void Initialize(int poolSize)
        {
            for (int i = 0; i < poolSize; i++)
            {
                Food food = Instantiate(GameManager.instance.food);
                foodPool.Add(food);
                food.gameObject.SetActive(false);
            }
        }

        public Food GetFood()
        {
            foreach (Food food in foodPool)
            {
                if (!food.gameObject.activeInHierarchy)
                {
                    food.gameObject.SetActive(true);
                    return food;
                }
            }
            return null;
        }

        public void ReturnFood(Food food)
        {
            food.gameObject.SetActive(false);
        }
    }
}

