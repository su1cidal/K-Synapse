using UnityEngine;

public class SoundManager : MonoBehaviour

{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        // DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        // CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        // Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        // BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        // TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    // private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    // {
    //     TrashCounter trashCounter = sender as TrashCounter;
    //     PlaySound(_audioClipRefsSO.trash, trashCounter.transform.position);
    // }
    //
    // private void BaseCounter_OnAnyObjectPlaced(object sender, System.EventArgs e)
    // {
    //     BaseCounter baseCounter = sender as BaseCounter;
    //     PlaySound(_audioClipRefsSO.objectDrop, baseCounter.transform.position);
    // }
    //
    // private void Player_OnPickedSomething(object sender, System.EventArgs e)
    // {
    //     PlaySound(_audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    // }
    //
    // private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    // {
    //     CuttingCounter cuttingCounter = sender as CuttingCounter;
    //     PlaySound(_audioClipRefsSO.chop, cuttingCounter.transform.position);
    // }
    //
    // private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    // {
    //     DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
    //     PlaySound(_audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    // }
    //
    // private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    // {
    //     DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
    //     PlaySound(_audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    // }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootsetpsSound(Vector3 position, float volume)
    {
        PlaySound(_audioClipRefsSO.footstep, position, volume);
    }
}