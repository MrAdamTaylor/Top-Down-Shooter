using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.ServiceLocator
{
    public class DispoceList 
    {
        [CanBeNull] private static DispoceList _instance;

        private readonly Stack<IDisposable> _disposables = new();

        public static DispoceList Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DispoceList();
                }
                return _instance;
            }
        }

        public void Add(IDisposable disposable)
        {
            _disposables.Push(disposable);
        }

        public void RemoveAll()
        {
            while (_disposables.Count > 0)
            {
                IDisposable disposable = _disposables.Pop();
                disposable.Dispose();
            }
        }
    }
}
