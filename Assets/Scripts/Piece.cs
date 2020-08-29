using ExtensionMethods;
using UnityEngine;

public class Piece : MonoBehaviour
{
	[SerializeField]
	GameEvent OnPieceClicked;

	[SerializeField]
	GameEvent OnFallFromBottom;

    [SerializeField]
    GameObject particlePF;


    private void OnMouseDown()
	{
        Instantiate(particlePF, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
