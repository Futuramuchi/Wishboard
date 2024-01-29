using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Numbers.MiniGames.BalloonsTransition
{ 
    [RequireComponent(typeof(Image))]
    public class Balloon : MonoBehaviour, IPointerClickHandler
    {
        public Action<Balloon, bool> Respawning;

        private Image _imageComponent;

        private Tween _movingUpSequence;
        private Tween _movingLeftRighSequence;

        private bool _respawning = true;

        private void OnDestroy()
        {
            _movingUpSequence.Kill();
            _movingLeftRighSequence.Kill();
        }

        private void Burst()
        {
            _imageComponent.raycastTarget = false;

            transform
                .DOScale(transform.localScale * 1.1f, 0.1f)
                .SetEase(Ease.InSine)
                .onComplete += () => Respawn(false);
        }

        private void Respawn(bool reachedOutOfScreen)
        {
            _imageComponent.raycastTarget = true;

            _movingUpSequence.Kill();
            _movingLeftRighSequence.Kill();

            Respawning?.Invoke(this, reachedOutOfScreen);

            if (!_respawning)
                Destroy(gameObject);
        }

        public void DontRespawn() => _respawning = false;

        public void Set(float maxPositionY, float movingTime, Sprite sprite)
        {
            _imageComponent = GetComponent<Image>();

            _imageComponent.sprite = sprite;

            _movingUpSequence = DOTween.Sequence()
                .Append((transform as RectTransform).DOAnchorPosY(maxPositionY, movingTime).SetEase(Ease.InSine))
                .AppendCallback(() => Respawn(true));

            var xMovingSideMultipilier = transform.position.x < 0 ? 1 : -1;
            var xMovingForce = UnityEngine.Random.Range(1f, 2f);

            _movingLeftRighSequence = transform.DOMoveX(transform.position.x + 0.1f * xMovingSideMultipilier * xMovingForce, movingTime * 0.2f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        public void OnPointerClick(PointerEventData eventData) => Burst();
    }
}