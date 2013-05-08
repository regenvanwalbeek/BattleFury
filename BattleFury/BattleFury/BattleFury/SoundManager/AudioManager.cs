using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using BattleFury.Components;
using BattleFury.Settings;

namespace BattleFury.SoundManager
{
    /// <summary>
    /// Audio manager which allows for easy playing of sounds.
    /// </summary>
    public class AudioManager
    {

        private static SoundEffectInstance battleMusicInstance;

        private static Random random = new Random();

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
                battleMusicInstance.Volume = MusicVolume *.40f;
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

        public static void PlayPunch()
        {
            int index = random.Next(5);
            if (index == 0)
            {
                ContentLoader.Punch1.Play(FXVolume * 0.5f, 0, 0);
            }
            else if (index == 1)
            {
                ContentLoader.Punch2.Play(FXVolume * 0.5f, 0, 0);
            }
            else if (index == 2)
            {
                ContentLoader.Punch3.Play(FXVolume * 0.5f, 0, 0);
            }
            else
            {
                ContentLoader.Punch4.Play(FXVolume * 0.5f, 0, 0);
            }
        }

        public static void PlayMissedPunch()
        {
            ContentLoader.MissedPunch.Play(FXVolume * 1.0f, 0, 0);
        }

        public static void PlayFirstBlood()
        {
            ContentLoader.FirstBlood.Play(FXVolume * 1.0f, 0, 0);
        }
      


    }
}
