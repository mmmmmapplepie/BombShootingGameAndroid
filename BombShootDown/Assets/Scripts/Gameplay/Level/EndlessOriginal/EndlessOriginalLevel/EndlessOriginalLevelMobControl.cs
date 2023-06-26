using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class EndlessOriginalLevelMobControl : MonoBehaviour {
  // public partial class EndlessOriginalLevel {
  //mobs by tiers
  [SerializeField] GameObject prefabobject;
  Enemy[][] mobsByTier = new Enemy[5][];
  [SerializeField] Enemy[] tier0Mobs, tier1Mobs, tier2Mobs, tier3Mobs, tier4Mobs;



  // 1st array is the actual values comapared for the rates
  // 2nd array is the current percentage (gap) of the tier
  // 3rd array is the final percentages to reach, the beginning percentages are put direclty in the beginning so don't matter
  int[,] difficultyRates = new int[3, 5] { { 60, 80, 100, 100, 100 }, { 60, 20, 20, 0, 0 }, { 0, 10, 20, 30, 40 } };


  //async cancellation token stuff
  CancellationTokenSource cancelToken = new CancellationTokenSource();


  bool wavecycle = false;
  void Start() {
    EndlessSpawner();
  }
  async void EndlessSpawner() {
    while (true) {
      if (cancelToken.IsCancellationRequested) return;
      if (wavecycle) { await Task.Yield(); continue; }
      wavecycle = true;
      int wavesNumber = Random.Range(1, 4);
      Task[] tasks = new Task[wavesNumber];
      tasks[0] = StartRandomWave(true);
      for (int i = 0; i < wavesNumber; i++) {
        tasks[i] = StartRandomWave();
      }
      await Task.WhenAll(tasks);
      wavecycle = false;
    }
  }
  async Task StartRandomWave(bool FirstSubWave = false) {
    float period = Random.Range(5f, 10f);
    float delay = Random.Range(4f, 5f);

    //spawnperiod:burst/delayed; oosition: scattered/bunched
    bool burst = Random.Range(0, 2) == 1 ? true : false;
    bool scattered = Random.Range(0, 2) == 1 ? true : false;
    await AsyncAdditional.Delay(delay, true);
    //difficulty goes from 0 up to 4. (0 is easiest)
    int difficulty = getDifficulty();
    if (cancelToken.IsCancellationRequested) return;
    Instantiate(prefabobject, Vector3.zero, Quaternion.identity);
    //make coroutine for actually instantiating stuff so that the game doesnt break due to missing cancellationtokens miss

    if (cancelToken.IsCancellationRequested) return;
    await AsyncAdditional.Delay(period, true);

    if (FirstSubWave) {


      string before = "before change:" + $"\n" + $"{difficultyRates[1, 0]},{difficultyRates[1, 1]},{difficultyRates[1, 2]},{difficultyRates[1, 3]},{difficultyRates[1, 4]}";
      print(before);


      changeDifficultyRates(difficulty);


      string after = "after change:" + $"\n" + $"{difficultyRates[1, 0]},{difficultyRates[1, 1]},{difficultyRates[1, 2]},{difficultyRates[1, 3]},{difficultyRates[1, 4]}";
      print(after);


    }
    //picktype: burst/spread; position: scattered/bunched
    //pickEnemies
  }

  int getDifficulty() {
    int difficultyRandom = Random.Range(1, 101);
    for (int i = 0; i < 5; i++) {
      if (i == 0) {
        if (difficultyRandom <= difficultyRates[0, i]) {
          return i;
        }
      } else {
        if (difficultyRandom > difficultyRates[0, i - 1] && difficultyRandom <= difficultyRates[0, i]) {
          return i;
        }
      }
    }
    return 0;
  }
  void changeDifficultyRates(int thisDifficulty) {
    if (difficultyRates[1, thisDifficulty] > difficultyRates[2, thisDifficulty]) {
      difficultyRates[0, thisDifficulty] -= 5;
    }
    updateDifficultyGapArray();
  }
  void updateDifficultyGapArray() {
    difficultyRates[1, 0] = difficultyRates[0, 0];
    for (int i = 1; i < 5; i++) {
      difficultyRates[1, i] = difficultyRates[0, i] - difficultyRates[0, i - 1];
    }
  }
  void OnDestroy() {
    cancelToken.Cancel();
  }
}
