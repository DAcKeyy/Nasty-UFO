using System.Collections;
using Data.Generators;
using Generation.Base;
using UnityEngine;

namespace Generation
{
    //безпечный опекун
    public class LevelGenerator : MonoBehaviour
    {
        //TODO Убрать зависимость от NastyUFOLevelGenerationSettings
        private NastyUFOLevelGenerationSettings _levelSettings;
        private ILevelGenerator _levelGeneration;
        private IEnumerator _updateCoroutine;
        
        private void OnEnable()
        {
            _levelGeneration.Create();
            
            _updateCoroutine = UpdateLevel();
            
            StartCoroutine(_updateCoroutine);
            
            /*
            _signalBus.Subscribe<PlayerDiedSignal>(x => {
                StopCoroutine(_updateCoroutine);
            });
            */
            
            /*
            _signalBus.Subscribe<GameStarted>(x => {
                //Ставим режим 2... хе, а что это за режим? Это генератор как бы знает что то, но нет..
                _levelGeneration.SetMode(2);
            });
            */
        }

        //TODO Убрать зависимость от MonoBehaviour
        private IEnumerator UpdateLevel()
        {
            while (true)
            {
                yield return new WaitForSeconds(_levelSettings._levelUpdateRate);;
                _levelGeneration.Update();
            }
        }
    }
}