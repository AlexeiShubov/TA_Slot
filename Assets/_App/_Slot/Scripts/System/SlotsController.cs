using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

public sealed class SlotsController : MonoBehaviourExtBind
{
    [SerializeField] private GameObject _particleEffect;
    [SerializeField] private GameObject[] _resultEffect;
    [SerializeField] private Slot[] _slots;

    private int _currentStoppingSlot = 0;
    
    [OnAwake]
    private void SlotInitialize()
    {
        foreach (var slot in _slots)
        {
            slot.Init();
        }
    }

    [Bind(Keys.MovingState)]
    private void ActivateSlots()
    {
        _currentStoppingSlot = 0;
        _particleEffect.SetActive(true);
        
        foreach (var slot in _slots)
        {
            slot.DoAction();
        }
    }

    [Bind(Keys.StoppingState)]
    private void DeactivateSlots()
    {
        if (_currentStoppingSlot >= _slots.Length)
        {
            _particleEffect.SetActive(false);
            Settings.Invoke(Keys.AllBlocksIsIdle);
            
            return;
        }
        
        // Будем считать, что получили данные выигрышного лота откуда-то из вне
        _slots[_currentStoppingSlot].StopSlot(Random.Range(0, 3), DeactivateSlots);
        _currentStoppingSlot++;
    }

    [Bind(Keys.BonusState)]
    private void ActiveResultAffect()
    {
        foreach (var effect in _resultEffect)
        {
            effect.SetActive(true);
        }
    }
}
