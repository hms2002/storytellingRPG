using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public List<Vector2> comboPosition;
    private int repeatNum;
    private EffectType type;
    private Actor _target;

    public enum EffectType
    {
        Flame,
        Attack,
        ItemUse,
        Shield,
        Combo
    }

    [System.Serializable]
    public class Effect
    {
        public EffectType type;
        public GameObject prefab;
        public bool isTemporary;
    }

    [SerializeField] private List<Effect> effects;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            comboPosition = new List<Vector2>
            {
                new Vector2(-0.61f, 1.76f),
                new Vector2(0.38f, 1.28f),
                new Vector2(-0.72f, 0.49f),
                new Vector2(0.4f, 0.39f)
            };
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 이펙트를 생성하는 메서드
    public void PlayEffect(EffectType effectType, Actor target)
    {
        Effect effect = effects.Find(e => e.type == effectType);
        if (effect == null)
        {
            Debug.LogWarning($"Effect of type {effectType} not found");
            return;
        }

        GameObject effectInstance;

        if (effectType == EffectType.Combo)
        {
            effectInstance = Instantiate(effect.prefab, target.transform.position, Quaternion.identity, target.transform);
        }
        else
        {
            effectInstance = Instantiate(effect.prefab, target.transform.position, Quaternion.identity, target.transform);
        }

        if (effect.isTemporary)
        {
            float duration = GetEffectDuration(effectInstance);
            Destroy(effectInstance, duration);
        }
    }

    public void PlayEffect(EffectType effectType, Actor target, int repeat)
    {
        Effect effect = effects.Find(e => e.type == effectType);
        if (effect == null)
        {
            Debug.LogWarning($"Effect of type {effectType} not found");
            return;
        }

        if (repeat >= 4)
        {
            for (int i = 4; i < repeat; i++)
            {
                float randomX = Random.Range(-0.72f, 0.4f);
                float randomY = Random.Range(0.39f, 1.76f);

                Vector2 randomPosition = new Vector2(randomX, randomY);
                comboPosition.Add(randomPosition);
            }
        }

        for (int i = 1; i < repeat; i++)
        {
            Vector2 positionOffset = comboPosition[i]; // No need for modulo if repeat <= comboPosition.Count
            Vector3 effectPosition = target.transform.position + new Vector3(positionOffset.x, positionOffset.y, 0f);

            GameObject effectInstance = Instantiate(effect.prefab, effectPosition, Quaternion.identity, target.transform);

            if (effect.isTemporary)
            {
                float duration = GetEffectDuration(effectInstance);
                Destroy(effectInstance, duration);
            }
        }
    }

    public void StartPlayEffectWithDelay(EffectType _type, Actor actor, int _repeat)
    {
        type = _type;
        _target = actor;
        repeatNum = _repeat;
        StartCoroutine("PlayEffectsWithDelay");
    }

    public IEnumerator PlayEffectsWithDelay()
    {
        Effect effect = effects.Find(e => e.type == type);

        if (repeatNum >= 4)
        {
            for (int i = 4; i < repeatNum; i++)
            {
                float randomX = Random.Range(-0.72f, 0.4f);
                float randomY = Random.Range(0.39f, 1.76f);

                Vector2 randomPosition = new Vector2(randomX, randomY);
                comboPosition.Add(randomPosition);
            }
        }

        for (int i = 0; i < repeatNum; i++)
        {
            Vector2 positionOffset = comboPosition[i % comboPosition.Count]; // Modulo to avoid index out of bounds
            Vector3 effectPosition = _target.transform.position + new Vector3(positionOffset.x, positionOffset.y, 0f);

            GameObject effectInstance = Instantiate(effect.prefab, effectPosition, Quaternion.identity, _target.transform);

            if (effect.isTemporary)
            {
                float duration = GetEffectDuration(effectInstance);
                Destroy(effectInstance, duration);
            }

            // Introduce a delay before the next effect instantiation
            yield return new WaitForSeconds(0.1f);
        }
    }

    // 이펙트의 애니메이션 길이를 가져오는 메서드
    private float GetEffectDuration(GameObject effectInstance)
    {
        Animator animator = effectInstance.GetComponent<Animator>();
        if (animator != null)
        {
            return animator.GetCurrentAnimatorStateInfo(0).length;
        }

        Animation animation = effectInstance.GetComponent<Animation>();
        if (animation != null)
        {
            float maxLength = 0f;
            foreach (AnimationState state in animation)
            {
                if (state.length > maxLength)
                {
                    maxLength = state.length;
                }
            }
            return maxLength;
        }

        // 애니메이션 컴포넌트가 없는 경우, 기본 지속 시간을 설정
        Debug.LogWarning("No Animator or Animation component found on effect instance.");
        return 1f;
    }
}