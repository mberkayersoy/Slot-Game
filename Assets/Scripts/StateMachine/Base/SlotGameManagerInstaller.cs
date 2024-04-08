using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SlotGameManagerInstaller : MonoInstaller<SlotGameManagerInstaller>
{
    [SerializeField] private SlotGameManager _slotGameManager;

    public override void InstallBindings()
    {
        Container.Bind<SlotGameManager>().FromInstance(_slotGameManager).AsSingle().NonLazy();
    }
}
