using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReadyGroupController : MonoBehaviour
{
    public float FloatHeight = 1f;
    public float P1HalfCycle = 1f, P2HalfCycle = 1f;

    [SerializeField]
    private GameObject _p1, _p2;
    [SerializeField]
    private Sprite _p1ReadySprite, _p2ReadySprite;
    [Space(5)]
    [SerializeField]
    private GameObject _p1Key, _p1Press, _p1Joined;
    [Space(5)]
    [SerializeField]
    private GameObject _p2Key, _p2Press, _p2Joined;


    private bool _p1Ready = false, _p2Ready = false;
    private Transform _p1Transform, _p2Transform;
    private Vector3 _p1Origin, _p2Origin;

    // Start is called before the first frame update
    void Start()
    {
        _p1Transform = _p1.transform;
        _p1Origin = _p1Transform.localPosition;
        _p2Transform = _p2.transform;
        _p2Origin = _p2Transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        _p1Transform.localPosition = _p1Origin + Vector3.up * FloatHeight * Mathf.SmoothStep(-1, 1, Mathf.PingPong(Time.time / P1HalfCycle, 1));
        _p2Transform.localPosition = _p2Origin + Vector3.up * FloatHeight * Mathf.SmoothStep(-1, 1, Mathf.PingPong(Time.time / P2HalfCycle, 1));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(P1FlashJoined());
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(P2FlashJoined());
        }
    }


    private IEnumerator P1FlashJoined()
    {
        if (_p1Ready) yield return null;
        _p1Ready = true;
        _p1Key.SetActive(false);
        _p1Press.SetActive(false);
        _p1Joined.SetActive(true);

        if(_p2Ready)
        {
            StartCoroutine(Proceed());
        }

        var flash = true;
        while (true)
        {
            _p1Joined.GetComponent<Text>().color = flash ? Color.cyan : Color.white;

            flash = !flash;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator P2FlashJoined()
    {
        if (_p2Ready) yield return null;
        _p2Ready = true;
        _p2Key.SetActive(false);
        _p2Press.SetActive(false);
        _p2Joined.SetActive(true);

        if (_p1Ready)
        {
            StartCoroutine(Proceed());
        }

        var flash = true;
        while (true)
        {
            _p2Joined.GetComponent<Text>().color = flash ? new Color(1,0.8f,0.8f): Color.white;

            flash = !flash;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Proceed()
    {
        // proceeds to next scene

        yield return new WaitForSeconds(3f);

        yield return null;
    }




}
