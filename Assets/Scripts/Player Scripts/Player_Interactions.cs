using System;
using UnityEngine;

public class Player_Interactions : MonoBehaviour, IKitchenObjectParent
{
    public static Player_Interactions Instance { get; private set; }

    public event EventHandler OnPickedUpSomething;

    // Public Variables.
    [SerializeField] private GameInputs gameInputs;
    [SerializeField] private LayerMask CountersLayerMask;
    [SerializeField] private Transform KitchenObjectHoldPoint;

    // Private Variables.
    private Counter_Base selectedCounter;
    private KitchenObjects kitchenObject;

    // Events.
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public Counter_Base selectedCounter;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one Player_Interactions instance!");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInputs.OnInteractAction += GameInputs_OnInteractAction;
        gameInputs.OnInteractAlternateAction += GameInputs_OnInteractAlternateAction;
    }

    private void GameInputs_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaying()) return;
        if(selectedCounter != null)
        {
            selectedCounter.CounterInteractionAlternate(this);
        }
    }

    private void GameInputs_OnInteractAction(object sender, System.EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaying()) return;
        if(selectedCounter != null)
        {
            selectedCounter.CounterInteraction(this);
        }
    }

    private void Update()
    {
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        float interactionDistance = 2f;
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactionDistance, CountersLayerMask))
        {
            if(hitInfo.transform.TryGetComponent(out Counter_Base counterBase))
            {
                if(selectedCounter != counterBase)
                {
                    SetSelectedCounter(counterBase);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(Counter_Base selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null) OnPickedUpSomething?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObjects GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
