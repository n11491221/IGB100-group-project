using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    public int keyCount = 0;
    public Image keyImage;
    public Sprite[] keySprites; 

    void Start()
    {
        UpdateKeyImage();
    }

    public void AddKey()
    {
        keyCount++;
        UpdateKeyImage();
    }

    public void RemoveKey()
    {
        if (keyCount > 0)
        {
            keyCount--;
            UpdateKeyImage();
        }
    }

    void UpdateKeyImage()
    {
        
        if (keyCount < keySprites.Length)
        {
            keyImage.sprite = keySprites[keyCount];
        }
    }
}