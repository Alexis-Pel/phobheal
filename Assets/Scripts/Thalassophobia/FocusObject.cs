using UnityEngine;

public class FocusObject : MonoBehaviour
{
    [SerializeField] private Vector3[] objectives;
    [field: SerializeField] public float Speed { get; private set; }

    private Vector3 currentObjective;
    private int idCurrentObjective;
    private Vector3? initPos = null;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        idCurrentObjective = 0;
        DefineObjective();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(currentObjective);
        transform.position += Speed * Time.deltaTime * transform.forward;
        //If the focus is close enough from it's current objective, we reselect an other
        if (Vector3.Distance(transform.position, currentObjective) < 0.1)
        {
            idCurrentObjective++;
            DefineObjective();
        }
    }

    private void DefineObjective()
    {
        if (idCurrentObjective >= objectives.Length) { idCurrentObjective = 0; }
        Vector3 mvt = objectives[idCurrentObjective];
        currentObjective = transform.position + mvt;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 oldObjective = initPos != null ? initPos.Value : transform.position;
        for (int i = 0; i < objectives.Length; i++)
        {
            Vector3 currentPos = oldObjective + objectives[i];
            Gizmos.DrawLine(currentPos, oldObjective);
            oldObjective = currentPos;
        }
    }
}
