using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChainCube.Scripts.Cube
{
    [RequireComponent(typeof(PointsContainerCollisionDetector), typeof(PointsContainer))]
    public class CollisionMergePointsContainer : MonoBehaviour
    {
        private PointsContainer _pointsContainer;
        private PointsContainerCollisionDetector _detector;
        [SerializeField] private TextMeshProUGUI _score;
        private void Start()
        {
            _pointsContainer = GetComponent<PointsContainer>();
            _detector = GetComponent<PointsContainerCollisionDetector>();
            _score = FindObjectOfType<TextMeshProUGUI>();
            Subscribe();
        }

        private void OnPointsContainerCollision(PointsContainer col)
        {
            if (col.points == _pointsContainer.points)
            {
                UpdateScore();
                _pointsContainer.points *= 2;
                Destroy(col.gameObject);
            }
           
        }

        private void UpdateScore()
        {
            long scoreSave = _pointsContainer.points / 2;
            if (scoreSave == 1024)
                SceneManager.LoadScene(1);
            _score.text = "Score: " + (scoreSave);
        }

        private void Subscribe()
        {
            _detector.onCollisionContinue += OnPointsContainerCollision;
        }
        
        private void Unsubscribe()
        {
            _detector.onCollisionContinue -= OnPointsContainerCollision;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
