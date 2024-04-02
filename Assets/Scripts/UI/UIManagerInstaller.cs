using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIManagerInstaller : MonoInstaller<UIManagerInstaller>
{
    [SerializeField] private UIManager _uiManager;

    public override void InstallBindings()
    {
        Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle().NonLazy();
    }
}
