using CardTest.Managers;
using CardTest.Managers.Interfaces;
using UnityEngine;
using Zenject;

namespace CardTest.Cards
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardAnimation _animation;

        private bool _isCanSelect = true;
        private Sprite _cardSprite;

        public int PairIndex { get; private set; }

        private IGameManager _gameManager;

        [Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void OnMouseDown()
        {
            if (!_isCanSelect) return;

            _isCanSelect = false;
            _gameManager.SelectCard(this);
            _animation.FlipToImage(_cardSprite);
        }

        public void Initiate(int pairIndex, Sprite sprite)
        {
            PairIndex = pairIndex;
            _cardSprite = sprite;
        }

        public void DeselectCard()
        {
            _isCanSelect = true;
            _animation.FlipToShirt();
        }

        public void FadeCard()
        {
            _animation.FadeCard();
        }

        public void ShowCard()
        {
            _animation.FlipToImage(_cardSprite);
        }
    }
}