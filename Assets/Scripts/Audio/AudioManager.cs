using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;

    [Header("Background Music Clips")]
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameplayMusic;

    [Header("Turret Fire Clips")]
    [SerializeField] private AudioClip[] _ballistaFireClips;
    [SerializeField] private AudioClip[] _cannonFireClips;
    [SerializeField] private AudioClip[] _slowTurretFireClips;

    [Header("Impact & Hit Clips")]
    [SerializeField] private AudioClip[] _enemyHitClips;
    [SerializeField] private AudioClip[] _enemyDeathClips;
    [SerializeField] private AudioClip[] _playerDamageClips;

    [Header("UI & Economy Clips")]
    [SerializeField] private AudioClip[] _coinCollectClips;
    [SerializeField] private AudioClip _buttonClickClip;
    [SerializeField] private AudioClip _buildTurretClip;
    [SerializeField] private AudioClip _upgradeTurretClip;
    [SerializeField] private AudioClip _sellTurretClip;

    [Header("Stingers")]
    [SerializeField] private AudioClip _winStinger;
    [SerializeField] private AudioClip _gameOverStinger;

    private Dictionary<AudioClip, float> _sfxCooldowns = new Dictionary<AudioClip, float>();
    private const float SFX_COOLDOWN_TIME = 0.05f;

    private void Awake() => I = this;

    #region Random SFX Player

    public void PlaySFX(AudioClip clip, float volumeScale = 1.0f, float pitchMin = 0.95f, float pitchMax = 1.05f)
    {
        if (_sfxSource == null || clip == null) return;

        if (_sfxCooldowns.TryGetValue(clip, out float lastPlayedTime))
        {
            if (Time.unscaledTime - lastPlayedTime < SFX_COOLDOWN_TIME)
            {
                return;
            }
        }

        _sfxCooldowns[clip] = Time.unscaledTime;

        _sfxSource.pitch = Random.Range(pitchMin, pitchMax);
        _sfxSource.PlayOneShot(clip, volumeScale);
    }

    public void PlayRandomSFX(AudioClip[] clips, float volumeScale = 1.0f, float pitchMin = 0.95f, float pitchMax = 1.05f)
    {
        if (clips == null || clips.Length == 0) return;

        int randomIndex = Random.Range(0, clips.Length);
        PlaySFX(clips[randomIndex], volumeScale, pitchMin, pitchMax);
    }

    #endregion

    #region Action Audio Triggers

    public void PlayBallistaFire() => PlayRandomSFX(_ballistaFireClips, 1.0f, 0.95f, 1.05f);
    public void PlayCannonFire() => PlayRandomSFX(_cannonFireClips, 1.0f, 0.98f, 1.02f);
    public void PlaySlowTurretFire() => PlayRandomSFX(_slowTurretFireClips, 1.0f, 0.95f, 1.05f);

    public void PlayEnemyHit() => PlayRandomSFX(_enemyHitClips, 0.7f, 0.90f, 1.10f);
    public void PlayEnemyDeath() => PlayRandomSFX(_enemyDeathClips, 1.0f, 0.92f, 1.08f);
    public void PlayPlayerDamage() => PlayRandomSFX(_playerDamageClips, 1.0f, 0.95f, 1.05f);

    public void PlayCoinCollect() => PlayRandomSFX(_coinCollectClips, 0.8f, 0.90f, 1.15f);
    public void PlayButtonClick() => PlaySFX(_buttonClickClip, 1.0f, 1.0f, 1.0f);
    public void PlayBuildTurret() => PlaySFX(_buildTurretClip, 1.0f, 1.0f, 1.0f);
    public void PlayUpgradeTurret() => PlaySFX(_upgradeTurretClip, 1.0f, 1.0f, 1.0f);
    public void PlaySellTurret() => PlaySFX(_sellTurretClip, 1.0f, 1.0f, 1.0f);

    public void PlayWinStinger() => PlaySFX(_winStinger, 1.0f, 1.0f, 1.0f);
    public void PlayGameOverStinger() => PlaySFX(_gameOverStinger, 1.0f, 1.0f, 1.0f);

    #endregion

    #region Music Player

    public void PlayMenuMusic() => SwitchMusic(_menuMusic);
    public void PlayGameplayMusic() => SwitchMusic(_gameplayMusic);

    public void StopMusic()
    {
        if (_musicSource != null)
        {
            _musicSource.Stop();
        }
    }

    private void SwitchMusic(AudioClip newClip)
    {
        if (_musicSource == null || newClip == null) return;

        if (_musicSource.clip != newClip)
        {
            _musicSource.clip = newClip;
            _musicSource.loop = true;
            _musicSource.Play();
        }
        else if (!_musicSource.isPlaying)
        {
            _musicSource.Play();
        }
    }

    #endregion
}
