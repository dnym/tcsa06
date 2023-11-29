namespace DrinksInfo.DataAccess.Models;

public record Category(string Name);
public record ListDrink(int Id, string Name);
public record Ingredient(string Name, int? Id = null, string? Description = null,
    string? Type = null, bool? Alcohol = null, float? AlcoholByVolume = null);
public record Glass(string Name);
public record AlcoholType(string Name);
public record Tag(string Name);
public record Drink(int Id, string Name, string? AlternateName,
    Tag[] Tags, Category? Category, string? IBACategory,
    AlcoholType? AlcoholType, Glass? Glass,
    Ingredient[] Ingredients, string[] Measures,
    string? Instructions);
