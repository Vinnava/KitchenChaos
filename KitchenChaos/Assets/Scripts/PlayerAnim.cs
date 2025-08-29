using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private const string IS_WALKING = "bIsWalking";
    private static readonly int IS_WALKING_HASHKEY = Animator.StringToHash(IS_WALKING);
    
    private Animator _animator;
    [SerializeField] private Player player;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        _animator.SetBool(IS_WALKING_HASHKEY, player.IsWalking());
    }
}
