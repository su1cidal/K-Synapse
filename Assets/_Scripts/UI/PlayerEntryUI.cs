using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntryUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _correctAnswers;
    [SerializeField] private TMP_Text _wrongAnswers;
    [SerializeField] private TMP_Text _damageReceived;
    [SerializeField] private TMP_Text _keysGained;
    [SerializeField] private TMP_Text _keysLost;

    public void SetImageBackground(Material material)
    {
        _image.material = material;
    }

    public void SetName(string name)
    {
        _name.text = name;
    }
    
    public void SetCorrectAnswers(string value)
    {
        _correctAnswers.text = value;
    }
    
    public void SetWrongAnswers(string value)
    {
        _wrongAnswers.text = value;
    }
    
    public void SetDamageReceived(string value)
    {
        _damageReceived.text = value;
    }
    
    public void SetKeysGained(string value)
    {
        _keysGained.text = value;
    }
    
    public void SetKeysLost(string value)
    {
        _keysLost.text = value;
    }
}
