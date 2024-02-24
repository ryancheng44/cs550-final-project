using System;

// The Recipe class is marked as Serializable, 
// which allows instances of the class to be edited in the Unity Inspector
[Serializable]
public class Recipe
{
    // The name of the recipe
    public string name;

    // The URL of the website where the recipe is found
    public string websiteURL;

    // An array of ingredients required for the recipe
    public Ingredient[] ingredients;
}

// An enumeration of possible ingredients
public enum Ingredient
{
    Butter,
    Carrots,
    Onions,
    SweetRedPeppers,
    PortobelloMushrooms,
    CannedTuna,
    Spinach,
    FrozenPeas,
    ShortPasta,
    AllPurposeFlour,
    ChickenBroth,
    HalfAndHalfCream,
    ParmesanCheese,
    Salt,
    Pepper,
    OliveOil,
    RedWineVinegar,
    Sugar,
    Garlic,
    Cumin,
    ChiliPowder,
    Rice,
    CannedKidneyBeans,
    CannedBlackBeans,
    FrozenCorn,
    GreenOnions,
    Cilantro,
    GroundTurkey,
    Celery,
    GreenPeppers,
    CayennePeppers,
    CannedTomatoes,
    PastaSauce,
    HotChiliBeans,
    CannedPintoBeans,
    CanolaOil,
    BonelessPorkChops,
    SweetOnions,
    Apples,
    BrownSugar,
    CiderVinegar,
    GarlicPowder,
    DriedRosemary,
    Zucchinis,
    Salsa,
    CornTortillas,
    CheddarCheese,
    Eggs,
    ItalianSeasoning,
    RedPepperFlakes,
    TilapiaFillets,
    GroundBeef,
    Cabbage,
    CannedRedBeans,
    TomatoSauce,
    Brocolli,
    PicanteSauce,
    CottageCheese,
    FlourTortillas,
    FrozenSpinach,
    BalsamicVinegar,
    FreshBasil,
    PlumTomatoes,
    BonelessChickenBreasts,
    CannedOlives,
    RedOnions,
    DriedBasil,
    DriedOregano,
    MozerellaCheese,
    FetaCheese,
    // Add more ingredients as needed
}
