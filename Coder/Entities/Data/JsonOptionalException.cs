namespace DStutz.Coder.Entities.Data;

public class JsonOptionalException : Exception
{
    public JsonOptionalException(
        string jsonList,
        string incorrectType)
        : base($"To set '?' in items of json list '{jsonList}' " +
               $"use property 'Type' instead of '{incorrectType}'")
    { }

    public JsonOptionalException(
        string jsonList,
        string incorrectType1,
        string incorrectType2)
        : this(jsonList,
               $"{incorrectType1}' or '{incorrectType2}")
    { }
}
