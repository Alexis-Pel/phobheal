using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StepGameObject
{
    public List<GameObject> objects;
}

[System.Serializable]
public class StepGameObjectsList
{
    public List<StepGameObject> Steps;
}
