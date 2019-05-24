namespace CellNinja.Localization
{
    public interface ITranslateResource
    {
        string GetStringValue(string resourceId, string locale);
    }
}
