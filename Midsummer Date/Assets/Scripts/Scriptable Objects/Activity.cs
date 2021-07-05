using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Activity", menuName = "Activity")]
public class Activity : ScriptableObject
{
    public string title;
    public Sprite picture;
    public enum ActiveTime { Morning, Midday, Evening}
    public ActiveTime activeTime;

    public Conversation conversation;
}
