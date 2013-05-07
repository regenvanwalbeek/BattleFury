using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using BattleFury.Components;
using BattleFury.Settings;

namespace BattleFury.SoundManager
{
    public class AudioManager
    {

        private static SoundEffectInstance battleMusicInstance;

        private static float MusicVolume
        {
            get
            {
                return GameSettings.MusicVolume / 10.0f;
            }
        }

        private static float FXVolume
        {
            get
            {
                return GameSettings.FXVolume / 10.0f;
            }
        }
        

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        public static void PlayFireLaser()
        {
            ContentLoader.ProjectileSoundEffect.Play(FXVolume * 0.5f, 0, 0);
        }

        public static void PlayJump()
        {

            ContentLoader.JumpSoundEffect.Play(FXVolume * 0.3f, 0.0f, 0.0f);
        }

        public static void PlayPain()
        {
            ContentLoader.PainSound.Play(FXVolume * 0.5f, 0, 0);
        }

        public static void StartBattleMusic()
        {
            if (battleMusicInstance == null)
            {
                battleMusicInstance = ContentLoader.BattleMusic.CreateInstance();
                battleMusicInstance.IsLooped = true;
                battleMusicInstance.Volume = MusicVolume *.5f;
                battleMusicInstance.Play();
            }
        }

        public static void StopBattleMusic()
        {
            if (battleMusicInstance != null)
            {
                battleMusicInstance.Stop();
            }
            battleMusicInstance = null;
        }

      


    }
}
