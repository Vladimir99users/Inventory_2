using UnityEngine;

namespace Assets.Inventory.Scripts.Helpers.Audio
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance = null;
        [SerializeField] private AudioSource audioMusicSource;
        [SerializeField] private AudioSource audioSoundSource;


        [Header("Sound clip")]
        [SerializeField] private AudioClip createItemClip;
        [SerializeField] private AudioClip moveItemClip;
        [SerializeField] private AudioClip errorClip;
        [SerializeField] private AudioClip goodClip;

        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance == this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        public void PlayCreateItemSound()
            => audioSoundSource.PlayOneShot(createItemClip);

        public void PlayBeginSound()
            => audioSoundSource.PlayOneShot(moveItemClip);

        public void PlayErrorSound()
            => audioSoundSource.PlayOneShot(errorClip);
        public void PlayGoodSound()
            => audioSoundSource.PlayOneShot(goodClip);


    }
}
