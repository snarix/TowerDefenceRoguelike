using _Source.MetaGameplay;
using UnityEngine.SceneManagement;

namespace Include
{
    public interface IScenesLoadingService
    {
        void Load(SceneName sceneName);
        void LoadWithData<T>(SceneName sceneName, T data);
    }

    public class SceneLoadingService : IScenesLoadingService
    {
        public void Load(SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }

        public void LoadWithData<T>(SceneName sceneName, T data)
        {
            ServiceLocator.RemoveService<SceneTransitionData<T>>();
            ServiceLocator.AddService(new SceneTransitionData<T>(data));
            Load(sceneName);
        }
    }

    public class SceneTransitionData<T> : ISceneTransitionData
    {
        private T _data;

        public SceneTransitionData(T data)
        {
            _data = data;
        }

        public T Data => _data;
    }
    
    public interface ISceneTransitionData { }
}