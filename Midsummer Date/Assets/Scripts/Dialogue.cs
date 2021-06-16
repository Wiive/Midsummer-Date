using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Character character;
    //public string speakerName;

    [TextArea(3, 10)]
    public string[] sentences;
}