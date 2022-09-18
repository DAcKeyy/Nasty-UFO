using System;
using UnityEngine;

namespace Miscellaneous.Saving
{
    public static class GlobalPlayerPrefs
    {
        public static int CurrentScore
        {
            get => PlayerPrefs.GetInt("CurrentScore", 0);
            set => PlayerPrefs.SetInt("CurrentScore", value);
        }
        
        public static int BestScore {
            get => PlayerPrefs.GetInt("BestScore", 0);
            set => PlayerPrefs.SetInt("BestScore", value);
        }
        
        public static float SoundValue {
            get => PlayerPrefs.GetFloat("SoundValue", 1f);
            set => PlayerPrefs.SetFloat("SoundValue", value);
        }
        
        public static float MusicValue {
            get => PlayerPrefs.GetFloat("MusicValue", 1f);
            set => PlayerPrefs.SetFloat("MusicValue", value);
        }

        public static bool IsItAllReadyLunched
        {
            get => PlayerPrefs.GetString("IsItFirstLunch", "false") == "true";
            set => PlayerPrefs.GetString("IsItFirstLunch", value.ToString());
        }
        
        public static bool IsItFirstEverLunch
        {
            get => PlayerPrefs.GetString("IsItFirstEverLunch", "true") == "true";
            set => PlayerPrefs.GetString("IsItFirstEverLunch", value.ToString());
        }
    }
}