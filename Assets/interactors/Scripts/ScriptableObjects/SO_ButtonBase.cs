using UnityEngine;

[CreateAssetMenu(fileName = "ButtonBase", menuName = "Buttons/Button Base", order = 1)]
public class SO_ButtonBase : ScriptableObject
{
    public Sprite buttonSprite;
    public Color hoverColor;
    public Color clickColor;
}
