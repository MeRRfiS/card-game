using CardTest.Managers;
using CardTest.Managers.Interfaces;
using UnityEngine;
using Zenject;

namespace CardTest.Installers
{
    public class Bootstrap : MonoInstaller
    {
        [SerializeField] private GameManager _gameManager;

        public override void InstallBindings()
        {
            BindGameManager();
        }

        private void BindGameManager()
        {
            Container.Bind<IGameManager>()
                     .FromInstance(_gameManager)
                     .AsSingle();
        }
    }
}