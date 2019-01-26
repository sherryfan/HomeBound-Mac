using UnityEngine;

public class ReadyGroupController : MonoBehaviour
{
    public float MaxSpriteSize = 1.3f, MinSpriteSize = 0.6f;
    public float HalfCycleTime = 5f;


    [SerializeField]
    private GameObject _p1, _p2;
    [SerializeField]
    private Sprite _p1ReadySprite, _p2ReadySprite;


    private bool _p1Ready = false, _p2Ready = false;
    private Transform _p1Transform, _p2Transform;

    // Start is called before the first frame update
    void Start()
    {
        _p1Transform = _p1.GetComponent<Transform>();
        _p2Transform = _p2.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_p1Ready && _p2Ready)
        {
            // Fire some sequence here/
            // If there's a CoRoutineSequence or anything, disable this script
            return;
        }

        if (_p1Ready)
        {
            _p1.GetComponent<SpriteRenderer>().sprite = _p1ReadySprite;
            _p1Transform.localScale = Vector3.one;
            //Changes the sprite of P1 Ready, along with the size.
        }
        if (_p2Ready)
        {
            _p2.GetComponent<SpriteRenderer>().sprite = _p2ReadySprite;
            _p2Transform.localScale = Vector3.one;
            //Changes the sprite of P2 Ready, along with the size.
        }
        _p1Transform.localScale = Vector3.one * Mathf.SmoothStep(MinSpriteSize, MaxSpriteSize, Mathf.PingPong(Time.time / HalfCycleTime, 1));
        _p2Transform.localScale = Vector3.one * Mathf.SmoothStep(MinSpriteSize, MaxSpriteSize, Mathf.PingPong(Time.time / HalfCycleTime, 1));


    }
}
