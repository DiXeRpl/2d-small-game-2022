using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Referencje
    private Player player;
    private PlayManager playManager;

    // Zablokowanie uciekania enemy poza ekran
    public CanvasScaler canvasScaler;

    // Kolider
    public BoxCollider colliderEnemy;

    // Flaga
    private bool isMoving = true;

    // Kierunek ruchu
    private Vector3 move;

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        if (playManager == null)
        {
            playManager = FindObjectOfType<PlayManager>();
        }

        if (canvasScaler == null)
        {
            canvasScaler = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasScaler>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(canvasScaler.referenceResolution);
        Debug.Log(transform.localPosition);
        // Ruszamy sie
        isMoving = true;

        // Ustalenie losowego kierunku ruchu
        RandomMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // Calculate distance from player
            float distance = Vector3.Distance(this.transform.position, player.transform.position);

            // Jesli player jest blisko
            if (distance < 100)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, - 1 * playManager.speedEnemy * Time.deltaTime);
            }
            // Jesli player jest daleko
            else
            {
                transform.position += move * playManager.speedEnemy * Time.deltaTime;
            }
        }
    }

    /*
     * Klatka po Update
     */

    private void LateUpdate()
    {
        // Zebranie poaktualnego polozenia
        Vector3 currentPosition = transform.localPosition;

        // Sprawdzenie czy polozenie wychodzi poza ekran - jesli tak to korekta
        currentPosition.x = Mathf.Clamp(currentPosition.x, -1 * canvasScaler.referenceResolution.x / 2, canvasScaler.referenceResolution.x / 2);
        currentPosition.y = Mathf.Clamp(currentPosition.y, -1 * canvasScaler.referenceResolution.y / 2, canvasScaler.referenceResolution.y / 2);

        // Jesli nie by³o korekty
        /*if (transform.position == currentPosition)
        {
            Debug.Log("Nie trzeba korekty");
        }*/

        // Oddanie polozenia po korekcie
        transform.localPosition = currentPosition;
    }

    /*
     * Ustalenie losowego kierunku ruchu
     */

    private void RandomMove()
    {
        move = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
    }

    /*
     * Zatrzymaj mnie
     */

    public void StopMe()
    {
        isMoving = false;

        // Zmien kolor na czarny 
        this.gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 100);

        // Znikniecie kolidera w czasie
        StartCoroutine(ColliderDestroy());
    }

    /*
     * Znikniecie kolidera po krotkim czasie - inaczej characterController na playerze dziwnie sie zachowuje
     */

    IEnumerator ColliderDestroy()
    {
        // Odczekanie krotkiej chwilii
        yield return new WaitForSeconds(0.01f);
        // Znikniecie kolidera
        colliderEnemy.enabled = false;
    }
}
