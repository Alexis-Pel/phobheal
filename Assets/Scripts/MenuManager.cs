using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private ContentManager contentManager;

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private GameObject phobiaCard;

    [SerializeField]
    private PhobiaScriptable[] phobias;

    // Start is called before the first frame update
    void Start()
    {
        foreach (PhobiaScriptable phobia in phobias)
        {
            phobiaCard.GetComponent<PhobiaCard>().phobiaScriptable = phobia;
            GameObject phobiaCardToSpawn = Instantiate(phobiaCard, content.transform);
            contentManager.contentPanels.Add(phobiaCardToSpawn);
        }
        contentManager.InitializeDots();
    }
}
