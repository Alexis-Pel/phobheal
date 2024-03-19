using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Phobia", menuName = "Phobia/New Phobia")]
public class PhobiaScriptable : ScriptableObject
{
    // Info
    public string m_name;
    public ScenesEnum m_scene;
    public Sprite m_image;
}
