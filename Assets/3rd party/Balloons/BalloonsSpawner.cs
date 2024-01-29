using System.Collections;
using UnityEngine;

namespace Numbers.MiniGames.BalloonsTransition
{
    public class BalloonsSpawner : MonoBehaviour
    {
        public System.Action BalloonClaped;
        public System.Action SpawningBalloonsStopped;

        [Header("References")]
        [SerializeField] private Balloon _balloonTemplate;
        
        [Space]
        [SerializeField] private RectTransform _spawnAreaRect;

        [Header("Parameters")]
        [SerializeField] private int _maxBalloonsNumber;

        [Space]
        [SerializeField] private Range _balloonScaleRange;
        [SerializeField] private Range _balloonMovingTimeRange;
        
        [Space]
        [SerializeField] private Sprite[] _balloonsSprites;

        private Range _spawnArea;
        private float _balloonsMaxPositionY;

        private void Start()
        {
            _spawnAreaRect = transform as RectTransform;

            var balloonTransform = _balloonTemplate.transform as RectTransform;

            _spawnArea = new Range() 
            { 
                Min = -_spawnAreaRect.rect.width * 0.5f + balloonTransform.sizeDelta.x * _balloonScaleRange.Max * 0.5f, 
                Max = _spawnAreaRect.rect.width * 0.5f - balloonTransform.sizeDelta.x * _balloonScaleRange.Max * 0.5f
            };

            _balloonsMaxPositionY = _spawnAreaRect.rect.yMax + balloonTransform.sizeDelta.y * _balloonScaleRange.Max;

            StartSpawningBalloons();
        }

        public void ClearAllBalloons()
        {
            foreach (var balloon in GetComponentsInChildren<Balloon>())
                Destroy(balloon.gameObject);
        }

        public void StopSpawningBalloons() => SpawningBalloonsStopped?.Invoke();

        public void StartSpawningBalloons() => StartCoroutine(SpawningBalloonsDelayed());

        private IEnumerator SpawningBalloonsDelayed()
        {
            var balloonsCounter = 0;

            while (balloonsCounter < _maxBalloonsNumber)
            {
                SpawnBalloon();
                balloonsCounter++;

                yield return new WaitForSeconds(0.1f);
            }
        }

        private void SpawnBalloon(Balloon balloon = null)
        {
            Balloon newBalloon = balloon;
                
            if (newBalloon == null)
            {
                newBalloon = Instantiate(_balloonTemplate, transform);
                newBalloon.Respawning += OnBalloonRespawning;
                SpawningBalloonsStopped += newBalloon.DontRespawn;
            }

            newBalloon.transform.localPosition = GetRandomSpawnPosition((newBalloon.transform as RectTransform).sizeDelta.y * _balloonScaleRange.Max * 0.5f);
            newBalloon.transform.localScale = Vector2.one * Random.Range(_balloonScaleRange.Min, _balloonScaleRange.Max);

            var randomMovingTime = Random.Range(_balloonMovingTimeRange.Min, _balloonMovingTimeRange.Max);
            var randomBalloonSprite = _balloonsSprites[Random.Range(0, _balloonsSprites.Length - 1)];

            newBalloon.Set(_balloonsMaxPositionY, randomMovingTime, randomBalloonSprite);
        }

        //private void SpawnClapParticles(Vector2 spawnPosition) => SpineParticles.SpawnParticles(_clapParticles, transform, spawnPosition);

        private void OnBalloonRespawning(Balloon balloon, bool reachedOutOfScreen)
        {
            if (!reachedOutOfScreen)
            {
                //SpawnClapParticles(balloon.transform.position);
                BalloonClaped?.Invoke();
            }

            SpawnBalloon(balloon);
        }

        private Vector2 GetRandomSpawnPosition(float yPositionOffset) => new Vector2() 
        { 
            x = Random.Range(_spawnArea.Min, _spawnArea.Max),
            y = _spawnAreaRect.rect.yMin - yPositionOffset
        };

        [System.Serializable]
        private struct Range
        {
            public float Min;
            public float Max;
        }
    }
}