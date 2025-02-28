using CardTest.Cards;

namespace CardTest.Managers.Interfaces
{
    public interface IGameManager
    {
        public void SelectCard(Card card);
        public void CheckFoundPairs();
    }
}