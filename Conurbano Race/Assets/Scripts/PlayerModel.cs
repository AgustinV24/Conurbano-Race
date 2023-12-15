using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
using UnityEngine.UI;

public class PlayerModel : NetworkBehaviour
{
    public float speed = 10f;             
                 
    public float rotationSpeed = 100f;    
    private float horizontalInput;        
    private float verticalInput;          
    public NetworkRigidbody kartRigidbody;
    [Networked] public int _currentItemIndex{ get; set; }
    public Item _currentItem{ get; set; }
     public NetworkInputData _inputData;
    public NetworkMecanimAnimator myAnim;
    public float rotationThreshold = 0.001f;
    public Item[] items = new Item[3];
    public Sprite[] itemsImages = new Sprite[3];
    [Networked]
    public bool call { get; set; }

    [SerializeField] GameObject _camera;
    float rotation;
    private bool canRotate;
    [Networked]
    public bool canMove { get; set;}
    float forwardSpeed;
 
    public GameObject inkImage;
    public GameObject winScreen;
    public GameObject defeatScreen;
    public Horn hornPrefab;
    public bool hasItem;
    public string item;
    public Image itemImage;
    public PlayerManager PM;
    [SerializeField] SpawnNetworkPlayer _snp;
    [SerializeField] MeshRenderer _car;
    [Networked] public bool winner { get; set; }
    public override void Spawned()
    {
        PM = FindObjectOfType<PlayerManager>();
        
        

        //  canMove = true;
        items[2] = new SpeedBoost();
        items[1] = new InkItem();
        items[0] = new HornItem();
        
        if (!HasInputAuthority)
        {
            _camera.SetActive(false);
        }

        _car.material.color = Random.ColorHSV();


     
    }
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        _snp = FindObjectOfType<SpawnNetworkPlayer>();
        _snp.OnConnected(GetComponent<NetworkPlayer>());
        if (PM.GetConnectedPlayers().Count == 1)
        {
            transform.position = PM.spawnPoints[1].position;
        }
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
        if (Physics.Raycast(transform.position, Vector3.down, Mathf.Infinity, 10))
        {

        }
        else
        {
            Debug.Log("Auch");
        }
    }
    public void OnChange()
    {

    }
    public void Quit()
    {
       // Runner.Shutdown();
        Application.Quit();
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
            // Debug.Log(_currentItem);
            _currentItem.Actions(this);
            hasItem = false;
            UpdateCanvas();
        }
    }
    
    void Move(float xAxis, float zAxis)
    {
        
        Vector3 movement = Vector3.zero;
        

        if(zAxis == 0)
        {
            kartRigidbody.Rigidbody.velocity = Vector3.Lerp(kartRigidbody.Rigidbody.velocity, Vector3.zero, 0.015f);
        }
        if (zAxis < 0)
        {
            movement = transform.forward * zAxis * speed * Time.fixedDeltaTime;
            kartRigidbody.Rigidbody.AddForce(movement, ForceMode.Impulse);
            kartRigidbody.Rigidbody.velocity = Vector3.ClampMagnitude(kartRigidbody.Rigidbody.velocity, 5);
        }        
        else
        {
            movement = transform.forward * zAxis * speed * Time.fixedDeltaTime;
            kartRigidbody.Rigidbody.AddForce(movement, ForceMode.Impulse);
            kartRigidbody.Rigidbody.velocity = Vector3.ClampMagnitude(kartRigidbody.Rigidbody.velocity, 25);
        }

        

        float speedReduction = 30 - Mathf.Clamp(Vector3.Angle(transform.forward, kartRigidbody.Rigidbody.velocity), 0, 10);

        




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
            if(HasStateAuthority)
            obj.transform.parent = this.transform;
            obj.playerM = this;
            obj.sC.enabled = true;
            RPC_Boke();
            yield return new WaitForSeconds(3f);
            Runner.Despawn(obj.Object);


    }

    public IEnumerator MovementLimiting()
    {        
        canMove = false;
        myAnim.Animator.SetBool("Active", true);
        yield return new WaitForSeconds(0.2f);
        myAnim.Animator.SetBool("Active", false);
        yield return new WaitForSeconds(1.8f);        
        canMove = true;
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false, InvokeResim = true, TickAligned = true)]
    public void RPC_Ink()
    {        
        call = true;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_Box(int index)
    {
        _currentItemIndex = index;
        _currentItem = items[index];
        //hasItem = true;
        //UpdateCanvas();
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = itemsImages[index];
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_Stun()
    {
        kartRigidbody.Rigidbody.velocity = Vector3.zero;
        StartCoroutine(MovementLimiting());
    }

    [Rpc(RpcSources.All, RpcTargets.InputAuthority)]
    public void RPC_End()
    {

        canMove = false;
        if (winner)
        {            
            winScreen.SetActive(true);
        }
        else
        {
            defeatScreen.SetActive(true);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_Boke()
    {
        GetComponent<AudioSource>().Play();
    }
    public void UpdateCanvas()
    {
        if (hasItem)
        {
            itemImage.gameObject.SetActive(true);
            for (int i = 0; i < items.Length; i++)
            {
                if(items[i] == _currentItem)
                {
                    itemImage.sprite = itemsImages[i];
                }
            }
        }
        else
        {
            itemImage.gameObject.SetActive(false);
        }
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


        
        
    }

    public void Shutdown()
    {
        if(HasStateAuthority && HasInputAuthority)
        {

        }
        else if(!HasStateAuthority && HasInputAuthority)
        {

        }
    }
}
