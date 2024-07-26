using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public enum EffectType
    {
        Flame,
        Attack,
        ItemUse,
        Shield
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

        GameObject effectInstance = Instantiate(effect.prefab, target.transform.position, Quaternion.identity, target.transform);

        if (effect.isTemporary)
        {
            float duration = GetEffectDuration(effectInstance);
            Destroy(effectInstance, duration);
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