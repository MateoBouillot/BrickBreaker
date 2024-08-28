using BrickBreaker.Scenes;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace BrickBreaker.Services
{
    public interface IAssetsService
    {
        T Get<T>(string name);
    }
    public sealed class AssetsService : IAssetsService
    {
        private Dictionary<string, object> _assets = new Dictionary<string, object>();
        private ContentManager _contentManager;

        public AssetsService(ContentManager contentManager)
        {
            _contentManager = contentManager; 
            ServiceLocator.Register<IAssetsService>(this);
        }

        public void Load<T>(string name)
        {
            if (_assets.ContainsKey(name))
                throw new InvalidOperationException($"Asset {name} already loaded");
            T asset = _contentManager.Load<T>(name);
            _assets[name] = asset;
        }

        public T Get<T>(string name)
        {
            if (!_assets.ContainsKey(name))
                throw new InvalidOperationException($"Asset {name} not loaded");
            return (T) _assets[name];
        }
    }
}
