using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class LineController : MonoBehaviour
{
    public static LineController instance;
    public LineRenderer lr;
    public static Vector3 originPoint;
    public static Vector3 targetPoint;
    public static int destination;
    public List<Vector3> cornerPoints=new List<Vector3>();
    public NavMeshPath path;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        lr=FindObjectOfType<LineRenderer>();
        lr.enabled=false;
        path = new NavMeshPath();
    }

    
    void Update()
    {
        DrawLine();

        // destination = cornerPoints[];
        
        
    }
    void DrawLine()
    {
        NavMesh.CalculatePath(originPoint, targetPoint, NavMesh.AllAreas, path);
        if(targetPoint!=null)
        {
            if(path.corners.Length>=1)
            {

                int i = 0;
                while(i<path.corners.Length)
                {
                    
                    cornerPoints=path.corners.ToList();
                    lr.positionCount= cornerPoints.Count;

                    for(int j=0; j<cornerPoints.Count; j++)
                    {
                        lr.SetPosition(j, cornerPoints[j]);

                    }
                    i++;
                }
            }
        }
    }
}
                
                    
                


        

        

