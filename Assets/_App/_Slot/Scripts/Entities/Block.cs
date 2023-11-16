using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Block : MonoBehaviour
{
    [SerializeField] private int _ID;
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Sprite _movingSprite;
    [SerializeField] private GameObject _frameEffect;
    
    private Image _image;

    public int ID => _ID;

    public void Init()
    {
        _image = GetComponent<Image>();

        _image.sprite = _idleSprite;
    }

    public void ChangeSprite(bool movingStatus)
    {
        _image.sprite = movingStatus ? _movingSprite : _idleSprite;
    }

    public void ActiveFrameEffect(bool status)
    {
        _frameEffect.SetActive(status);
    }
}
