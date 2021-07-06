using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Information", menuName = "Information")]
public class Information : ScriptableObject
{
    public string title;
    [TextArea(3, 10)]
    public string information;
    public Sprite image;
}
