using MyExtensions.RandomSelection;
using UnityEngine;

public abstract class BaseSlotSymbolSO : ScriptableObject, IRandomSelectedWithWeight
{
    [SerializeField] private int _symbolID;
    [SerializeField] private Sprite _symbolSprite;
    [SerializeField, Range(1, 100)] private float _weight;
    public float GetWeight => _weight;

    public Sprite SymbolSprite { get => _symbolSprite; set => _symbolSprite = value; }
    public int SymbolID { get => _symbolID; private set => _symbolID = value; }
}
