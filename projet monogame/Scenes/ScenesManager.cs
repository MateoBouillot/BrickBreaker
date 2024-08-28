using System;
using System.Collections.Generic;

namespace BrickBreaker.Scenes
{
    public interface IScenesManager
    {
        void Load<T>(params object[] data) where T : Scene, new();
        Scene GetCurrentScene();
    }
    public sealed class ScenesManager : IScenesManager
    {
        private Dictionary<Type, Scene> _scenes = new Dictionary<Type, Scene>();
        private Scene _currentScene;

        public ScenesManager()
        {
            ServiceLocator.Register<IScenesManager>(this);
        }

        public void Register<T>() where T : Scene, new()
        {
            var type = typeof(T);
            if (_scenes.ContainsKey(type))
                throw new InvalidOperationException($"Scene of type {typeof(T)} already registered");
            _scenes[typeof(T)] = new T();
        }

        public void Load<T>(params object[] datas) where T : Scene, new()
        {
            var type = typeof(T);
            if(_currentScene != null)
                _currentScene.unload();
            _currentScene = new T();
            _currentScene.Load(datas);
        }

        public Scene GetCurrentScene()
        {
            return _currentScene; 
        }

        public void Update(float dt)
        {
            _currentScene.Update(dt);
        }

        public void Draw()
        {
            _currentScene.Draw();
        }
    }
}
