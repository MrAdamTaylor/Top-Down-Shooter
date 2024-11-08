using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

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
            Debug.Log("Disposable Count"+_disposables.Count);
            while (_disposables.Count > 0)
            {
                IDisposable disposable = _disposables.Pop();
                Debug.Log($"<color=cyan>Destroing object is {disposable}</color>");
                disposable.Dispose();
            }
        }
}
