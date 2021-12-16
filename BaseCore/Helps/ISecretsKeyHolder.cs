namespace BaseCore.Helps
{
    public interface ISecretsKeyHolder
    {
        string GetValue(string key);
    }
}
