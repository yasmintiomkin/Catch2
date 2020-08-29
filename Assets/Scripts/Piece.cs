using ExtensionMethods;
using UnityEngine;

public class Piece : MonoBehaviour
{
	[SerializeField]
	private GameEvent OnPieceClicked;

	[SerializeField]
	private GameEvent OnFallFromBottom;

    private void OnMouseDown()
	{
		OnPieceClicked.Raise();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.IsBottomCollider())
        {
            OnFallFromBottom.Raise();
            Destroy(gameObject);
        }
    }
}
