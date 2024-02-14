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

    public virtual void UpdateAttack()
    {
        if (__attackIconAnimator == null)
        {
            __attackIconAnimator = _attackIcon.transform.parent.GetComponent<Animator>();
        }
        else
        {
            __attackIconAnimator.SetTrigger("Change");
        }
        _power = statSystem.ATK;
        _power = Random.Range(_power, _power + 2);
        _atkText.text = $"{_power}";
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