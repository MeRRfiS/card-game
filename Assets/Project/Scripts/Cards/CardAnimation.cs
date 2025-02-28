using CardTest.Managers.Interfaces;
using CardTest.Models;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardTest.Cards
{
    public class CardAnimation : MonoBehaviour
    {
        [SerializeField] private Sprite _shirt;

        private bool _isCanAnimate = true;
        private Image _image;

        private const float FlipDuration = 0.2f;
        private const float FadeDuration = 0.2f;

        private IGameManager _gameManager;

        [Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        public void FlipToImage(Sprite cardImage)
        {
            FlipCard(cardImage);
        }

        public void FlipToShirt()
        {
            FlipCard(_shirt);
        }

        public void FadeCard()
        {
            if (!_isCanAnimate) return;

            _isCanAnimate = false;

            _image.DOFade(0, FadeDuration).OnComplete(() =>
            {
                _isCanAnimate = true;
                gameObject.SetActive(false);
                _image.color = Color.white;
                _image.sprite = _shirt;
            });
        }

        private void FlipCard(Sprite cardSprite)
        {
            if (!_isCanAnimate) return;

            _isCanAnimate = false;

            transform.DOScaleX(0, FlipDuration).OnComplete(() =>
            {
                _image.sprite = cardSprite;

                transform.DOScaleX(1, FlipDuration).OnComplete(() => _isCanAnimate = true);
            });
        }
    }
}