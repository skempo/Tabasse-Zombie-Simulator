using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ViewportHandler : MonoBehaviour
{
    
    public float UnitsSize = 1; // size of your scene in unity units
    public Constraint constraint;
    public static ViewportHandler ViewportHandlerScript;
    public new Camera camera;

    
    #region METHODS
    private void Awake()
    {
        camera = GetComponent<Camera>();
        ViewportHandlerScript = this;
        ComputeResolution();

        Debug.Log(camera.aspect);
    }

    private void ComputeResolution()
    {
        float leftX, rightX, topY, bottomY;

        if (constraint == Constraint.Landscape)
        {
            camera.orthographicSize = UnitsSize / (camera.aspect * 2);
        }
        
    }

    private void Update()
    {
#if UNITY_EDITOR
        ComputeResolution();
#endif
    }

    #endregion

    public enum Constraint { Landscape, Portrait }
}
