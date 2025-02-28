using TMPro;
using UnityEngine;

namespace CardTest.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private string _scoreFormat;

        private void Awake()
        {
            _scoreFormat = _scoreText.text;
            _scoreText.text = string.Format(_scoreFormat, 0);
        }

        public void UpdateScore(int score)
        {
            _scoreText.text = string.Format(_scoreFormat, score);
        }
    }
}