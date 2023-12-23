# TechnicalTest-3d-snake
## Description:
In this task you are provided with a mini-game 3d snake, when you run it, you will notice some issues in the gameplay, scene, and code.

## Task:
Your task is to:
- Highlight the nearest food item from the snake head.
- Fix the snake tail generation.
- Optimize the game.
- Apply best practice techniques for the code and the project.
- Bonus Point: make 2 simple UI screens ( reference image on the second page )

## Solution:
### 1. organizing the project files , adding floders for Scripts, Prefabs and Textures 
(this is considered best practice for unity file structure)

the files structure should be like this:
```
📦Assets
 ┣ 📂Materials
 ┃ ┣ 📜Cartoon_green_texture_grass.mat
 ┃ ┣ 📜New Material 1.mat
 ┃ ┣ 📜New Material.mat
 ┣ 📂Prefabs
 ┃ ┣ 📜Cube (1).prefab
 ┃ ┣ 📜Sphere.prefab
 ┣ 📂Scenes
 ┃ ┣ 📜SampleScene.unity
 ┣ 📂Scripts
 ┃ ┣ 📜Food.cs
 ┃ ┣ 📜GameManager.cs
 ┃ ┣ 📜Snake.cs
 ┣ 📂Textures
 ┃ ┣ 📜42582.jpg
 ┃ ┣ 📜Cartoon_green_texture_grass.eps
 ┃ ┣ 📜Cartoon_green_texture_grass.jpg
```
### 2. adding pooling system
A pooling system is used to efficiently manage the reuse of objects in order to optimize performance and memory usage. In the context of game development, a pooling system is commonly used to manage objects that are frequently instantiated and destroyed, such as projectiles, enemies, or in this case, food items.(this is considered as Optimizing the game)

this class used for managing the pooling system
```csharp
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
```

### 3. Highlight the nearest food item from the snake head
in the Food Class, i added this code to Apply Highlight Effect and removing it .

```csharp
private bool isHighlighted = false;
private Color originalMaterialColor;

private void Awake()
{
    originalMaterialColor = GetComponent<Renderer>().material.color;
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
```
and in the Snake Class, I added the code to find the nearest food object
```csharp
public class Snake : MonoBehaviour
{
    // ...

    private Food previousHighlightedFood; // Track the previously highlighted food object

    private void Update()
    {
        // ...

        HighlightNearestFood();
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

    // ...
}
```