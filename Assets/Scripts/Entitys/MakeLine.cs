using UnityEngine;

public class MakeLine : MonoBehaviour
{
    private RectTransform imageRectTransform; 
    float lineWidth = 2f; 
    public Vector3 pointA; 
    public Vector3 pointB;
    public Vector3 pointC;
    public Vector3 pointD;
    public Vector3 pointE;


    // Start is called before the first frame update
    void Start()
    {
        imageRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 differenceVector = pointB - pointA; 
        imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude, lineWidth); 
        imageRectTransform.pivot = new Vector2(0, 0.5f); 
        imageRectTransform.position = pointA; 
        float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg; 
        imageRectTransform.rotation = Quaternion.Euler(0, 0, angle);

    }

}
