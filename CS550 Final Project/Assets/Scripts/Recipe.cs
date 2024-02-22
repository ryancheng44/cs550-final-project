using System;
using System.Collections.Generic;

[Serializable]
public class Recipe
{
    public string name;
    public string websiteURL;
    public Ingredient[] ingredients;
}

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
    DriedRosemary
}