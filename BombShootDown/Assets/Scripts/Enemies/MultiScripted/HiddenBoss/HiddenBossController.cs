using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenBossController : MonoBehaviour {
  [SerializeField] HiddenBossLife lifeScript;

  [SerializeField] HiddenBoss_Buff BuffSkill;
  [SerializeField] HiddenBoss_Debuff DebuffSkill;
  [SerializeField] HiddenBoss_Invisible InvisibleSkill;
  [SerializeField] HiddenBoss_Summon SummonSkill;
  [SerializeField] HiddenBoss_Vampire VampireSkill;
  [SerializeField] HiddenBoss_DOT DOTSkill;
  [SerializeField] HiddenBoss_Teleport TeleportSkill;
  [SerializeField] HiddenBoss_BlockPierce BlockPierceSkill;
  [SerializeField] HiddenBoss_Pull PullSkill;
  [SerializeField] GameObject buffLight, debuffLight, invisibleLight, summonLight, vampireLight, dotLight, teleportLight, pullLight, blockPierceLight;

  [SerializeField] Slider timer;

  List<string> Skills = new List<string> { "Invisible", "Debuff", "Buff", "Summon", "Vampire", "DOT", "Teleport", "Pull", "BlockPierce" };

  float cycleStartTime = 0f;
  int[] SkillsPerState = { 3, 5, 7 };

  void Start() {
    StartCoroutine(SkillCycleRoutine());
  }
  IEnumerator SkillCycleRoutine() {
    timer.gameObject.SetActive(true);
    cycleStartTime = Time.time;
    disableAllSkills();
    StartSkills();
    while (true) {
      //every 30 seconds change skills
      if (Time.time - cycleStartTime < 30f) {
        timer.value = (30f + cycleStartTime - Time.time) / 30f;
      } else {
        cycleStartTime = Time.time;
        disableAllSkills();
        StartSkills();
      }
      yield return null;
    }
  }
  void disableAllSkills() {
    BuffSkill.enabled = false;
    DebuffSkill.enabled = false;
    InvisibleSkill.enabled = false;
    SummonSkill.enabled = false;
    VampireSkill.enabled = false;
    DOTSkill.enabled = false;
    TeleportSkill.enabled = false;
    PullSkill.enabled = false;
    BlockPierceSkill.enabled = false;


    //disable all lights;
    // buffLight.SetActive(false);
    // debuffLight.SetActive(false);
    // invisibleLight.SetActive(false);
    // summonLight.SetActive(false);
    // vampireLight.SetActive(false);
    // dotLight.SetActive(false);
    // teleportLight.SetActive(false);
    // pullLight.SetActive(false);
    // blockPierceLight.SetActive(false);

    // timer slider
    timer.gameObject.SetActive(false);
  }
  void chooseRandomAbilities() {
    List<string> tempSkillsCopy = Skills;
    int count = 0;
    while (count < SkillsPerState[lifeScript.currentStage]) {
      string skillToAdd = tempSkillsCopy[Random.Range(0, tempSkillsCopy.Count)];
      tempSkillsCopy.Remove(skillToAdd);
      StartCoroutine(skillToAdd);
      count++;
    }
  }
  public void StopSkills() {
    StopAllCoroutines();
    disableAllSkills();
  }
  public void StartSkills() {
    StartCoroutine(SkillCycleRoutine());
  }



  #region skill&respectivecallingCoroutines
  IEnumerator Buff() {
    BuffSkill.enabled = true;
    buffLight.SetActive(true);
    yield return null;
  }
  IEnumerator Debuff() {
    DebuffSkill.enabled = true;
    debuffLight.SetActive(true);
    yield return null;
  }
  IEnumerator Invisible() {
    InvisibleSkill.enabled = true;
    invisibleLight.SetActive(true);
    yield return null;
  }
  IEnumerator Vampire() {
    VampireSkill.enabled = true;
    vampireLight.SetActive(true);
    yield return null;
  }
  IEnumerator Summon() {
    SummonSkill.enabled = true;
    summonLight.SetActive(true);
    yield return null;
  }
  IEnumerator DOT() {
    DOTSkill.enabled = true;
    dotLight.SetActive(true);
    yield return null;
  }
  IEnumerator Teleport() {
    TeleportSkill.enabled = true;
    teleportLight.SetActive(true);
    yield return null;
  }
  IEnumerator Pull() {
    PullSkill.enabled = true;
    pullLight.SetActive(true);
    yield return null;
  }
  IEnumerator BlockPierce() {
    BlockPierceSkill.enabled = true;
    blockPierceLight.SetActive(true);
    yield return null;
  }
  #endregion
}
