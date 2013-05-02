using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using BattleFury.Components;

namespace BattleFury.SoundManager
{
    public class AudioManager
    {

        static SoundEffectInstance battleMusicInstance;
        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        public static void PlayFireLaser()
        {
            ContentLoader.ProjectileSoundEffect.Play(0.5f, 0, 0);
        }

        public static void PlayJump()
        {
          
            ContentLoader.JumpSoundEffect.Play(0.3f, 0.0f, 0.0f);
        }

        public static void PlayPain()
        {
            ContentLoader.PainSound.Play(0.5f, 0, 0);
        }

        public static void StartBattleMusic()
        {
            if (battleMusicInstance == null)
            {
                battleMusicInstance = ContentLoader.BattleMusic.CreateInstance();
                battleMusicInstance.IsLooped = true;
                battleMusicInstance.Volume = .5f;
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
