using System.Collections.Generic;
using UnityEngine;

namespace CardTest.Cards
{
    public class CardDesk : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards;
        [SerializeField] private List<BoxCollider2D> _cardsCollider;

        public void InitiateCards(int pairAmount, List<Sprite> cardsSprite)
        {
            int[] cardPairsIndex = new int[pairAmount * 2];

            SetPairs(pairAmount, ref cardPairsIndex);
            RandomPairs(ref cardPairsIndex);

            for (int i = 0; i < cardPairsIndex.Length; i++)
            {
                _cards[i].Initiate(cardPairsIndex[i], cardsSprite[cardPairsIndex[i]]);
            }
        }

        private static void RandomPairs(ref int[] cardPairsIndex)
        {
            for (int i = cardPairsIndex.Length - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (cardPairsIndex[i], cardPairsIndex[randomIndex]) = (cardPairsIndex[randomIndex], cardPairsIndex[i]);
            }
        }

        private static void SetPairs(int pairAmount, ref int[] cardPairsIndex)
        {
            for (int i = 0; i < pairAmount; i++)
            {
                cardPairsIndex[i * 2] = i;
                cardPairsIndex[i * 2 + 1] = i;
            }
        }

        public void ChangeCardsColliderStatus(bool status)
        {
            foreach (var collider in _cardsCollider)
            {
                collider.enabled = status;
            }
        }

        public void EnableCards()
        {
            foreach (var card in _cards)
            {
                card.gameObject.SetActive(true);
            }
        }

        public void ShowAllCards()
        {
            foreach (var card in _cards)
            {
                card.ShowCard();
            }
        }

        public void CloseAllCards()
        {
            foreach (var card in _cards)
            {
                card.DeselectCard();
            }
        }
    }
}