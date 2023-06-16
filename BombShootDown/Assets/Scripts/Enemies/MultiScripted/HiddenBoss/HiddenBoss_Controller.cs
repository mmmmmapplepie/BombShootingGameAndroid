using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_Controller : MonoBehaviour {
  [SerializeField] HiddenBossLife lifeScript;
  [SerializeField] HiddenBoss_Buff BuffSkill;
  [SerializeField] HiddenBoss_Debuff DebuffSkill;
  [SerializeField] HiddenBoss_Invisible InvisibleSkill;
  [SerializeField] HiddenBoss_Summon SummonSkill;
  [SerializeField] HiddenBoss_Vampire VampireSkill;
  [SerializeField] GameObject buffLight, debuffLight, invisibleLight, summonLight, vampireLight;

  List<string> Skills = new List<string> { "Invisible", "Debuff", "Buff", "Summon", "Vampire" };


  float cycleStartTime = 0f;
  int[] SkillsPerState = { 4, 6, 8 };
  public int currentStage = 0;

  void Start() {
    StartCoroutine(SkillCycleRoutine());
  }
  IEnumerator SkillCycleRoutine() {
    cycleStartTime = Time.time;
    while (true) {
      yield return null;

    }
  }
  void disableAllSkills() {
    BuffSkill.enabled = false;
    DebuffSkill.enabled = false;
    InvisibleSkill.enabled = false;
    SummonSkill.enabled = false;
    VampireSkill.enabled = false;
    // BuffSkill.enabled = false;
    // BuffSkill.enabled = false;
  }
  void chooseRandomAbilities() {
    List<string> tempSkillsCopy = Skills;
    int count = 0;
    while (count < SkillsPerState[currentStage]) {
      string skillToAdd = tempSkillsCopy[Random.Range(0, tempSkillsCopy.Count)];
      tempSkillsCopy.Remove(skillToAdd);
      StartCoroutine(skillToAdd);
      count++;
    }
  }
  void ResetSkillRoutine() {
    StopAllCoroutines();
    disableAllSkills();
    StartCoroutine(SkillCycleRoutine());
  }









  #region skill&respectivecallingCoroutines
  IEnumerator Buff() {
    BuffSkill.enabled = true;
    yield return null;
  }
  IEnumerator Debuff() {
    DebuffSkill.enabled = true;
    yield return null;
  }
  IEnumerator Invisible() {
    InvisibleSkill.enabled = true;
    yield return null;
  }
  IEnumerator Vampire() {
    VampireSkill.enabled = true;
    yield return null;
  }
  IEnumerator Summon() {
    SummonSkill.enabled = true;
    yield return null;
  }



















  #endregion
}
