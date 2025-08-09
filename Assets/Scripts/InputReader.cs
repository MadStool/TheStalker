using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    public Vector2 ReadInput()
    {
        return new Vector2(
            Input.GetAxis(HorizontalAxis),
            Input.GetAxis(VerticalAxis)
        );
    }

    public bool HasInput()
    {
        return Input.GetAxis(HorizontalAxis) != 0f ||
               Input.GetAxis(VerticalAxis) != 0f;
    }
}