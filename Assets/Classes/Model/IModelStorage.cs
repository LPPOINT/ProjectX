namespace Assets.Classes.Model
{
    public interface IModelStorage
    {
        void SetFloat(string key, float value);
        float GetFloat(string key);

        void SetBool(string key, bool value);
        bool GetBool(string key);

        void SetString(string key, string value);
        string GetString(string key);

        void SetInt(string key, int value);
        int GetInt(string key);
    }
}
