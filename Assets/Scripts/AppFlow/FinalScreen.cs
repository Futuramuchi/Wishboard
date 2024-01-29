using DG.Tweening;
using Numbers.MiniGames.BalloonsTransition;
using UnityEngine;
using UnityEngine.UI;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] private Transform result;
    [SerializeField] private Transform resultTarget;
    [Space]
    [SerializeField] private Image background;
    [SerializeField] private RectTransform title;
    [Space]
    [SerializeField] private BalloonsSpawner balloonsSpawner;

    public void Start()
    {
        result.parent = resultTarget.transform;

        result.DOMove(resultTarget.transform.position, 0.35f);

        background.DOFade(1, 0.25f).From(0);

        title
            .DOMove(title.transform.position, 0.45f)
            .From(title.transform.position + Vector3.up * 300)
            .SetDelay(0.1f)
            .SetEase(Ease.OutBack);

        DOVirtual.DelayedCall(8, () => balloonsSpawner.StopSpawningBalloons());
    }
}
