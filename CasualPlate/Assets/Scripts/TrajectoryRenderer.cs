using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private GameObject fx;
    private LineRenderer lineRendererComponent;
    [SerializeField] private Transform arrow;
    public Transform forLook;
    Vector3[] points;
    private void Start()
    {
        lineRendererComponent = GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        points = new Vector3[100];
        lineRendererComponent.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;

            points[i] = origin + speed * time + Physics.gravity * time * time / 2f;
            fx.transform.position = points[5];
            if(points[i].y < 0)
            {
                lineRendererComponent.positionCount = i+1;
                break;
            }
        }

        lineRendererComponent.SetPositions(points);
    }
    void Update()
    {
        lineRendererComponent.numCornerVertices = 5;
        forLook.position = new Vector3(points[5].x, forLook.position.y,forLook.position.z);
        arrow.position = new Vector3(forLook.position.x, arrow.position.y, arrow.position.z);
    }
}