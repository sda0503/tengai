using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class MonsterBase : MonoBehaviour
{
    [SerializeField] private Buff _buff;
    [SerializeField] private Image _attackIcon;
    [SerializeField] private TextMeshProUGUI _atkText;
    private Animator __attackIconAnimator;

    public StatSystem statSystem;
    public StatSystem target;

    protected int _power;
    private int _atk;

    public virtual void UpdateAttack()
    {
        if (__attackIconAnimator == null)
        {
            __attackIconAnimator = _attackIcon.transform.parent.GetComponent<Animator>();
            _atk = statSystem.ATK;
        }
        else
        {
            __attackIconAnimator.SetTrigger("Change");
        }
        _power = statSystem.ATK;
        _power = Random.Range(_atk, _atk + 2);
        _atkText.text = $"{statSystem.ATK}";
    }

    public void UpdateText()
    {
        _atkText.text = $"{statSystem.ATK}";
    }

    public virtual void CheckATKText()
    {
        _atkText.gameObject.SetActive(true);
    }

    public virtual void Attack()
    {
        statSystem.Attack();
    }
}