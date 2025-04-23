using UnityEngine;

namespace Include
{
    public interface ISaveLoadService
    {
        void Save(string dataID, object data);
        T Load<T>(string dataID);
    }

    public class SaveLoadService : ISaveLoadService
    {
        public void Save(string dataID, object data)
        {
            string json = JsonUtility.ToJson(data, true);
            PlayerPrefs.SetString(dataID, json);
            PlayerPrefs.Save();
        }

        public T Load<T>(string dataID)
        {
            if (PlayerPrefs.HasKey(dataID))
            {
                string json = PlayerPrefs.GetString(dataID);
                return JsonUtility.FromJson<T>(json);
            }
            else
                return default(T);
        }
    }
}