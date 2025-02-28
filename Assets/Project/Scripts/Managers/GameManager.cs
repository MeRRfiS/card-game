using CardTest.Cards;
using CardTest.Managers.Interfaces;
using CardTest.Web;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace CardTest.Managers
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        [SerializeField] private CardDesk _desk;
        [SerializeField] private UnityEvent OnSpritesLoaded;
        [SerializeField] private UnityEvent<int> OnUpdateScore;

        private int _foundPairs;
        private int _score;
        private ImageLoader _imageLoader;
        private List<Sprite> _cardSprites = new();
        private List<Card> _selectedCards = new();

        private const int MaxCardPair = 3;

        private async void Start()
        {
            _imageLoader = new ImageLoader();
            _cardSprites = await _imageLoader.LoadImage(MaxCardPair);
            OnSpritesLoaded?.Invoke();

            await LoadDesk();
        }

        private async Task LoadDesk()
        {
            _desk.ChangeCardsColliderStatus(false);
            _desk.EnableCards();
            _desk.InitiateCards(MaxCardPair, _cardSprites);

            StartCoroutine(ShowDesk());

            _cardSprites = await _imageLoader.LoadImage(MaxCardPair);
        }

        public void SelectCard(Card card)
        {
            _selectedCards.Add(card);

            if(_selectedCards.Count == 2)
            {
                _desk.ChangeCardsColliderStatus(false);
                StartCoroutine(CompairCard());
            }
        }
        
        public async void CheckFoundPairs()
        {
            if(_foundPairs == MaxCardPair)
            {
                _foundPairs = 0;
                await LoadDesk();
            }
        }

        private void AddScore()
        {
            _score++;
            OnUpdateScore?.Invoke(_score);
            foreach (var card in _selectedCards)
            {
                card.FadeCard();
            }
        }

        private void DeselectCards()
        {
            foreach (var card in _selectedCards)
            {
                card.DeselectCard();
            }
        }

        private IEnumerator CompairCard()
        {
            yield return new WaitForSeconds(1f);

            if (_selectedCards[0].PairIndex == _selectedCards[1].PairIndex)
            {
                _foundPairs++;
                AddScore();
            }
            else
            {
                DeselectCards();
            }

            _desk.ChangeCardsColliderStatus(true);
            _selectedCards.Clear();

            yield return new WaitForSeconds(0.5f);
            CheckFoundPairs();
        }

        private IEnumerator ShowDesk()
        {
            _desk.ShowAllCards();

            yield return new WaitForSeconds(5f);

            _desk.ChangeCardsColliderStatus(true);
            _desk.CloseAllCards();
        }
    }
}