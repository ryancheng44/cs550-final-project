using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private GameObject ingredientPrefab;
    [SerializeField] private GameObject recipePrefab;

    [SerializeField] private TextMeshProUGUI availableRecipesText;

    [SerializeField] private RectTransform selectIngredientsContent;
    [SerializeField] private RectTransform availableRecipesContent;
    [SerializeField] private RectTransform insructionsContent;

    [SerializeField] private GameObject[] instructionsUI;
    [SerializeField] private GameObject[] selectIngredientsUI;
    [SerializeField] private GameObject[] availableRecipesUI;

    private float maxPreferredWidth;
    private bool isSelectIngredients = false;
    private bool isSwapped = false;

    public void StartProgram()
    {
        foreach (GameObject gameObject in instructionsUI)
            gameObject.SetActive(false);

        SwapUI();

        foreach (Ingredient ingredient in Enum.GetValues(typeof(Ingredient)))
        {
            TextMeshProUGUI instance = Instantiate(ingredientPrefab, selectIngredientsContent).GetComponentInChildren<TextMeshProUGUI>();
            
            string ingredientString = ingredient.ToString();

            for (int i = 1; i < ingredientString.Length; i++)
            {
                if (char.IsUpper(ingredientString[i]))
                {
                    ingredientString = ingredientString.Insert(i, " ");
                    i++;
                }
            }

            instance.text = ingredientString;

            if (instance.preferredWidth > maxPreferredWidth)
                maxPreferredWidth = instance.preferredWidth;

            instance.GetComponentInParent<IngredientButton>().ingredient = ingredient;
        }

        GridLayoutGroup gridLayoutGroup = selectIngredientsContent.GetComponent<GridLayoutGroup>();

        Vector2 cellSize = gridLayoutGroup.cellSize;
        cellSize.x = maxPreferredWidth + 100f;
        gridLayoutGroup.cellSize = cellSize;
    }

    public void Done()
    {
        SwapUI();
        SetRecipes();
    }

    public void Switch()
    {
        ClearRecipes();
        isSwapped = !isSwapped;
        SetRecipes();
    }

    public void Edit()
    {
        ClearRecipes();
        SwapUI();
    }

    private void SetRecipes()
    {
        availableRecipesText.text = isSwapped ? "Ingredient Spotlight" : "Complete Match";

        RecipeManager.instance.DetermineAvailableRecipes(isSwapped);

        foreach (Recipe recipe in RecipeManager.instance.availableRecipes)
        {
            TextMeshProUGUI instance = Instantiate(recipePrefab, availableRecipesContent).GetComponentInChildren<TextMeshProUGUI>();
            instance.text = recipe.name;
            instance.GetComponentInParent<RecipeButton>().recipe = recipe;
        }
    }

    private void ClearRecipes()
    {
        foreach (Transform child in availableRecipesContent)
            Destroy(child.gameObject);
    }

    private void SwapUI()
    {
        isSelectIngredients = !isSelectIngredients;

        foreach (GameObject gameObject in selectIngredientsUI)
            gameObject.SetActive(isSelectIngredients);
        
        foreach (GameObject gameObject in availableRecipesUI)
            gameObject.SetActive(!isSelectIngredients);

        scrollRect.content = isSelectIngredients ? selectIngredientsContent : availableRecipesContent;
    }
}
