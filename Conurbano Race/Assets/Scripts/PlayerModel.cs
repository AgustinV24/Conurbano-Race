using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
public class PlayerModel : NetworkBehaviour
{
    public float speed = 10f;             // Velocidad de movimiento del auto
    public float rotationSpeed = 100f;    // Velocidad de rotación del auto

    private float horizontalInput;        // Entrada horizontal (izquierda/derecha)
    private float verticalInput;          // Entrada vertical (adelante/atrás)
    public NetworkRigidbody kartRigidbody;     // Referencia al componente Rigidbody del auto
    public Item _currentItem;
    public NetworkInputData _inputData;
    public float rotationThreshold = 0.001f;
    public Item[] items = new Item[1];

    [Networked]
    public bool call { get; set; }

    [SerializeField] GameObject _camera;
    float rotation;
    private bool canRotate;
    [Networked]
    private bool canMove { get; set;}
    float forwardSpeed;

    public GameObject inkImage;
    public Horn hornPrefab;
    public bool hasItem;
    public string item;

    PlayerManager PM;
    [SerializeField] SpawnNetworkPlayer _snp;


    public override void Spawned()
    {
        _snp = FindObjectOfType<SpawnNetworkPlayer>();
        _snp.OnConnected(GetComponent<NetworkPlayer>());
        canMove = true;
        //items[0] = new SpeedBoost();
        //items[1] = new InkItem();
        items[0] = new HornItem();
        
        if (!HasInputAuthority)
        {
            _camera.SetActive(false);
        }

        PM = FindObjectOfType<PlayerManager>();
    }
    public void Update()
    {
        forwardSpeed = Vector3.Dot(kartRigidbody.Rigidbody.velocity, transform.forward);

        
        canRotate = Mathf.Abs(forwardSpeed) > rotationThreshold;
        if(_currentItem != null)
        item = _currentItem.ToString();

        if (call)
        {
            call = false;
            DetectOtherPLayers();
        }
    }
    public override void FixedUpdateNetwork()
    {
       
        if (GetInput(out _inputData))
        {
            if (canMove)
            {
                Move(_inputData.xMovement, _inputData.zMovement);
            }        
            
        }

        if (_inputData.isUsingItem)
        {            
            _currentItem.Actions(this);
            hasItem = false;
        }
    }
    void FixedUpdate()
    {

        // Calcular la velocidad de movimiento

    }
    void Move(float xAxis, float zAxis)
    {
        
        Vector3 movement = Vector3.zero;
        if (zAxis < 0)
        {
            movement = transform.forward * zAxis * speed/3 * Time.fixedDeltaTime;
        }        
        else
        {
            movement = transform.forward * zAxis * speed * Time.fixedDeltaTime;
        }

        kartRigidbody.Rigidbody.AddForce(movement, ForceMode.Impulse);

        float speedReduction = 100 - Mathf.Clamp(Vector3.Angle(transform.forward, kartRigidbody.Rigidbody.velocity), 0, 70);
        if(!_inputData.isBeingBoosted)
            kartRigidbody.Rigidbody.velocity = Vector3.ClampMagnitude(kartRigidbody.Rigidbody.velocity, speedReduction);


        

        if (canRotate)
        {
            rotation = xAxis * rotationSpeed * Time.fixedDeltaTime; 
            
        }
        else
        {
            rotation = Mathf.Lerp(rotation, 0, 0.1f);
        }

        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotation);
        kartRigidbody.Rigidbody.MoveRotation(kartRigidbody.Rigidbody.rotation * deltaRotation);
    }

    public void CouroutineActivator(IEnumerator Cou)
    {
        StartCoroutine(Cou);
    }

    public IEnumerator ActivateImage()
    {
        inkImage.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        inkImage.SetActive(false);
    }

    public IEnumerator HornActivation()
    {
        Horn obj = Runner.Spawn(hornPrefab, transform.position, Quaternion.identity);        
        obj.transform.parent = this.transform;
        obj.playerM = this;
        obj.sC.enabled = true;
        yield return new WaitForSeconds(3f);
        Runner.Despawn(obj.Object);
    }

    public IEnumerator MovementLimiting()
    {        
        canMove = false;
        yield return new WaitForSeconds(100f);
        canMove = true;
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false, InvokeResim = true, TickAligned = true)]
    public void RPC_Ink()
    {        
        call = true;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_Stun()
    {
        kartRigidbody.Rigidbody.velocity = Vector3.zero;
        StartCoroutine(MovementLimiting());
    }

    public void DetectOtherPLayers()
    {
        var col = PM.GetConnectedPlayers().ToArray();

        foreach(var item in col)
        {
            if(item != null)
            {
                Debug.Log(item);
                if(item != this)
                {
                    item.StartCoroutine(item.ActivateImage());
                    
                }
                    
            }
            
        }


        //NetworkObject otherPlayer = null; 


        //foreach (var item in Runner.ActivePlayers)
        //{
        //    otherPlayer = Runner.GetPlayerObject(item);
        //    if(otherPlayer != null)
        //    {
        //        if (otherPlayer.IsProxy) break;
        //    }
            
        //}

        //if(otherPlayer != null)
        //{
        //    StartCoroutine(ActivateImage(otherPlayer.GetComponent<PlayerModel>()));
        //    //if (otherPlayer.HasInputAuthority)
        //    //{
                
        //    //}
        //}      

        
    }
}
